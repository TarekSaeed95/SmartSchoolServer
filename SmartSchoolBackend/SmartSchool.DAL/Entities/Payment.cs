using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.DAL.Entities
{
	public class Payment
	{
		[Key]
		public int Id { get; set; }
		public string? StripePaymentID { get; set; }
		public string? StripeCustomerID { get; set; }
		public long? Amount { get; set; }
		public string? StudentID { get; set; }
		public string? ParentID { get; set; }
		//public string CustomerName { get; set; }
		//public string CustomerEmail { get; set; }
		//public string CardName { get; set; }
		//public string CardNumber { get; set; }
		//public string CardExpiryYear { get; set; }
		//public string CardExpiryMonth { get; set; }
		//public string CardCvc { get; set; }
	
	}


}
