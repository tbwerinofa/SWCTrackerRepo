using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        public async Task<IActionResult> Index()
        {
            ComplaintViewModel model =  await _modelBL.GetModelAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SaveModel(ComplaintViewModel viewModel)
        {

            SaveResult resultSet = new SaveResult();

            if (viewModel.ComplaintDetail.ProcessCompliant)
            {
                ModelState.Remove("ComplaintDetail.ProcessDescription");
            }

            ModelState.Remove("NextOfKin.IDNumber");
            ModelState.Remove("NextOfKin.CellPhone");
            if (ModelState.IsValid)
            {


                resultSet =  await _modelBL.SaveModelAsync(viewModel);
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        resultSet.Message = error.ErrorMessage;
                    }
                }
            }
            return Json(resultSet);
        }
    }
}
