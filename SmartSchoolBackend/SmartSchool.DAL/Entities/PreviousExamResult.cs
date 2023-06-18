using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.DAL.Entities
{
    public class PreviousExamResult
    {
        [Key]
        public int Id { set; get; }

        public int FirstTermGrade { set; get; } = 0;

        public int SecondTermGrade { set; get; } = 0;

        public int Total { get; set; } = 0;
        public int Year { get; set; }

        public bool Passed { get; set; } = false;


        [ForeignKey("Student")]
        public string StudentId { set; get; }

        public virtual Student? Student { set; get; }

        [ForeignKey("Subject")]
        public int SubjectId { set; get; }

        public virtual Subject? Subject { set; get; }

    }
}
