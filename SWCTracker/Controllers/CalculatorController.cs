using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SWCTracker.API;
using BusinessObject;

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
            CalculatorViewModel dataset = await _clientBL.GetCalculator(model.SectorId, model.OccupationGroupId);
            model.OccupationGroups = dataset.OccupationGroups;
            model.Occupations = dataset.Occupations;
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

        public async Task<string> GetWageRateAsync(
          int sectorId,
          int occupationId)
        {
            var selectListItem = await _clientBL.GetWageRateAsync(sectorId, occupationId);

            return JsonConvert.SerializeObject(new
            {
                Response = selectListItem
            });
        }
    }
}
