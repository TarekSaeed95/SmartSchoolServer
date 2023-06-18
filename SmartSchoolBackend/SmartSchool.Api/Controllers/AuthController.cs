using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SmartSchool.BL.Services;
using SmartSchool.BL.Models;
using SmartSchool.BL.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Data;
//using JWT.Models;
//using JWT.Services;

namespace SmartSchool.BL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {


        #region

        /*
         {
              "FirstName": "ahmed",
              "LastName": "sayed",
              "Username": "ahmedsayed",
              "Email": "saye@gmail.com",
              "Password": "Ahmed_123"
          }
        */

        #endregion

        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authService.RegisterAsync(model);

            //if user name or email is already exists
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);


            //return Ok({ token = result.Token, expireOn = result.ExpireOn});

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authService.LoginAsync(model);

            //if user name or email is already exists
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);


            //return Ok({ token = result.Token, expireOn = result.ExpireOn});

            return Ok(result);
        }

        [HttpPost("addrole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authService.AddRoleAsync(model);

            
            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);


            return Ok(model);
        }
		[HttpGet("confirmemail")]
		public async Task<IActionResult> ConfirmEmail(string userId, string token)
		{
			if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
				return NotFound();

			var result = await authService.ConfirmEmailAsync(userId, token);

			if (result.IsAuthenticated)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}


		[HttpPost("forgotpassword")]
		public async Task<IActionResult> ForgotPassword(string email)
		{
			if (string.IsNullOrEmpty(email))
				return NotFound();

			var result = await authService.ForgotPasswordAsync(email);

			if (result.IsAuthenticated)
				return Ok(result);

			return BadRequest(result);
		}

		[HttpPost("resetpassword")]
		public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
		{
			if (ModelState.IsValid)
			{
				var result = await authService.ResetPasswordAsync(model);
				if (result.IsAuthenticated)
					return Ok(result);

				return BadRequest(result);
			}
			return BadRequest("Something went wrong");
		}

        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordVM)
        {
            if (ModelState.IsValid)
            {
                var result = await authService.ChangePasswordAsync(changePasswordVM);
                if (result.IsAuthenticated)
                    return Ok(result);
                return BadRequest(result);
            }
			return BadRequest("Something went wrong");
		}

	}
}
