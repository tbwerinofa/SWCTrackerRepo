using Microsoft.AspNetCore.Mvc;
using SWCTracker.API;
namespace SWCTracker.Controllers
{


    namespace SWCTracker
    {
        public class TermsController : Controller
        {
            public IActionResult Index()
            {
                return View();
            }

            [Route("Privacy")]
            public IActionResult Privacy()
            {
                return View();
            }

            [Route("Disclaimer")]
            public IActionResult Disclaimer()
            {
                return View();
            }

            [Route("CookiePolicy")]
            public IActionResult CookiePolicy()
            {
                return View();
            }

        }
    }

}