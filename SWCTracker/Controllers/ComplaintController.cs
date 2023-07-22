using Microsoft.AspNetCore.Mvc;
using SWCTracker.API;
using SWCTracker.Models;

namespace SWCTracker.Controllers
{
    public class ComplaintController : Controller
    {
        private readonly IComplaintAPIClient _modelBL;
        public ComplaintController(IComplaintAPIClient modelBL)
        {
            _modelBL = modelBL;
        }
        public IActionResult Index()
        {
            ComplaintViewModel model =  _modelBL.GetModel();
            return View(model);
        }
    }
}
