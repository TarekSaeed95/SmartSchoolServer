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
    public class SubjectDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15,MinimumLength = 3)]
        public string Name { get; set; }
        
        [Required]
        [RegularExpression("^[1-9]{1}[0-9]{1,}$")]
        public int TotalMark { get; set; }

		[Required]
        public int GradeYearId { get; set; }
        public string? GradeYearName { get; set; }


	}
}
