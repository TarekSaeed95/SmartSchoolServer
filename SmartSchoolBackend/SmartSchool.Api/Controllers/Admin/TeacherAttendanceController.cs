using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using SmartSchool.BL.DTO;
using SmartSchool.DAL.Entities;
using System.Data;

namespace SmartSchool.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAttendanceController : ControllerBase
    {
        public ITeacherAttendanceRepo Repo { get; }
        public TeacherAttendanceController(ITeacherAttendanceRepo _repo)
        {
            Repo = _repo;
        }

        [HttpGet]
        [Route("generateAttendance")]
        [Authorize(Roles = "Admin")]
        public IActionResult Generate()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repo.GenerateAttendance();
                    var TeachersAtt = Repo.GetAllAttendance();
                    return Ok(TeachersAtt);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("addTeacherAttendance")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(List<TeacherAttendanceDTO> teacherAtt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repo.AddTeacherAttendance(teacherAtt);
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
