//using Microsoft.AspNetCore.Identity;
//using Abp.Authorization;
//using Abp.Authorization.Users;
//using Abp.Configuration;
//using Abp.Configuration.Startup;
//using Abp.Dependency;
//using Abp.Domain.Repositories;
//using Abp.Domain.Uow;
//using Abp.Zero.Configuration;
//using sdakcc.Repositories;
//using sdakcc.Entities;

//namespace sdakcc.Repositories
//{
//    public class LogInManager : AbpLogInManager<Tenant, Role, AppUser>
//    {
//        public LogInManager(
//            UserManager userManager,
//            IMultiTenancyConfig multiTenancyConfig,
//            IRepository<Tenant> tenantRepository,
//            IUnitOfWorkManager unitOfWorkManager,
//            ISettingManager settingManager,
//            IRepository<UserLoginAttempt, long> userLoginAttemptRepository,
//            IUserManagementConfig userManagementConfig,
//            IIocResolver iocResolver,
//            IPasswordHasher<AppUser> passwordHasher,
//            RoleManager roleManager,
//            AbpUserClaimsPrincipalFactory<AppUser, Role> abpUserClaimsPrincipalFactory
//            )
//            : base(
//                  userManager,
//                  multiTenancyConfig,
//                  tenantRepository,
//                  unitOfWorkManager,
//                  settingManager,
//                  userLoginAttemptRepository,
//                  userManagementConfig,
//                  iocResolver,
//                  passwordHasher,
//                  roleManager,
//                  abpUserClaimsPrincipalFactory
//                  )
//        {
//        }
//    }
//}


namespace sdakcc.Repositories
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
