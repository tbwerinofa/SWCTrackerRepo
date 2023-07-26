using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using SWCTracker.Models;
using System.Collections.Generic;
using System.Reflection;

namespace SWCTracker.API
{
    public class TaskGradeAPIClient : ITaskGradeAPIClient
    {
        private readonly HttpClient _client;
        private readonly IMemoryCache _memoryCache;
        public TaskGradeAPIClient(HttpClient client,
            IConfiguration config,
            IMemoryCache memoryCache)
        {
            _client = client;
            _client.BaseAddress = new System.Uri(config.GetValue<string>("NUMConsecApi"));
            _memoryCache = memoryCache;
        }
        public async Task<List<WageRateDetailResultSet>> GetTaskGrades(int id)
        {
            var requestUri = $"taskgrade/{id}";

            var cacheKey = String.Concat("WageRateDetailResultSet_", id.ToString());
            _ = new List<WageRateDetailResultSet>();
            if (_memoryCache.TryGetValue(cacheKey, out List<WageRateDetailResultSet> model))
            {
                model = _memoryCache.Get(cacheKey) as List<WageRateDetailResultSet>;
            }
            else
            {
                model = await _client.GetFromJsonAsync<List<WageRateDetailResultSet>>(requestUri);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
              //Priority on removing when reaching size limit (memory pressure)
              .SetPriority(CacheItemPriority.Normal)
              // Keep in cache for this time, reset time if accessed.
              .SetSlidingExpiration(TimeSpan.FromHours(8))
              // Remove from cache after this time, regardless of sliding expiration
              .SetAbsoluteExpiration(TimeSpan.FromHours(24));
                // Save data in cache.
                _memoryCache.Set(cacheKey, model, cacheEntryOptions);
            }

            return model;
        }

        public async Task<CalculatorViewModel> GetCalculator(int id, int? occupationGroupId = null,int? occupationId =null)
        {
            var resultSet = await GetTaskGrades(id);

            CalculatorViewModel model = new CalculatorViewModel();

            model.OccupationGroups = resultSet.Select(a => new
            {
                Value = a.OccupationGroupId.ToString(),
                Text = a.OccupationGroup??string.Empty
            }).ToList().Distinct().ToSelectListItem(a => a.Text, a => a.Value);

            if(occupationGroupId != null)
            {
                model.Occupations = resultSet.Where(a=>a.OccupationGroupId == occupationGroupId).Select(a => new
                {
                    Value = a.OccupationId.ToString(),
                    Text = a.Occupation ?? string.Empty
                }).ToList().Distinct().ToSelectListItem(a => a.Text, a => a.Value);
                model.WageRate = GetWageRate(resultSet, occupationId ?? 0);
            }
            else
            {
                model.Occupations = IQueryableExtensions.DefaultSelectListItem();
            }
            
            return model;
        }

        public WageRateDetailResultSet GetWageRate(List<WageRateDetailResultSet> wageRates,int occupationId)
        {
            WageRateDetailResultSet model = wageRates.Where(a => a.OccupationId == occupationId).OrderByDescending(a=>a.FinYear).FirstOrDefault();
            return model;
        }

        public async Task<IEnumerable<SelectListItem>> GetOccupationSelectListItem_ByParentId(
            int sectorId,
          int parentId)
        {
           var resultSet = await GetTaskGrades(sectorId);

            return resultSet.Where(a=>a.OccupationGroupId == parentId).Select(a => new
            {
                Value = a.OccupationId.ToString(),
                Text = a.Occupation ?? string.Empty
            }).ToList().Distinct().ToSelectListItem(a => a.Text, a => a.Value);
        }
    }
}
