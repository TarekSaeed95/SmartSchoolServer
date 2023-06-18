using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Helpers;
using SmartSchool.BL.Interface;
using SmartSchool.BL.Models;
using SmartSchool.BL.Repository;
using SmartSchool.BL.Services;
using SmartSchool.BL.DTO;
using SmartSchool.DAL.Entities;
using System.Data;

namespace SmartSchool.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        public ITeacherRepo TeacherRepo { get; }
        private readonly IAuthService authService;
        private readonly UserManager<IdentityUser> UserManager;
        public TeacherController(ITeacherRepo teacherrepo, IAuthService authService, UserManager<IdentityUser> userManager)
        {
            TeacherRepo = teacherrepo;
            this.authService = authService;
            UserManager = userManager;
        }

        [HttpPost]
        [Route("Save")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAsync(TeacherDTO obj)
        {
            if (obj == null)
            {
                return BadRequest("Object Is Null");
            }
            if (UserManager.FindByEmailAsync(obj.Email)?.Result != null)
            {
                return BadRequest("This Email ALready Exists");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (obj.Photo != null)
                        obj.PhotoUrl = UploadFile.Photo(obj.Photo, "TeacherImages");

                    RegisterModel myTeacher = new RegisterModel()
                    {
                        Email = obj.Email,
                        Username = obj.Email,
                        Password = obj.Password,
                        myRole = "Teacher"
                    };

                    var Teacher = await authService.RegisterAsync(myTeacher);
                    var TeacherIdentityId = UserManager.FindByEmailAsync(myTeacher.Email).Result.Id;
                    string teacherId = TeacherRepo.SaveInDb(obj, TeacherIdentityId);
                    return Ok(new { id = teacherId });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            try
            {
                var allTeachers = TeacherRepo.GetAll();
                return Ok(allTeachers);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
       
        [HttpGet]
        [Route("GetById/{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult GetById(string id)
        {
            try
            {
                var myTeacher = TeacherRepo.GetById(id);
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

        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            try
            {
                TeacherRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch]
        [Route("Edit")]
        [Authorize(Roles = "Admin,Teacher")]
        public ActionResult Edit(TeacherEditDTO obj)
        {
			try
			{
				if (ModelState.IsValid)
				{
					Teacher t = TeacherRepo.Edit(obj);
                    return Ok(t);
		        }
                else
                {
                    return BadRequest();
	            }
            }
            catch (Exception ex)
            {
	            return BadRequest(ex.Message);
            }
        }

    }
}

