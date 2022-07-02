using sdakcc.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace sdakcc.Permissions;

public class sdakccPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(sdakccPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(sdakccPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<sdakccResource>(name);
    }
}
