using System.ComponentModel.DataAnnotations;

namespace SmartSchool.BL.Models
{
    //act like a viewmodel
    public class RegisterModel
    {

        //[Required, StringLength(100)]
        //public string FirstName { get; set; }
        //[Required, StringLength(100)]
        //public string LastName { get; set; }
        //[Required, StringLength(100)]
        public string Username { get; set; }
        [Required, StringLength(128)]
        public string Email { get; set; }
        [Required, StringLength(256)]
        public string Password { get; set; } 

        public string myRole { set; get; }

    }
}
