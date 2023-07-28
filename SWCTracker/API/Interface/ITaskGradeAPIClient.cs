using Microsoft.AspNetCore.Mvc.Rendering;
using SWCTracker.Models;

namespace SWCTracker.API
{
    public interface ITaskGradeAPIClient
    {
        Task<List<WageRateDetailResultSet>> GetTaskGrades(int id);

        Task<CalculatorViewModel> GetCalculator(int id,int? occupationGroupId =null);
        Task<IEnumerable<SelectListItem>> GetOccupationSelectListItem_ByParentId(
            int sectorId,
          int parentId);

        Task<WageRateDetailResultSet> GetWageRateAsync(int sectorId, int occupationId);

        Task<IEnumerable<WageRateDetailResultSet>> GetWageRateByGradeAsync(int id, int grade);
    }
}