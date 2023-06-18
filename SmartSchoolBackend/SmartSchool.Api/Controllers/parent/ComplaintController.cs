using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using SmartSchool.BL.DTO;
using System.Data;

namespace SmartSchool.Api.Controllers.parent
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        public IComplaintRepo complaint;

        public ComplaintController(IComplaintRepo comp)
        {
            complaint = comp;
        }

        [HttpPost]
        [Authorize(Roles = "Parent")]
        public IActionResult SendEmail(ComplaintDTO request)
        {
            try
            {
                complaint.SendEmail(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
