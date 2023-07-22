using SWCTracker.Models;

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
            ComplaintViewModel model = new ComplaintViewModel();
            //var products = GetProducts();
            //return await _client.GetFromJsonAsync<List<DashboardItem>>(requestUri);

            return model;
        }
    }
}
