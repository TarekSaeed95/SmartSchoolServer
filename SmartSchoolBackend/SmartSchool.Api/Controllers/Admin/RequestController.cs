using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Helpers;
using SmartSchool.BL.Interface;
using SmartSchool.BL.Models;
using SmartSchool.BL.Services;
using SmartSchool.BL.DTO;
using System.Data;


namespace SmartSchool.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly UserManager<IdentityUser> UserManager;
        public IRequestRepo RequestRepo { get; }

        public RequestController(IRequestRepo requestRepo, IAuthService authService, UserManager<IdentityUser> userManager)
        {
            RequestRepo = requestRepo;
            this.authService = authService;
            UserManager = userManager;
        }


        [HttpPost]
        [Route("Create")]
        public IActionResult CreateRequest(RequestDTO obj)
        {
            if (obj == null)
            {
                return BadRequest("Object Is Null");
            }

            if (UserManager.FindByEmailAsync(obj.StudentEmail)?.Result?.Email == obj.StudentEmail || RequestRepo.RequestExist(obj))
            {
                return BadRequest("student email already exists");
            }
            if (obj.StudentEmail == obj.ParentEmail)
            {
                return BadRequest("parent email and student email must be different");
            }


            try
            {
                if (ModelState.IsValid)
                {

                    if (obj.StudentPhoto != null)
                    {
                        obj.StudentPhotoUrl = UploadFile.Photo(obj.StudentPhoto, "StudentImages");
                    }
                    if (obj.IdentityParentPhoto != null)
                    {
                        obj.IdentityParentPhotoUrl = UploadFile.Photo(obj.IdentityParentPhoto, "IdentityParentImages");
                    }
                    if (obj.StudentBirthCertPhoto != null)
                    {
                        obj.StudentBirthCertPhotoUrl = UploadFile.Photo(obj.StudentBirthCertPhoto, "StudentImages");
                    }

                    var requestId = RequestRepo.Create(obj);

                    return Ok(requestId);
                }
                return BadRequest();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Admin")]

        public IActionResult Get()
        {
            try
            {
                var allRequests = RequestRepo.GetAll();
                return Ok(allRequests);
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
                var myRequest = RequestRepo.GetById(id);
                if (myRequest == null)
                {
                    return NotFound();
                }
                return Ok(myRequest);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }



        [HttpPost]
        [Route("Save/{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> RegisterAsync(int id)
        {

            var obj = RequestRepo.GetById(id);

            RegisterModel myParent = new RegisterModel()
            {
                Email = obj.ParentEmail,
                Username = obj.ParentEmail,
                Password = EncodePassword.DecodeFrom64(obj.Password),
                myRole = "Parent"
            };

            RegisterModel myStudent = new RegisterModel()
            {
                Email = obj.StudentEmail,
                Username = obj.StudentEmail,
                Password = EncodePassword.DecodeFrom64(obj.Password),
                myRole = "Student"
            };

            if (UserManager.FindByEmailAsync(obj.ParentEmail)?.Result?.Email != obj.ParentEmail)
            {
                var Parent = await authService.RegisterAsync(myParent);
            }


            var Student = await authService.RegisterAsync(myStudent);


            var ParentIdentityId = UserManager.FindByEmailAsync(myParent.Email).Result.Id;
            var StudentIdentityId = UserManager.FindByEmailAsync(myStudent.Email).Result.Id;

            var std = RequestRepo.SaveInDb(obj.id, ParentIdentityId, StudentIdentityId);
            RequestRepo.Delete(id);
            return Ok(new { studentid = std });
        }



        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                RequestRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
