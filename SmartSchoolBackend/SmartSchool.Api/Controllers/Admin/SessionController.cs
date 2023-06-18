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
    public class SessionController : ControllerBase
    {
        public ISessionRepo SessionRepo { get; }
        public SessionController(ISessionRepo sessionRepo)
        {
            SessionRepo = sessionRepo;
        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Admin")]

        public IActionResult Get()
        {
            try
            {
                var allSessions = SessionRepo.GetAll();
                return Ok(allSessions);
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
                var mySession = SessionRepo.GetById(id);
                if (mySession == null)
                {
                    return NotFound();
                }
                return Ok(mySession);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Create(SessionDTO obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //if (SessionRepo.CheckSchedule(obj))
                    //{
                    //    return BadRequest("This Class Room Has No Students");
                    //}
                    var data = SessionRepo.Create(obj);
                    if (data == null)
                    {
                        return BadRequest("Session conflicts with another session");
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
                SessionRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("Edit")]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(SessionDTO obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Session s = SessionRepo.Edit(obj);
                    return Ok(s);
                }
				return BadRequest();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

        [HttpGet]
        [Route("Getsessions/classanddate/{classid}/{date}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetByClassDate(int classid, DateTime date)
        {
            try
            {
                var mySchedule = SessionRepo.GetByClassIdDate(classid, date);
                if (mySchedule == null)
                {
                    return NotFound();
                }
                return Ok(mySchedule);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
