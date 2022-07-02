using System;
using System.Collections.Generic;
using System.Text;
using sdakcc.Localization;
using Volo.Abp.Application.Services;

namespace sdakcc;

/* Inherit your application services from this class.
 */
public abstract class sdakccAppService : ApplicationService
{
    protected sdakccAppService()
    {
        LocalizationResource = typeof(sdakccResource);
    }
}
