//using IdentityServer4.Stores;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Http;
//using Microsoft.IdentityModel.Tokens;
//using sdakcc.AuthLogin;
//using sdakcc.UsersDto;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using static Volo.Abp.Identity.IdentityPermissions;
//using sdakcc.Application.AuthLogin;
//using Abp.Domain.Repositories;
//using Volo.Abp.Identity;
//using sdakcc.Entities;

//namespace sdakcc.Web.Controllers
//{

//    public class AuthorizationAppService : sdakccAppService
//    {
//        private readonly IdentityUserManager _userManager;
//        private readonly LoginManager _loginManager;
//        private readonly ISigningCredentialStore _signingCredentialStore;
//        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;


//        public AuthorizationAppService(IdentityUserManager userManager, LoginManager loginManager, ISigningCredentialStore signingCredentialStore
//           , Microsoft.Extensions.Configuration.IConfiguration configuration
//            )
//        {
//            _userManager = userManager;
//            _signingCredentialStore = signingCredentialStore ?? throw new ArgumentNullException(nameof(signingCredentialStore));
//            _configuration = configuration;
//            _loginManager = loginManager;
//        }


//        public async Task<UserLoginResultDto> CustomLogin(UserLoginDto credentials)
//        {

//            var user = await GetCustomValidatedUserAsync(credentials);
//            if (user == null) return null;

//            var signingCredentials = await _signingCredentialStore.GetSigningCredentialsAsync();
//            var tokenHandler = new JwtSecurityTokenHandler();



//            //var ValidIssuer = "https://localhost:44361" ;_configuration["Jwt:Issuer"]; // jwtSetting.Issuer,
//            //var ValidAudience = _configuration["Jwt:Audience"]; // jwtSetting.Audience,

//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new[] { new Claim("user", user.ToString()) /* extra custom claims */ }),
//                Issuer = "https://localhost:44361", // ValidIssuer,
//                IssuedAt = DateTime.UtcNow,
//                Audience = "https://localhost:44361",
//                Expires = DateTime.UtcNow.AddDays(1),
//                SigningCredentials = signingCredentials
//            };
//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            var outputToken = new UserLoginResultDto()
//            {
//                Token = tokenHandler.WriteToken(token)
//            };
//            return outputToken;

//        }

//        private async Task<IdentityUser> GetCustomValidatedUserAsync(UserLoginDto credentials)
//        {

//            var loginResult = await _loginManager.PasswordSignInAsync(credentials.Email, credentials.PassWord, credentials.RememberMe, false);

//            if (loginResult.Succeeded)
//            {
//                var user = await _userManager.FindByEmailAsync(credentials.Email);
//                return user;
//            }
//            return null;
//        }
//    }
//}
