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
    public class Parent
    {
        [Key]
        [StringLength(50)]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z]{2,15}\\s([a-zA-Z]{2,15}\\s)[a-zA-Z]{2,15}$", ErrorMessage = "Please Enter at least 3 Names")]
        public string ParentFullName { get; set; }

        [Required]
        [StringLength(11)]
        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "Please Enter valid Phone number")]
        public string ParentPhone { get; set; }


        [Required]
        public string IdentityParentPhotoUrl { set; get; }


        [ForeignKey("IdentityUser")]
        public string IdentityUserId { set; get; }

        public virtual IdentityUser IdentityUser { set; get; }


        [InverseProperty("Parent")]
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
    }
}
