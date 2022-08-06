﻿using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
using sdakcc.Application.AuthLogin;
using Abp.Domain.Repositories;
using Abp.Authorization.Users;
using Volo.Abp.Identity;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace sdakcc.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthorizationController : Controller
    {
        private readonly IdentityUserManager _userManager;
        private readonly LoginManager _loginManager;
        private readonly ISigningCredentialStore _signingCredentialStore;
        private readonly IConfiguration _configuration;

        
        public AuthorizationController(IdentityUserManager userManager, LoginManager loginManager, ISigningCredentialStore signingCredentialStore, IConfiguration configuration)
        {
            _userManager = userManager;
            _signingCredentialStore = signingCredentialStore ?? throw new ArgumentNullException(nameof(signingCredentialStore));
            _configuration = configuration;
            _loginManager = loginManager;
        }

        [HttpPost]
        public async Task<ActionResult<UserLoginResultDto>> CustomLogin(UserLoginDto credentials)
        {

            var user = await GetCustomValidatedUserAsync(credentials);
            if (user == null) return NotFound("Invalid Userame and or password");

            var signingCredentials = await _signingCredentialStore.GetSigningCredentialsAsync();
            var tokenHandler = new JwtSecurityTokenHandler();



            var ValidIssuer = _configuration["Jwt:Issuer"]; // jwtSetting.Issuer,
            var ValidAudience = _configuration["Jwt:Audience"]; // jwtSetting.Audience,

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("user", user.ToString()) /* extra custom claims */ }),
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

        private async Task<IdentityUser> GetCustomValidatedUserAsync(UserLoginDto credentials)
        {
            
            var loginResult = await _loginManager.PasswordSignInAsync(credentials.Email, credentials.PassWord, credentials.RememberMe, false);

            if (loginResult.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(credentials.Email);
                return user;
            }
            return null;
        }
    }
}
