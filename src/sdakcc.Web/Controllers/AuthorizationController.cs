using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using sdakcc.Entities;
using sdakcc.UsersDto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using static Volo.Abp.Identity.IdentityPermissions;
using Abp.Domain.Repositories;
using Abp.Authorization.Users;
using Volo.Abp.Identity;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using Newtonsoft.Json;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;
using System.Web.Http.Filters;
using sdakcc.Repositories;

namespace sdakcc.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthorizationController : Controller
    {
       // private readonly IdentityUserManager _userManager;
        private readonly UserManager _userManager;
       private readonly LogInManager _loginManager;
        private readonly ISigningCredentialStore _signingCredentialStore;
        private readonly IConfiguration _configuration;


        public AuthorizationController( UserManager userManager, LogInManager loginManager, ISigningCredentialStore signingCredentialStore, IConfiguration configuration)
        {
            _userManager = userManager;
           // _userManager2 = userManager2;
            //this.userManager1 = userManager1; 
            _signingCredentialStore = signingCredentialStore ?? throw new ArgumentNullException(nameof(signingCredentialStore));
            _configuration = configuration;
            _loginManager = loginManager;
        }

        

            [HttpPost]
        public async Task<ActionResult<UserLoginResultDto>> CustomLogin([FromBody]UserLoginDto credentials)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var user = await GetCustomValidatedUserAsync(credentials);
            if (user == null) return NotFound("Invalid Userame and or password");

            var signingCredentials = await _signingCredentialStore.GetSigningCredentialsAsync();
            var tokenHandler = new JwtSecurityTokenHandler();

            var ValidIssuer = _configuration["Jwt:Issuer"]; // jwtSetting.Issuer,
            var ValidAudience = _configuration["Jwt:Audience"]; // jwtSetting.Audience,

            var userOutput = new UserClaimsDto()
            {
                Email = user.EmailAddress,
                FirstName = user.Name,
                LastName = user.Surname,
                FullName = $"{user.Name} {user.Surname}",
                TenantId = user.TenantId,
                UserId = user.Id,
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("user", JsonConvert.SerializeObject(userOutput).ToString()) /* extra custom claims */ }),
                Issuer = ValidIssuer,
                IssuedAt = DateTime.UtcNow,
                Audience = ValidAudience,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = signingCredentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var outputToken = new UserLoginResultDto()
            {
                Token = tokenHandler.WriteToken(token)
            };
            return Ok(outputToken);

        }

      
        private async Task<AppUser> GetCustomValidatedUserAsync(UserLoginDto credentials)
        {
            var user = await _userManager.FindByEmailAsync(credentials.userNameOrEmailAddress);
            if (user != null)
            {

                var loginResult = await _loginManager.LoginAsync(credentials.userNameOrEmailAddress, credentials.Password,null, false);

                return loginResult.User;
            }
            return null;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromBody]CreateUserDto credentials)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userManager.FindByEmailAsync(credentials.EmailAddress);

            if (user != null) return BadRequest("User already exists");
            var newUser = new AppUser()
            {
                Name = credentials.Name,
                EmailAddress = credentials.EmailAddress,
                UserName = credentials.UserName,
                Surname = credentials.Surname

            };

           var result = await _userManager.CreateAsync(newUser, credentials.Password);
         
            if (result.Succeeded) 
            {
                return CreatedAtRoute(null, newUser);
            }
            
            return BadRequest(result.Errors);
        }
    }
}
