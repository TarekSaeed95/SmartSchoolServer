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
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15,MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[1-9][0-9]{1,}$")]
        public int TotalMark { get; set; }

        [Required]       
        public int GradeYearId { get; set; }

        [ForeignKey("GradeYearId")]
        [InverseProperty("Subjects")]
        public virtual GradeYear GradeYear { get; set; }

        public virtual ICollection<ExamResult> ExamsResults { get; set; } = new List<ExamResult>();
        public virtual ICollection<PreviousExamResult> PreviousExamResults { get; set; } = new List<PreviousExamResult>();

        [InverseProperty("Subject")]
        public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
        public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
    }
}
