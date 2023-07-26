using Microsoft.AspNetCore.Mvc;
using SWCTracker.API;

namespace SWCTracker.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ISubSectorAPIClient _subSectorBL;
        public CalculatorController(ISubSectorAPIClient subSectorBL)
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
