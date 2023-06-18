using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.DTO
{
    public class ClassRoomDTO
    {

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[1-6]{1}/[1-9]{1}$", ErrorMessage = "Please Enter valid Name")]
        public string Name { get; set; }

        [Required]
        public int gradeYearId { get; set; }

        public string? GradeYearName { set; get; }

       
    }
}
