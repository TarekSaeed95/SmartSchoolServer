using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.DTO
{
    public class StudentAttendanceDTO
    {
        public int id { set; get; }

        [DataType(DataType.Date)]
        public DateTime date { set; get; }
        public bool state { set; get; }

        public string studentId { set; get; }

        public string studentName { get; set; }
    }
}
