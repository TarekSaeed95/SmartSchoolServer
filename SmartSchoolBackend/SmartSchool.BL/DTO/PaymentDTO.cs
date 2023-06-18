using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.DTO
{
	public class PaymentDTO
	{
		//public string? CustomerName { get; set; }
		//public string? CustomerEmail { get; set; }
		public string? ParentID { get; set; }
		public long? Amount { get; set; }
		public string? StudentID { get; set; }
		public string? CardName { get; set; }
		public string? CardNumber { get; set; }
		public string? CardExpiryYear { get; set; }
		public string? CardExpiryMonth { get; set; }
		public string? CardCvc { get; set; }
	}
}
