using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.DAL.Entities
{
    public class TeacherAttendance
    {
        [Key] 
        public int Id { set; get; }

        [DataType(DataType.Date)]
        public DateTime Date { set; get; }
        public bool State { set; get; }

        [ForeignKey("Teacher")]
        public string TeacherId{ set; get; }

        public virtual Teacher? Teacher{ get; set; }
    }
}
