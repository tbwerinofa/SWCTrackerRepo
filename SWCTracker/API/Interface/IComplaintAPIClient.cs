using SWCTracker.Models;

namespace SWCTracker.API
{
    public interface IComplaintAPIClient
    {
        Task<ComplaintViewModel> GetModelAsync();

        SaveResult SaveModel(ComplaintViewModel model);
    }
}