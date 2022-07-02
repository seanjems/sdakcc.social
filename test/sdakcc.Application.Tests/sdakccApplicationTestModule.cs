using Volo.Abp.Modularity;

namespace sdakcc;

[DependsOn(
    typeof(sdakccApplicationModule),
    typeof(sdakccDomainTestModule)
    )]
public class sdakccApplicationTestModule : AbpModule
{

}
