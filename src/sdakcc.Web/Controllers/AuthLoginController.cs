using Abp.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using sdakcc.Web.Models;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace sdakcc.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthLoginController : AbpControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthLoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult AuthLogin([FromBody] UserLoginInput userLogin)
        {
            var user = Authenticate(userLogin);
            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("Usernot found");
        }

        private string Generate(UserLoginInput user)
        {
            throw new NotImplementedException();
        }

        private UserLoginResult Authenticate(UserLoginInput userLoginInput)
        {
            throw new NotImplementedException();
        }

        #region External Auth Todo:
        //[HttpGet]
        //public List<ExternalLoginProviderInfoModel> GetExternalAuthenticationProviders()
        //{
        //    return ObjectMapper.Map<List<ExternalLoginProviderInfoModel>>(_externalAuthConfiguration.Providers);
        //}

        //[HttpPost]
        //public async Task<ExternalAuthenticateResultModel> ExternalAuthenticate([FromBody] ExternalAuthenticateModel model)
        //{
        //    var externalUser = await GetExternalUserInfo(model);

        //    var loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, model.ProviderKey, model.AuthProvider), GetTenancyNameOrNull());

        //    switch (loginResult.Result)
        //    {
        //        case AbpLoginResultType.Success:
        //            {
        //                var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
        //                return new ExternalAuthenticateResultModel
        //                {
        //                    AccessToken = accessToken,
        //                    EncryptedAccessToken = GetEncryptedAccessToken(accessToken),
        //                    ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds
        //                };
        //            }
        //        case AbpLoginResultType.UnknownExternalLogin:
        //            {
        //                var newUser = await RegisterExternalUserAsync(externalUser);
        //                if (!newUser.IsActive)
        //                {
        //                    return new ExternalAuthenticateResultModel
        //                    {
        //                        WaitingForActivation = true
        //                    };
        //                }

        //                // Try to login again with newly registered user!
        //                loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, model.ProviderKey, model.AuthProvider), GetTenancyNameOrNull());
        //                if (loginResult.Result != AbpLoginResultType.Success)
        //                {
        //                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
        //                        loginResult.Result,
        //                        model.ProviderKey,
        //                        GetTenancyNameOrNull()
        //                    );
        //                }

        //                return new ExternalAuthenticateResultModel
        //                {
        //                    AccessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity)),
        //                    ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds
        //                };
        //            }
        //        default:
        //            {
        //                throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
        //                    loginResult.Result,
        //                    model.ProviderKey,
        //                    GetTenancyNameOrNull()
        //                );
        //            }
        //    }
        //}

        //private async Task<User> RegisterExternalUserAsync(ExternalAuthUserInfo externalUser)
        //{
        //    var user = await _userRegistrationManager.RegisterAsync(
        //        externalUser.Name,
        //        externalUser.Surname,
        //        externalUser.EmailAddress,
        //        externalUser.EmailAddress,
        //        Authorization.Users.User.CreateRandomPassword(),
        //        true
        //    );

        //    user.Logins = new List<UserLogin>
        //    {
        //        new UserLogin
        //        {
        //            LoginProvider = externalUser.Provider,
        //            ProviderKey = externalUser.ProviderKey,
        //            TenantId = user.TenantId
        //        }
        //    };

        //    await CurrentUnitOfWork.SaveChangesAsync();

        //    return user;
        //}

        //private async Task<ExternalAuthUserInfo> GetExternalUserInfo(ExternalAuthenticateModel model)
        //{
        //    var userInfo = await _externalAuthManager.GetUserInfo(model.AuthProvider, model.ProviderAccessCode);
        //    if (userInfo.ProviderKey != model.ProviderKey)
        //    {
        //        throw new UserFriendlyException(L("CouldNotValidateExternalUser"));
        //    }

        //    return userInfo;
        //}
        #endregion


        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

        private string GetEncryptedAccessToken(string accessToken)
    }
}
