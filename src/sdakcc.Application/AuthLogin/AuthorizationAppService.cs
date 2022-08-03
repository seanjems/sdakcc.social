using Microsoft.AspNetCore.Identity;
using sdakcc.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdakcc.AuthLogin
{
    public class AuthorizationAppService : sdakccAppService
    {
        private readonly UserManager<AppUser> _userManager;

         public AuthorizationAppService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;       
        }

        public string 
    }
}
