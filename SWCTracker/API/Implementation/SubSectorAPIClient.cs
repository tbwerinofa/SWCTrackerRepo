using BusinessObject;
namespace SWCTracker.API
{
    public class SubSectorAPIClient: ISubSectorAPIClient
    {
        private readonly HttpClient _client;
        public SubSectorAPIClient(HttpClient client, IConfiguration config)
        {
            _client = client;
            _client.BaseAddress = new System.Uri(config.GetValue<string>("NUMConsecApi"));
        }
        public async Task<List<DashboardItem>> GetSubSectors()
        {
            var requestUri = "subsector";

            //var products = GetProducts();
            return await _client.GetFromJsonAsync<List<DashboardItem>>(requestUri);
        }
    }
}
