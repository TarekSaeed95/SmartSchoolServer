using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.DTO
{
    public class ScheduleDTO
    {
        
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime Day { get; set; }

        [Required]
        public int ClassId { get; set; }

        public string? ClassRoomName { set; get; }

    }
}
