using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using SmartSchool.BL.DTO;



namespace SmartSchool.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassRoomController : ControllerBase
    {
        public IClassRoomRepo ClassRoomRepo { get; }
        public ClassRoomController(IClassRoomRepo classRoomRepo)
        {
            ClassRoomRepo = classRoomRepo;

        }

        [HttpGet]
        [Route("GetAll")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            try
            {
                var allClassRooms = ClassRoomRepo.GetAll();
                return Ok(allClassRooms);
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
                var myClassRoom = ClassRoomRepo.GetById(id);
                if (myClassRoom == null)
                {
                    return NotFound();
                }
                return Ok(myClassRoom);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetBySubjectId/{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult GetBySubjectId(int id)
        {
            try
            {
                var myClassRoom = ClassRoomRepo.GetBySubjectId(id);
                if (myClassRoom == null)
                {
                    return NotFound();
                }
                return Ok(myClassRoom);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ClassRoomDTO obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = ClassRoomRepo.Create(obj);

                    if(data == null)
                    {
                        return BadRequest("Class Room Already Exist");
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
                ClassRoomRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
