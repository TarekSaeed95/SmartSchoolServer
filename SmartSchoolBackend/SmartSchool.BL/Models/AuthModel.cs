using System;
using System.Collections.Generic;

namespace SmartSchool.BL.Models
{
    public class AuthModel
    {
        public string Message { get; set; }
        //hteb2a true lma ye3mel login
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

        //l token <3
        public string Token { get; set; }
        public DateTime ExpireOn { get; set; }

    }
}