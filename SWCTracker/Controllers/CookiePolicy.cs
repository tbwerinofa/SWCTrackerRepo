using Microsoft.AspNetCore.Mvc;

namespace SWCTracker.Controllers
{
    public class CookiePolicy : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
