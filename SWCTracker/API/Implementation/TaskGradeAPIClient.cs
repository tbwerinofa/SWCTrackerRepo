using SWCTracker.Models;
namespace SWCTracker.API
{
    public class TaskGradeAPIClient : ITaskGradeAPIClient
    {
        private readonly HttpClient _client;
        public TaskGradeAPIClient(HttpClient client, IConfiguration config)
        {
            _client = client;
            _client.BaseAddress = new System.Uri(config.GetValue<string>("NUMConsecApi"));
        }
        public async Task<List<WageRateDetailResultSet>> GetTaskGrades(int id)
        {
            var requestUri = $"taskgrade/{id}";



            //var products = GetProducts();
            return await _client.GetFromJsonAsync<List<WageRateDetailResultSet>>(requestUri);
        }
    }
}
