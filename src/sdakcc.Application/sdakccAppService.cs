using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using sdakcc.Entities;
using sdakcc.Localization;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

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
