using SWCTracker.Models;

namespace SWCTracker.API
{
    public interface IComplaintAPIClient
    {
        Task<ComplaintViewModel> GetModelAsync();

        Task<SaveResult> SaveModelAsync(ComplaintViewModel model);
    }
}