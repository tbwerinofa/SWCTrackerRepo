using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SWCTracker.API;

namespace SWCTracker.Controllers
{
    [ViewComponentAttribute]
    public class TaskGrade : ViewComponent
    {
        private readonly ITaskGradeAPIClient _clientBL;
        public TaskGrade(ITaskGradeAPIClient clientBL)
        {
            _clientBL =clientBL;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var model = await _clientBL.GetTaskGrades(id);

            return View(model);
        }
    }
}
