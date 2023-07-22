using SWCTracker.Models;

namespace SWCTracker.API
{
    public interface IComplaintAPIClient
    {
        ComplaintViewModel GetModel();
    }
}