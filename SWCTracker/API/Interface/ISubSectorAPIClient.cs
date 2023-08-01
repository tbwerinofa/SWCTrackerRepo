using BusinessObject;

namespace SWCTracker.API
{
    public interface ISubSectorAPIClient
    {
        Task<List<DashboardItem>> GetSubSectors();
    }
}