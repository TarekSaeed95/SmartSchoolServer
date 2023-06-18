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
    public class ExamResultDTO
    {
        public int Id { set; get; }
        public int FirstTermGrade { set; get; } = 0;

        public int SecondTermGrade { set; get; } = 0;

        public int Total { get; set; } = 0;

        public int Year { get; set; }
        public int? TotalSubjectMark { get; set; }
        public string StudentId { set; get; }

        public string? StudentName { set; get; }

        public int SubjectId { set; get; }

        public string? SubjectName { set; get; }

        public string? ClassRoomName { set; get; }

        public bool? Passed { get; set; } 



    }
}
