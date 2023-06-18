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
    public class ParentDTO
    {
       
        [StringLength(50)]
        public string? Id { get; set; }


        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z]{2,15}\\s([a-zA-Z]{2,15}\\s)[a-zA-Z]{2,15}$", ErrorMessage = "Please Enter at least 3 Names")]
        public string ParentFullName { get; set; }

        [Required]
        [StringLength(11)]
        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "Please Enter valid Phone number")]
        public string ParentPhone { get; set; }


        public string? IdentityParentPhotoUrl { set; get; }

        [Required]
        public string IdentityParentPhoto { set; get; }

    }
}
