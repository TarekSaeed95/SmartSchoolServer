using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using SmartSchool.DAL.OurEnums;
using System.Text.Json.Serialization;

namespace SmartSchool.BL.DTO
{
    public class TeacherEditDTO
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z]{2,15}\\s([a-zA-Z]{2,15}\\s)[a-zA-Z]{2,15}$", ErrorMessage = "Please Enter at least 3 Names")]
        public string FullName { set; get; }

        [EnumDataType(typeof(Gender), ErrorMessage = "Choose valid Gender")]
		public string Gender { get; set; }

        [Required]
        [RegularExpression("^[1-9][0-9]{1,}$")]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Salary { get; set; }

        [Required]
        [StringLength(11)]
        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "Please Enter valid Phone number")]
        public string? Phone { get; set; }

        [Required]
        [StringLength(50,MinimumLength =5)]
		public string? Address { get; set; }

        public string? PhotoUrl { set; get; }
        public string? Photo { set; get; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime HireDate { set; get; }


        [RegularExpression("^[0-9]{1,3}$")]
        [Required]
		public int MaxDayOff { get; set; }
		
        [RegularExpression("^[0-9]{1,3}$")]
		public int AbsenceDays { get; set; }


     
    }
}
