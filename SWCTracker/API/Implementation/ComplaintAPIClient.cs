using Microsoft.AspNetCore.Mvc.Rendering;
using SWCTracker.Models;
using System.Net;
using System.Reflection;

namespace SWCTracker.API
{
    public class ComplaintAPIClient : IComplaintAPIClient
    {
        private readonly HttpClient _client;
        public ComplaintAPIClient(HttpClient client, IConfiguration config)
        {
            _client = client;
            _client.BaseAddress = new System.Uri(config.GetValue<string>("NUMConsecApi"));
        }

        #region Read
        public ComplaintViewModel GetModel()
        {
            var requestUri = "subsector";
            AddressViewModel addressModel = GetAddressModel();
            ComplaintViewModel model = new ComplaintViewModel();
            model.ComplaintDetail = new ComplaintDetailViewModel{
                ComplaintTypes = BuildComplaintTypes(),
                Sectors = BuildSectors()
            };

            model.EmploymentDetail = new EmploymentDetailViewModel{ Occupations = IQueryableExtensions.Default_DropDownItem(), Address = addressModel };

            model.Employee = new PersonViewModel{Address = addressModel};
            model.NextOfKin = new PersonViewModel{Address = addressModel};

            //var products = GetProducts();
            //return await _client.GetFromJsonAsync<List<DashboardItem>>(requestUri);

            return model;
        }

        public AddressViewModel GetAddressModel()
        {
            return new AddressViewModel
            {
                Cities = IQueryableExtensions.DefaultSelectListItem()
            };
        }

        private IEnumerable<SelectListItem> BuildComplaintTypes()
        {
            List<SelectListItem> selectList = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Non - payment of Overtime" },
                new SelectListItem { Value = "2", Text = "Wages / Rates of Pay Sick Leave*" },
                new SelectListItem { Value = "3", Text = "Non - payment of work on Public Holidays" },
                new SelectListItem { Value = "4", Text = "Short Time Payment Leave Bonus" },
                new SelectListItem { Value = "5", Text = "Non - payment of Severance Pay / Ex - Gratia Inclement Weather payment" }
            };
            return selectList;
        }
        private IEnumerable<SelectListItem> BuildSectors()
        {
            List<SelectListItem> selectList = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Civil Engineering" },
                new SelectListItem { Value = "2", Text = "Plant Hire" },
                new SelectListItem { Value = "3", Text = "Open Cast Mining" },
                new SelectListItem { Value = "4", Text = "Temporary Employment Services" }
            };
            return selectList;
        }

        #endregion

        #region Save
        public SaveResult SaveModel(ComplaintViewModel model)
        {
            SaveResult saveResult = new SaveResult{IsSuccess=true };

            return saveResult;
        }
        #endregion
    }
}
