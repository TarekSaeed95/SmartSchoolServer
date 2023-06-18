using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime Day { get; set; }
        
        [Required]
        public int ClassId { get; set; }

        [ForeignKey("ClassId")]
        public virtual ClassRoom Class { get; set; }

        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();       
        
    }
}
