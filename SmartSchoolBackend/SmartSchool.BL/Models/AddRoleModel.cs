using System.ComponentModel.DataAnnotations;

namespace SmartSchool.BL.Models
{
    public class AddRoleModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Role { get; set; }

    }
}
