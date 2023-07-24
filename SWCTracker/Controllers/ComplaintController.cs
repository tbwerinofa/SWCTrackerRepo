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
        public IActionResult Index()
        {
            ComplaintViewModel model =  _modelBL.GetModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveModel(ComplaintViewModel viewModel)
        {

            SaveResult resultSet = new SaveResult();

            if (viewModel.ComplaintDetail.ProcessCompliant)
            {
                ModelState.Remove("ComplaintDetail.ProcessDescription");
            }


            if (ModelState.IsValid)
            {


                resultSet =  _modelBL.SaveModel(viewModel);
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
