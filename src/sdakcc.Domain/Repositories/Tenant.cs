using Abp.MultiTenancy;
using sdakcc.Entities;

namespace sdakcc.Repositories
{
    public class Tenant : AbpTenant<AppUser>
    {
        public Tenant()
        {
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
