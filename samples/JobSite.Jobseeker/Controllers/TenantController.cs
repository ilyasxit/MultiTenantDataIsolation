using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobSite.Jobseeker.Controllers
{
    [AllowAnonymous]
    public class TenantController : Controller
    {
        public IActionResult Index()
        {
            return View(HttpContext.GetMultiTenantContext()?.TenantInfo);
        }
    }
}
