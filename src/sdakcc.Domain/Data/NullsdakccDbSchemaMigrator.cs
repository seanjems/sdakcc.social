using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace sdakcc.Data;

/* This is used if database provider does't define
 * IsdakccDbSchemaMigrator implementation.
 */
public class NullsdakccDbSchemaMigrator : IsdakccDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
