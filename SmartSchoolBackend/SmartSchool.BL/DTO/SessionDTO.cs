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
    public class SessionDTO
    {
        public int Id { get; set; }

        [Range(1, 6)]
        public int SessionNo { get; set; }

        [Required]
        public int ScheduleID { get; set; }

        [Required]
        public string TeacherID { get; set; }
        public string? SubjectName { get; set; }
        public string? TeacherName { get; set; }
        public string? ScheduleDay { get; set; }
        public string? ClassRoomName { get; set; }
    }
}
