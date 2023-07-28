using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCTracker.API;

namespace SWCTracker.Controllers
{
    public class WorkingConditionController : Controller
    {
        private readonly ITaskGradeAPIClient _clientBL;
        public WorkingConditionController(ITaskGradeAPIClient clientBL)
        {
            _clientBL = clientBL;
        }

        public async Task<ActionResult> Index(int id=1)
        {
            var model = await _clientBL.GetTaskGrades(id);

            return View(model);
        }
    }
}
