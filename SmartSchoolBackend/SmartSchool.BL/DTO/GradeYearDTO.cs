using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.DTO
{
    public class GradeYearDTO
    {
      
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [MinLength(3)]
        [MaxLength(15)]

        public string Name { get; set; }

        [Required]
        [RegularExpression("^[0-9]{2,}$", ErrorMessage = "Please Enter valid fees")]
        public decimal Fees { get; set; }

       

    }
}
