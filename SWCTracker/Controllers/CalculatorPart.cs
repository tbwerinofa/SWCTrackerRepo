using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SWCTracker.API;

namespace SWCTracker.Controllers
{
    [ViewComponentAttribute]
    public class CalculatorPart : ViewComponent
    {
        private readonly ITaskGradeAPIClient _clientBL;
        public CalculatorPart(ITaskGradeAPIClient clientBL)
        {
            _clientBL =clientBL;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var model = await _clientBL.GetCalculator(id);

            return View(model);
        }
    }
}
