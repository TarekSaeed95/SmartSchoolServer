using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartSchool.BL.DTO;
using SmartSchool.DAL.Entities;

namespace SmartSchool.BL.Interface
{
	public interface IPaymentRepo
	{
		Task<Payment> CreateCustomerCharge(PaymentDTO resource);
	}
}
