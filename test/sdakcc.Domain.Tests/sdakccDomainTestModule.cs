using sdakcc.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace sdakcc;

[DependsOn(
    typeof(sdakccEntityFrameworkCoreTestModule)
    )]
public class sdakccDomainTestModule : AbpModule
{

}
