
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using sdakcc.Entities;
//using System;
//using System.Collections.Generic;

//namespace sdakcc.Repositories
//{
//    public class UserManager : Microsoft.AspNetCore.Identity.UserManager<Volo.Abp.Identity.IdentityUser>
//    {

//        public UserManager(
//            IUserStore<Volo.Abp.Identity.IdentityUser> userStore,
//            IOptions<Microsoft.AspNetCore.Identity.IdentityOptions> options,
//            IPasswordHasher<Volo.Abp.Identity.IdentityUser> passwordHasher,
//            IEnumerable<IUserValidator<Volo.Abp.Identity.IdentityUser>> userValidators,
//            IEnumerable<IPasswordValidator<Volo.Abp.Identity.IdentityUser>> passwordValidators,
//            ILookupNormalizer lookupNormalizer,
//            IdentityErrorDescriber identityErrorDescriber,
//            IServiceProvider serviceProvider,
//            ILogger<UserManager<Volo.Abp.Identity.IdentityUser>> logger
//            )
//            : base(
//             userStore,
//             options,
//             passwordHasher,
//             userValidators,
//             passwordValidators,
//             lookupNormalizer,
//             identityErrorDescriber,
//             serviceProvider,
//             logger)
//        {
//        }
//    }
//}











using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using sdakcc.Entities;
using System;
using System.Collections.Generic;
using Volo.Abp.TenantManagement;

namespace sdakcc.Repositories
{
    public class UserManager : AbpUserManager<Role, AppUser>
    {
        public UserManager(
            RoleManager roleManager,
            UserStore store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<AppUser> passwordHasher,
            IEnumerable<IUserValidator<AppUser>> userValidators,
            IEnumerable<IPasswordValidator<AppUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<AppUser>> logger,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings,
            ISettingManager settingManager,
            IRepository<UserLogin, long> loginRepository)
            : base(
                roleManager,
                store,
                optionsAccessor,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errors,
                services,
                logger,
                permissionManager,
                unitOfWorkManager,
                cacheManager,
                organizationUnitRepository,
                userOrganizationUnitRepository,
                organizationUnitSettings,
                settingManager,
                loginRepository)
        {
        }
    }
}
