using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SWCTracker.API;
using SWCTracker.Models;

namespace SWCTracker.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ITaskGradeAPIClient _clientBL;
        public CalculatorController(ITaskGradeAPIClient clientBL)
        {
            _clientBL = clientBL;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int id =1)
        {
            var model = await _clientBL.GetCalculator(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CalculatorViewModel model)
        {
            CalculatorViewModel dataset = await _clientBL.GetCalculator(model.Id,model.OccupationGroupId,model.OccupationId);
            model.OccupationGroups = dataset.OccupationGroups;
            model.Occupations = dataset.Occupations;
            model.WageRate = dataset.WageRate;
            model.IsPostBack =true;
            return View(model);
        }

        public async Task<string> GetOccupationSelectListItem_ByParentId(
         int parentId)
        {
            var selectListItem = await _clientBL.GetOccupationSelectListItem_ByParentId(1,parentId);

            return JsonConvert.SerializeObject(new
            {
                Response = selectListItem
            });
        }
    }
}
