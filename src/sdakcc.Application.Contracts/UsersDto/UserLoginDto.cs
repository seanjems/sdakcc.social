using System;
using System.Collections.Generic;
using System.Text;

namespace sdakcc.UsersDto
{
    public class UserLoginDto
    {
        public string Email { get; set; }
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }

    }
}
