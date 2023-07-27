using Microsoft.AspNetCore.Mvc;
using SWCTracker.API;
namespace SWCTracker.Controllers
{


    namespace SWCTracker
    {
        public class HomeController : Controller
        {
            private readonly ISubSectorAPIClient _subSectorBL;
            public HomeController(ISubSectorAPIClient subSectorBL)
            {
                _subSectorBL = subSectorBL;
            }
            public async Task<IActionResult> Index()
            {
                var model = await _subSectorBL.GetSubSectors();
                return View(model);
            }

        }
    }

}