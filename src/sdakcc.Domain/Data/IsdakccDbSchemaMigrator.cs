using System.Threading.Tasks;

namespace sdakcc.Data;

public interface IsdakccDbSchemaMigrator
{
    Task MigrateAsync();
}
