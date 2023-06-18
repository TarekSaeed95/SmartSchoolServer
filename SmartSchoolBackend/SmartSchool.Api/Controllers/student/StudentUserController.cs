using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using System.Data;

namespace SmartSchool.Api.Controllers.student
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentUserController : ControllerBase
    {
        public IStudentRepo StudentRepo { get; }
        public IScheduleRepo ScheduleRepo { get; }
        public IExamResultRepo ExamResultRepo { get; }

        public StudentUserController( IStudentRepo _studentRepo, IScheduleRepo scheduleRepo, IExamResultRepo examResult)
        {
            StudentRepo = _studentRepo;
            ScheduleRepo = scheduleRepo;
            ExamResultRepo = examResult;
        }

        [HttpGet]
        [Route("GetMySchedule/{classid}/{start}/{end}")]
        [Authorize(Roles = "Student,Parent")]

        public IActionResult GetMySchedule(int classid, DateTime start, DateTime end)
        {
            try
            {
                var childSchedule = ScheduleRepo.GetStudentSchedule(classid, start, end);
                return Ok(childSchedule);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetMyExamResult/{studentId}/{gradeYearId}")]
        [Authorize(Roles = "Student,Parent")]

        public IActionResult GetMyExamResult(string studentId, int gradeYearId)
        {
            try
            {
                var myGrades = ExamResultRepo.getStudentExamResults(studentId, gradeYearId);
                return Ok(myGrades);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetByIdentity/{id}")]
        [Authorize(Roles = "Student")]

        public IActionResult GetByIdentity(string id)
        {
            try
            {
                var myStud = StudentRepo.GetByIdentity(id);
                if (myStud == null)
                {
                    return NotFound();
                }
                return Ok(myStud);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetFirstTermGrade/{studentId}/{gradeYearId}")]
        [Authorize(Roles = "Student,Parent")]

        public IActionResult GetFirstTermGrade(string studentId, int gradeYearId)
        {
            try
            {
                var myGrades = ExamResultRepo.getFirstTermGrade(studentId, gradeYearId);
                return Ok(myGrades);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

    }
}
