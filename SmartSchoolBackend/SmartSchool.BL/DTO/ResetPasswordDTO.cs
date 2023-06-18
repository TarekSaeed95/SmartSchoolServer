using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.DTO
{
	public class ResetPasswordDTO
	{
		[Required]
		public required string Token { get; set; }
		[Required]
		[EmailAddress]
		public required string Email { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password must contain at least (8 charachters, 1 Special Charachter, 1 UpperCase, 1 LowerCase, 1 Digit")]
		public required string NewPassword { get; set; }

		[Required]
		//[StringLength(50, MinimumLength = 8)]
		//[Compare("NewPassword",ErrorMessage ="Passwords don't match")]
		public required string ConfirmPassword { get; set; }

	}
}
