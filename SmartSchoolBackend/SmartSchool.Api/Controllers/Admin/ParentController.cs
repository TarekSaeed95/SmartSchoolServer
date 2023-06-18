using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using SmartSchool.BL.Repository;
using SmartSchool.BL.DTO;
using SmartSchool.DAL.Context;
using SmartSchool.DAL.Entities;
using System.Data;

namespace SmartSchool.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        public IParentRepo ParentRepo { get; }
        public ParentController(IParentRepo _repo)
        {
            ParentRepo = _repo;
        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            try
            {
                var allParents = ParentRepo.GetAll();
                return Ok(allParents);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetById/{id}")]
        [Authorize(Roles = "Parent")]
        public IActionResult GetById(string id)
        {
            try
            {
                var myPnt = ParentRepo.GetbyId(id);
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


        [HttpPut]
        [Route("Edit")]
        [Authorize(Roles = "Parent")]
        public IActionResult Edit(ParentDTO pnt)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Parent P = ParentRepo.Edit(pnt);
                    return Ok(P);
                }
                return BadRequest();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }


        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            try
            {
                ParentRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }



    }
}

