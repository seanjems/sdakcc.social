using Microsoft.EntityFrameworkCore;
using sdakcc.Entities;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace sdakcc.EntityFrameworkCore;

public static class sdakccEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        sdakccGlobalFeatureConfigurator.Configure();
        sdakccModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
            /* You can configure extra properties for the
             * entities defined in the modules used by your application.
             *
             * This class can be used to map these extra properties to table fields in the database.
             *
             * USE THIS CLASS ONLY TO CONFIGURE EF CORE RELATED MAPPING.
             * USE sdakccModuleExtensionConfigurator CLASS (in the Domain.Shared project)
             * FOR A HIGH LEVEL API TO DEFINE EXTRA PROPERTIES TO ENTITIES OF THE USED MODULES
             *
             * Example: Map a property to a table field:

                 ObjectExtensionManager.Instance
                     .MapEfCoreProperty<IdentityUser, string>(
                         "MyProperty",
                         (entityBuilder, propertyBuilder) =>
                         {
                             propertyBuilder.HasMaxLength(128);
                         }
                     );

             * See the documentation for more:
             * https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Extending-Entities
             */

            ObjectExtensionManager.Instance
                     .MapEfCoreProperty<IdentityUser, string>(
                         nameof(AppUser.Aboutme),
                         (entityBuilder, propertyBuilder) =>
                         {
                             propertyBuilder.HasMaxLength(200);
                         }
                     )
                    .MapEfCoreProperty<IdentityUser, string>(
                         nameof(AppUser.Address),
                         (entityBuilder, propertyBuilder) =>
                         {
                             propertyBuilder.HasMaxLength(200);
                         }
                     )
                    .MapEfCoreProperty<IdentityUser, string>(
                         nameof(AppUser.Profession),
                         (entityBuilder, propertyBuilder) =>
                         {
                             propertyBuilder.HasMaxLength(50);
                         }
                     )
                    .MapEfCoreProperty<IdentityUser, string>(
                         nameof(AppUser.Contacts),
                         (entityBuilder, propertyBuilder) =>
                         {
                             propertyBuilder.HasMaxLength(30);
                         }
                     )
                    .MapEfCoreProperty<IdentityUser, string>(
                         nameof(AppUser.LocalChurch),
                         (entityBuilder, propertyBuilder) =>
                         {
                             propertyBuilder.HasMaxLength(200);
                         }
                     )
                    .MapEfCoreProperty<IdentityUser, string>(
                         nameof(AppUser.FavouriteVerse),
                         (entityBuilder, propertyBuilder) =>
                         {
                             propertyBuilder.HasMaxLength(200);
                         }
                     )
                    .MapEfCoreProperty<IdentityUser, string>(
                         nameof(AppUser.Followers),
                         (entityBuilder, propertyBuilder) =>
                         {

                         }
                     )
                    .MapEfCoreProperty<IdentityUser, string>(
                         nameof(AppUser.Family),
                         (entityBuilder, propertyBuilder) =>
                         {
                             propertyBuilder.HasMaxLength(200);
                         }
                     )
                    .MapEfCoreProperty<IdentityUser, string>(
                         nameof(AppUser.Relationship),
                         (entityBuilder, propertyBuilder) =>
                         {
                             propertyBuilder.HasMaxLength(100);
                         }
                     );
                    

        });
    }
}
