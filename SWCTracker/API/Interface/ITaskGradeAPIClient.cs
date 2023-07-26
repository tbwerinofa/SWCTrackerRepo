using Microsoft.AspNetCore.Mvc.Rendering;
using SWCTracker.Models;

namespace SWCTracker.API
{
    public interface ITaskGradeAPIClient
    {
        Task<List<WageRateDetailResultSet>> GetTaskGrades(int id);

        Task<CalculatorViewModel> GetCalculator(int id,int? occupationGroupId =null, int? occupationId = null);
        Task<IEnumerable<SelectListItem>> GetOccupationSelectListItem_ByParentId(
            int sectorId,
          int parentId);
    }
}