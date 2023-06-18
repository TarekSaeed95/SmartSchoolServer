using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using SmartSchool.BL.Repository;
using SmartSchool.BL.DTO;
using SmartSchool.DAL.Entities;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.Api.Controllers.teacher
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAttendanceController : ControllerBase
    {

        public IStudentAttendanceRepo studentAttendance { get; }
        public StudentAttendanceController(IStudentAttendanceRepo StudentAttendanceRepo)
        {
            studentAttendance = StudentAttendanceRepo;

        }


  


        [HttpGet]
        [Route("generateAttendance/{classid}")]
        [Authorize(Roles = "Teacher")]

        public IActionResult generate(int classid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    studentAttendance.generateAttendance();
                    var studentsAtt = studentAttendance.getAllAttendance(classid);
                    return Ok(studentsAtt);
                }
                return BadRequest();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("addStudentAttendance")]
        [Authorize(Roles = "Teacher")]

        public IActionResult add(List<StudentAttendanceDTO> studentAtt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    studentAttendance.addStudentAttendance(studentAtt);
                    return Ok();
                }
                return BadRequest();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }




    }

}


