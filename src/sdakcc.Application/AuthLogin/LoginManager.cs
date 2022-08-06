

namespace sdakcc.Application.AuthLogin
{

    public class LoginManager : Microsoft.AspNetCore.Identity.SignInManager<Volo.Abp.Identity.IdentityUser>
    {
        public LoginManager(
            Microsoft.AspNetCore.Identity.UserManager<Volo.Abp.Identity.IdentityUser> userManager,
            Microsoft.AspNetCore.Http.IHttpContextAccessor contextAccessor,
            Microsoft.AspNetCore.Identity.IUserClaimsPrincipalFactory<Volo.Abp.Identity.IdentityUser> claimsFactory,
            Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Identity.IdentityOptions> optionsAccessor,
            Microsoft.Extensions.Logging.ILogger<Microsoft.AspNetCore.Identity.SignInManager<Volo.Abp.Identity.IdentityUser>> logger,
            Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider schemes,
            Microsoft.AspNetCore.Identity.IUserConfirmation<Volo.Abp.Identity.IdentityUser> confirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }
    }



}
