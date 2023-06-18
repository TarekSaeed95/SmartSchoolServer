using Microsoft.AspNetCore.Http;
using SmartSchool.DAL.OurEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.DAL.Entities
{
    public class Request
    {
        [Key]
        public int id { set; get; }

        [Required]
        [StringLength(15)]
        [RegularExpression("^[a-zA-Z]{3,}$", ErrorMessage = "Please Enter valid Name")]
        public string StudentFirstName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage = "Please Enter valid Email Address")]
        public string StudentEmail { get; set; }

        [Required]
        [EnumDataType(typeof(Gender),ErrorMessage ="Choose valid Gender")]
        public Gender StudentGender { get; set; }

        [Required]
        [StringLength(11)]
        [RegularExpression("^01[0125][0-9]{8}$",ErrorMessage = "Please Enter valid Phone number")]
        public string StudentPhone { get; set; }

        [Required]
        [DataType(DataType.Date,ErrorMessage = "Please Enter valid Date")]
        public DateTime StudentBirthDate { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(5)]
        [MaxLength(50)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z]{2,15}\\s([a-zA-Z]{2,15}\\s)[a-zA-Z]{2,15}$",ErrorMessage ="Please Enter at least 3 Names")]
        public string ParentFullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please Enter valid Email Address")]
        public string ParentEmail { get; set; }

        [Required]
        [StringLength(11)]
        [RegularExpression("^01[0125][0-9]{8}$",ErrorMessage = "Please Enter valid Phone number")]
        public string ParentPhone { get; set; }

        [Required]
        public string StudentPhotoUrl { set; get; }

        [Required]
        public string StudentBirthCertPhotoUrl { set; get; }

        [Required]
        public string IdentityParentPhotoUrl { set; get; }

        [Required]  
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password must contain at least (8 charachters, 1 Special Charachter, 1 UpperCase, 1 LowerCase, 1 Digit")]
        public string Password { set; get; }
    }
}
