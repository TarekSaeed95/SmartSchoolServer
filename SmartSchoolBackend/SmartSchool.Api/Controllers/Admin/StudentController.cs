using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using SmartSchool.BL.Repository;
using SmartSchool.BL.DTO;
using SmartSchool.DAL.Entities;
using System.Data;

namespace SmartSchool.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public IStudentRepo Repo { get; }
        public IExamResultRepo examRepo { get; }
        public StudentController(IStudentRepo _repo, IExamResultRepo _examRepo)
        {
            Repo = _repo;
            examRepo= _examRepo;
        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            try
            {
                var allStudents = Repo.GetAll();
                return Ok(allStudents);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetById/{id}")]
        [Authorize(Roles = "Admin,Parent")]

        public IActionResult GetById(string id)
        {
            try
            {
                var myStud = Repo.GetbyId(id);
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
        [Route("GetByClass/{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult GetStudentByClass(int id)
        {
            try
            {
                var myStud = Repo.GetStudentByClass(id);
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
        [Route("GetByGradeYear/{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult GetStudentByGradeYear(int id)
        {
            try
            {
                var myStud = Repo.GetStudentByGradeYear(id);
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

        [HttpPut]
        [Route("Edit")]
        [Authorize(Roles = "Admin,Student")]
        public IActionResult Edit(StudentDTO std)
        {
            Student S = Repo.Edit(std);
            return Ok(S);
        }

        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            try
            {
                Repo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetAbsenceStudents")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAbsenceStudents()
        {
            try
            {
                var allStudents = Repo.getAbsenceStudents();
                return Ok(allStudents);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        
        [HttpGet]
        [Route("GetStudentsGrade/{studentid}/{gradeYearId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetStudentsGrade(string studentid,int gradeYearId)
        {
            try
            {
                var myGrades = examRepo.getFullGrade(studentid,gradeYearId);
                return Ok(myGrades);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }


        [HttpGet]
        [Route("CheckStudentsForClassRoom/{id}")]
        //[Authorize(Roles = "Admin,Teacher")]
        public IActionResult CheckStudentsForClassRoom(int id)
        {
            try
            {
                bool IsThereStudents = Repo.CheckStudentsForClassRoom(id);
                
                return Ok(new { CheckStudent = IsThereStudents });
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
