using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace sdakcc.UsersDto
{
    public class UserLoginDto
    {
        [Required]
        public string userNameOrEmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;

    }
}
