using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sdakcc.Data;
using Volo.Abp.DependencyInjection;

namespace sdakcc.EntityFrameworkCore;

public class EntityFrameworkCoresdakccDbSchemaMigrator
    : IsdakccDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoresdakccDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the sdakccDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<sdakccDbContext>()
            .Database
            .MigrateAsync();
    }
}
