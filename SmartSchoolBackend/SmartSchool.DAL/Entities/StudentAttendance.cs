using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.DAL.Entities
{
    public class StudentAttendance
    {
        [Key]
        public int id { set; get; }

        [DataType(DataType.Date)]
        public DateTime date { set; get; }
        public bool state { set; get; }

        [ForeignKey("student")]
        public string studentId { set; get; }

        public virtual Student? student { get; set; }
    }
}
