using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using SmartSchool.BL.Repository;
using SmartSchool.BL.DTO;

namespace SmartSchool.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeYearController : ControllerBase
    {
        public IGradeYearRepo GradeYearRepo { get; }
        public GradeYearController(IGradeYearRepo gradYearRepo)
        {
            GradeYearRepo = gradYearRepo;

        }

        [HttpGet]
        [Route("GetAll")]
        //[Authorize(Roles = "Admin,Student,Parent")]
        public IActionResult Get()
        {
            try
            {
                var allGradeYears = GradeYearRepo.GetAll();
                return Ok(allGradeYears);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetById/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetById(int id)
        {
            try
            {
                var myGradeYear = GradeYearRepo.GetById(id);
                if (myGradeYear == null)
                {
                    return NotFound();
                }
                return Ok(myGradeYear);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetByClassId/{id}")]
        [Authorize(Roles = "Student,Parent")]
        public IActionResult GetByClassId(int id)
        {
            try
            {
                var myGradeYear = GradeYearRepo.GetByClassId(id);
                if (myGradeYear == null)
                {
                    return NotFound();
                }
                return Ok(myGradeYear);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(GradeYearDTO obj)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var data = GradeYearRepo.Create(obj);
                    if (data == null)
                    {
                        return BadRequest("Grade Year Already Exists");
                    }

                    return Ok(data);
                }
                return BadRequest();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                GradeYearRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
