using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartSchool.DAL.OurEnums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace SmartSchool.DAL.Entities
{
    public class Student
    {
        [Key]
        [StringLength(50)]
        public string Id { get; set; }

        [Required]
        [StringLength(70)]
      //  [RegularExpression("^[a-zA-Z]{3,}$", ErrorMessage = "Please Enter valid Name")]
        public string StudentFirstName { get; set; }

        [Required]
        [EnumDataType(typeof(Gender), ErrorMessage = "Choose valid Gender")]
        public Gender? Gender { get; set; }

        [Required]
        [StringLength(11)]
        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "Please Enter valid Phone number")]
        public string StudentPhone { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Please Enter valid Date")]
        public DateTime StudentBirthDate { get; set; }


        [Required]
        [StringLength(50,MinimumLength =5)]
        public string Address { get; set; }

        [Required]
        public string StudentPhotoUrl { set; get; }

        [Required]
        public string StudentBirthCertPhotoUrl { set; get; }


        [RegularExpression("^[1-9][0-9]{1,}$")]
        public int? MaxDayOff { get; set; } = 15;


        [RegularExpression("^[0-9]{1,}$")]
        public int? AbsenceDays { get; set; } = 0;

        public bool Fees { get; set; } 


        [StringLength(50)]
        public string ParentID { get; set; }


        [ForeignKey("ParentID")]
        [InverseProperty("Students")]
        public virtual Parent Parent { get; set; }


        [ForeignKey("IdentityUser")]
        public string IdentityUserId { set; get; }
        public virtual IdentityUser IdentityUser { set; get; }

        public int? ClassRoomID { get; set; }


        [ForeignKey("ClassRoomID")]
        public virtual ClassRoom ClassRoom { get; set; }

        public virtual ICollection<StudentAttendance> Attendances { get; set; } = new List<StudentAttendance>();

        public virtual ICollection<ExamResult> ExamsResults { get; set; } = new List<ExamResult>();

        public virtual ICollection<PreviousExamResult> PreviousExamResults { get; set; } = new List<PreviousExamResult>();


    }
}
