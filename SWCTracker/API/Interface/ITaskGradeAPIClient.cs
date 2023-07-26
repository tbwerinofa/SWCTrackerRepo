using SWCTracker.Models;

namespace SWCTracker.API
{
    public interface ITaskGradeAPIClient
    {
        Task<List<WageRateDetailResultSet>> GetTaskGrades(int id);
    }
}