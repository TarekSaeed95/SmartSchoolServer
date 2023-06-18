using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartSchool.BL.DTO;
using SmartSchool.BL.Interface;
using SmartSchool.BL.Services;
using SmartSchool.DAL.Context;
using SmartSchool.DAL.Entities;
using Stripe;


namespace SmartSchool.BL.Repository
{
	public class PaymentRepo : IPaymentRepo
	{
		public TokenService TokenService { get; }
		public CustomerService CustomerService { get; }
		public ChargeService ChargeService { get; }
		public SmartSchoolContext Db { get; }
        public IMailService MailService { get; }

        public PaymentRepo(TokenService tokenService, CustomerService customerService, ChargeService chargeService, SmartSchoolContext db, IMailService mailService)
		{
			TokenService = tokenService;
			CustomerService = customerService;
			ChargeService = chargeService;
			Db = db;
            this.MailService = mailService;
        }

		public async Task<Payment> CreateCustomerCharge(PaymentDTO resource)
		{
			var tokenOptions = new TokenCreateOptions
			{
				Card = new TokenCardOptions
				{
					Name = resource.CardName,
					Number = resource.CardNumber,
					ExpYear = resource.CardExpiryYear,
					ExpMonth = resource.CardExpiryMonth,
					Cvc = resource.CardCvc
				}
			};

			var token = await TokenService.CreateAsync(tokenOptions);

			var parent = Db.Parents.Include(s => s.IdentityUser).Where(p => p.Id == resource.ParentID).FirstOrDefault();
			
			var customerOptions = new CustomerCreateOptions
			{
				Name = parent.ParentFullName,
				Email = parent.IdentityUser.Email,
				Source = token.Id
			};

			//dh hysm3 fe stripe dashboard customers
			var customer = await CustomerService.CreateAsync(customerOptions);


			var chargeOptions = new ChargeCreateOptions
			{
				Amount = resource.Amount,
				ReceiptEmail = parent.IdentityUser.Email,
				Currency = "egp",
				Customer = customer.Id,
				Description = "School Fees"
			};

			//dh hysme fe stripe dashboard payments
			var charge = await ChargeService.CreateAsync(chargeOptions);

			Db.Students.Find(resource.StudentID).Fees = true;

			


            Payment P = new Payment()
			{
				Amount = resource.Amount,
				StripeCustomerID = customer.Id,
				StripePaymentID = charge.Id,
				ParentID = resource.ParentID,
				StudentID = resource.StudentID,
			};

            Db.payments.Add(P);
			Db.SaveChanges();

            await MailService.SendEmailAsync(parent.IdentityUser.Email, "Payment Success",
               $"<p>Your payment has been succeessfully completed \nPayment ID: {charge.Id}");
            
			return P;
		}
	}
}
