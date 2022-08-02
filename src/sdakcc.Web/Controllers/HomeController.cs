using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace sdakcc.Web.Controllers
{
    public class HomeController : AbpControllerBase
    {
        
        public HomeController()
        {
           
        }

        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
