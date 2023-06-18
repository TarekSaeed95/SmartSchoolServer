using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using SmartSchool.BL.DTO;

namespace SmartSchool.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        public ISubjectRepo SubjectRepo { get; }
        public SubjectController(ISubjectRepo gradYearRepo)
        {
            SubjectRepo = gradYearRepo;

        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Admin")]

        public IActionResult Get()
        {
            try
            {
                var allSubjects = SubjectRepo.GetAll();
                return Ok(allSubjects);
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
                var mySubject = SubjectRepo.GetById(id);
                if (mySubject == null)
                {
                    return NotFound();
                }
                return Ok(mySubject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(SubjectDTO obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = SubjectRepo.Create(obj);

                    if (data == null)
                    {
                        return BadRequest("this subject already exist in this grade year");
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
                SubjectRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
		
        //parent, student, teacher can access this action
        [HttpGet]
		[Route("GetByClassId/{id}")]
        [Authorize(Roles ="Student")]
		public IActionResult GetByClassId(int id)
		{
			try
			{
				var mySubject = SubjectRepo.GetByClassRoom(id);
				if (mySubject == null)
				{
					return NotFound();
				}
				return Ok(mySubject);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

        [HttpGet]
        [Route("GetByGradeYearId/{id}")]
        public IActionResult GetByGradeYearId(int id)
        {
            try
            {
                var mySubject = SubjectRepo.GetByGradeYear(id);
                if (mySubject == null)
                {
                    return NotFound();
                }
                return Ok(mySubject);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
