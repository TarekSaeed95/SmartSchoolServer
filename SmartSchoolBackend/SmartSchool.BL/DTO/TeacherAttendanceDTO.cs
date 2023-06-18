using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.DTO
{
    public class TeacherAttendanceDTO
    {
        public int Id { set; get; }

        [DataType(DataType.Date)]
        public DateTime Date { set; get; }
        public bool State { set; get; }
        public string TeacherId { set; get; }
        public string TeacherName { get; set; }
    }
}
