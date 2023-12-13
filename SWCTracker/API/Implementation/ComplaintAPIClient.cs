using BusinessObject;
using System.Net.Http.Formatting;

namespace SWCTracker.API
{
    public class ComplaintAPIClient : IComplaintAPIClient
    {
        private readonly HttpClient _client;
        private  readonly string? RouteId;
        public ComplaintAPIClient(HttpClient client, IConfiguration config)
        {
            _client = client;
            _client.BaseAddress = new System.Uri(config.GetValue<string>("NUMConsecApi"));
            RouteId = config.GetValue<string>("RouteGuid");
        }

        #region Read
        public async Task<ComplaintViewModel> GetModelAsync()
        {
            var requestUri = "complaint";
            var resultSet =  await _client.GetFromJsonAsync<ComplaintViewModel>(requestUri);
         

            return resultSet;
        }
        #endregion

        #region Save
        public async Task<SaveResult> SaveModelAsync(ComplaintViewModel model)
        {
            model.SessionUserId= RouteId;
            var requestUri = "complaint";
            var response = await _client.PostAsync(requestUri, model, new JsonMediaTypeFormatter());
            SaveResult saveResult = await response.Content.ReadAsAsync<SaveResult>();

            return saveResult;
        }
        #endregion
    }
}
