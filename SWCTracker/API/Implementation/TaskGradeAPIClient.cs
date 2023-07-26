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

        public async Task<CalculatorViewModel> GetCalculator(int id)
        {
            var resultSet = await GetTaskGrades(id);

            CalculatorViewModel model = new CalculatorViewModel();

            model.OccupationGroups = resultSet.Select(a => new
            {
                Value = a.OccupationGroupId.ToString(),
                Text = a.OccupationGroup??string.Empty
            }).ToList().Distinct().ToSelectListItem(a => a.Text, a => a.Value);


            model.Occupations = IQueryableExtensions.DefaultSelectListItem();
            return model;
        }
    }
}
