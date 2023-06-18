using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.DAL.Entities
{
    public class Session
    {
        [Key]
        public int Id { get; set; }

        [Range(1,6)]
        public int SessionNo { get; set; }
        
        [Required]
        public int ScheduleID { get; set; }

        [ForeignKey("ScheduleID")]
        public virtual Schedule? Schedule { get; set; }
        
        [Required]
        public string? TeacherID { get; set; }

        [ForeignKey("TeacherID")]
        public virtual Teacher? Teacher { get; set; }
    }
}
