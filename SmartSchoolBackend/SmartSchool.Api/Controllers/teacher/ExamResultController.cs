using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Interface;
using SmartSchool.BL.DTO;
using SmartSchool.DAL.Entities;
using System.Collections.Generic;
using System.Data;

namespace SmartSchool.Api.Controllers.teacher
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExamResultController : ControllerBase
    {
        public IExamResultRepo ExamRepo { get; }
        public ExamResultController(IExamResultRepo examRepo)
        {
            ExamRepo = examRepo;

        }


        [HttpGet]
        [Route("generateStudentsGrades/{classid}/{subjectid}")]
        [Authorize(Roles = "Teacher")]

        public IActionResult generate(int classid,int subjectid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ExamRepo.generateStudentsGrades();
                    var result = ExamRepo.getResultsByClassRoom(classid, subjectid);
                    return Ok(result);
                }
                return BadRequest();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("SaveResults")]
        [Authorize(Roles = "Teacher")]
        public IActionResult SaveResults(List<ExamResultDTO> Result)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(ExamRepo.Edit(Result));
                }
                return BadRequest();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("upgradeStudent")]
        [Authorize(Roles = "Admin")]

        public IActionResult upgradeStudent()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ExamRepo.upgradeStudent();
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
