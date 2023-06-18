using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using SmartSchool.BL.Repository;
using System.Data;

namespace SmartSchool.Api.Controllers.parent
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentUserController : ControllerBase
    {
        public IParentRepo ParentRepo { get; }
        public IScheduleRepo ScheduleRepo { get; }
        public IStudentRepo StudentRepo { get; }
        public IExamResultRepo ExamResultRepo { get; }

        public ParentUserController(IParentRepo _repo,IScheduleRepo scheduleRepo, IStudentRepo studentRepo ,IExamResultRepo examResult)
        {
            ParentRepo = _repo;
            ScheduleRepo = scheduleRepo;
            StudentRepo = studentRepo;
            ExamResultRepo = examResult;
        }

        [HttpGet]
        [Route("GetParentStudents")]
        [Authorize(Roles = "Parent")]
        public IActionResult Get(string id)
        {
            try
            {
                var allStudents = StudentRepo.GetByParentId(id);
                return Ok(allStudents);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        //[HttpGet]
        //[Route("GetMyChildsSchedule/{classid}/{start}/{end}")]
        //public IActionResult GetMyChildsSchedule(int classid, DateTime start, DateTime end)
        //{
        //    try
        //    {
        //        var childSchedule = ScheduleRepo.GetStudentSchedule(classid, start, end);
        //        return Ok(childSchedule);
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.Message);
        //    }

        //}
        [HttpGet]
        [Route("GetByIdentity/{id}")]
        [Authorize(Roles = "Parent")]
        public IActionResult GetByIdentity(string id)
        {
            try
            {
                var myPnt = ParentRepo.GetByIdentity(id);
                if (myPnt == null)
                {
                    return NotFound();
                }
                return Ok(myPnt);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet]
        //[Route("GetMyStudentResult/{studentId}/{gradeYearId}")]
        //public IActionResult GetMyStudentResult(string studentId, int gradeYearId)
        //{
        //    try
        //    {
        //        var myGrades = ExamResultRepo.getStudentExamResults(studentId, gradeYearId);
        //        return Ok(myGrades);
        //    }

        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.Message);
        //    }

        //}

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
