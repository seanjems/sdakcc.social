using sdakcc.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace sdakcc.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class sdakccController : AbpControllerBase
{
    protected sdakccController()
    {
        LocalizationResource = typeof(sdakccResource);
    }
}
