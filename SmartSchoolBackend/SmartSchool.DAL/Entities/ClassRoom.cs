using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.DAL.Entities
{
    public class ClassRoom
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[1-6]{1}/[1-9]{1}$", ErrorMessage = "Please Enter valid Name")]
        public string Name { get; set; }

        [Required]
        public int gradeYearId { get; set; }


        [InverseProperty("Class")]
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();


        [InverseProperty("ClassRoom")]
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();


        [ForeignKey("gradeYearId")]
        public virtual GradeYear gradeYear { get; set; }


    }
}
