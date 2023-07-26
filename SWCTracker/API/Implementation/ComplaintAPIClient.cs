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
        public async Task<ComplaintViewModel> GetModelAsync()
        {
            var requestUri = "complaint";
            var resultSet =  await _client.GetFromJsonAsync<ComplaintViewModel>(requestUri);
         

            return resultSet;
        }

        public AddressViewModel GetAddressModel()
        {
            return new AddressViewModel
            {
                Cities = IQueryableExtensions.DefaultSelectListItem()
            };
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
