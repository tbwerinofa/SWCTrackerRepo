using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SWCTracker.API;

namespace SWCTracker.Controllers
{
    public class GradeController : Controller
    {
        private readonly ITaskGradeAPIClient _clientBL;
        public GradeController(ITaskGradeAPIClient clientBL)
        {
            _clientBL = clientBL;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var model = await _clientBL.GetWageRateByGradeAsync(1,id);
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
