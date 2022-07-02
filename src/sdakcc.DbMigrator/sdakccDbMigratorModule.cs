using sdakcc.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace sdakcc.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(sdakccEntityFrameworkCoreModule),
    typeof(sdakccApplicationContractsModule)
    )]
public class sdakccDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
