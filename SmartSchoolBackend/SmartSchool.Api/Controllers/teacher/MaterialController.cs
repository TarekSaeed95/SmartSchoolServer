using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Helpers;
using SmartSchool.BL.Interface;
using SmartSchool.BL.DTO;
using SmartSchool.DAL.Context;
using System.Data;

namespace SmartSchool.Api.Controllers.teacher
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {

        public IMaterialRepo MaterialRepo { get; }

        public MaterialsController(IMaterialRepo materialRepo)
        {
      
            MaterialRepo = materialRepo;
        }


        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        [Authorize(Roles = "Teacher")]

        public async Task<IActionResult> UploadMaterial()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                if (formCollection.Files.First().Length > 0)
                {

                    MaterialRepo.Material(formCollection);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        [Authorize(Roles = "Teacher")]

        public async Task<IActionResult> Get()
        {
            try
            {
                var materials= MaterialRepo.getAll();
                return Ok(materials);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("getbysubject/{subjectid}/{type}")]
        [Authorize(Roles = "Teacher,Student")]

        public async Task<IActionResult> GetBySubject(int subjectid, string type)
        {
            try
            {

                var Material = MaterialRepo.getBySubject(subjectid, type);
                return Ok(Material);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}