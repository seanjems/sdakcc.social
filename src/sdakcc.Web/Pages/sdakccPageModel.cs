using sdakcc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace sdakcc.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class sdakccPageModel : AbpPageModel
{
    protected sdakccPageModel()
    {
        LocalizationResourceType = typeof(sdakccResource);
    }
}
