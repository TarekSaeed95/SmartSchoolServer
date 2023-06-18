using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.DTO;
using SmartSchool.BL.Interface;
using SmartSchool.DAL.Entities;

namespace SmartSchool.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		public IPaymentRepo PaymentRepo { get; }
		public PaymentController(IPaymentRepo paymentRepo)
		{
			PaymentRepo = paymentRepo;
		}

		[HttpPost("parentpayment")]
		public async Task<ActionResult<Payment>> CustomerCharge([FromBody] PaymentDTO resource)
		{
			var response = await PaymentRepo.CreateCustomerCharge(resource);

			//return response;
			return Ok(response);
		}
	}
}
