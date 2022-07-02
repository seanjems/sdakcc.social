using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace sdakcc.Web;

[Dependency(ReplaceServices = true)]
public class sdakccBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "sdakcc";
}
