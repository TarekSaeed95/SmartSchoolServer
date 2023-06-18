using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using SmartSchool.BL.Repository;
using System.Data;

namespace SmartSchool.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherUserController : ControllerBase
    {
        public ITeacherRepo TeacherRepo { get; }
		public IScheduleRepo ScheduleRepo { get; }

		public TeacherUserController(ITeacherRepo teacherrepo, IScheduleRepo scheduleRepo)
        {
            this.TeacherRepo = teacherrepo;
			ScheduleRepo = scheduleRepo;
		}

        [HttpGet]
        [Route("GetByIdentity/{id}")]
        [Authorize(Roles = "Teacher")]

        public IActionResult GetByIdentity(string id)
        {
            try
            {
                var myTeacher = TeacherRepo.GetByIdentity(id);
                if (myTeacher == null)
                {
                    return NotFound();
                }
                return Ok(myTeacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Getsessions/{id}/{start}/{end}")]
        [Authorize(Roles = "Teacher")]
        public IActionResult GetSessions(string id, DateTime start, DateTime end)
        {
            var sessions = ScheduleRepo.GetTeacherSchedule(id, start, end);
            return Ok(sessions);
        }
    }
}
