using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SmartSchool.DAL.Entities;
using SmartSchool.DAL.OurEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartSchool.BL.DTO
{
    public class StudentDTO
    {

        [StringLength(50)]
        public string Id { get; set; }

        [Required]
        [StringLength(70)]
     //   [RegularExpression("^[a-zA-Z]{3,}$", ErrorMessage = "Please Enter valid Name")]
        public string StudentFirstName { get; set; }


        [Required]
        [EnumDataType(typeof(Gender), ErrorMessage = "Choose valid Gender")]
        public string? Gender { get; set; }


        [Required]
        [StringLength(11)]
        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "Please Enter valid Phone number")]
        public string StudentPhone { get; set; }



        [Required]
        [DataType(DataType.Date, ErrorMessage = "Please Enter valid Date")]
        public DateTime StudentBirthDate { get; set; }



        [Required]
        [StringLength(50)]
        [MinLength(5)]
        [MaxLength(50)]
        public string Address { get; set; }



      //  [Required]
        public string? StudentPhoto { set; get; }


        public string? StudentPhotoUrl { set; get; }

       // [Required]
        public string? StudentBirthCertPhoto { set; get; }

      
        public string? StudentBirthCertPhotoUrl { set; get; }


        [RegularExpression("^[1-9][0-9]{1,}$")]
        public int? MaxDayOff { get; set; }


        [RegularExpression("^[0-9]{1,}$")]
        public int? AbsenceDays { get; set; }

        public bool Fees { get; set; }

        public long? GradePrice { get; set; }

        [StringLength(50)]
        public string ParentID { get; set; }

        public int? ClassRoomID { get; set; }

        public string? ClassRoomName { get; set; }

    }
}
