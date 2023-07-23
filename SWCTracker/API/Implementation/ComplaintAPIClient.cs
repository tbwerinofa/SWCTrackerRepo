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
        public ComplaintViewModel GetModel()
        {
            var requestUri = "subsector";
            AddressViewModel addressModel = GetAddressModel();
            ComplaintViewModel model = new ComplaintViewModel();
            model.ComplaintDetail = new ComplaintDetailViewModel();
            model.EmploymentDetail = new EmploymentDetailViewModel();

            model.Employee = new PersonViewModel{Address = addressModel};
            model.Organisation = new OrganisationViewModel{Address = addressModel};
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
    }
}
