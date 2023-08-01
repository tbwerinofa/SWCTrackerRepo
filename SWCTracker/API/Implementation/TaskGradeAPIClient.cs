using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using SWCTracker.Models;
using System.Collections.Generic;
using System.Reflection;
using BusinessObject;

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

        public async Task<CalculatorViewModel> GetCalculator(int id, int? occupationGroupId = null)
        {
            var resultSet = await GetTaskGrades(id);

            CalculatorViewModel model = new CalculatorViewModel{ 
            SectorId =id
            };

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
            }
            else
            {
                model.Occupations = IQueryableExtensions.DefaultSelectListItem();
            }
            
            return model;
        }

        public async Task<WageRateDetailResultSet> GetWageRateAsync(int sectorId,int occupationId)
        {
            var wageRates = await GetTaskGrades(sectorId);
            WageRateDetailResultSet model = wageRates.Where(a => a.OccupationId == occupationId).OrderByDescending(a=>a.FinYear).FirstOrDefault();
            return model;
        }

        public async Task<IEnumerable<WageRateDetailResultSet>> GetWageRateByGradeAsync(int id,int grade)
        {
            var resultSet = await GetTaskGrades(id);

            return resultSet.Where(a=>a.Ordinal == grade).AsEnumerable();

        }

        public async Task<IEnumerable<SelectListItem>> GetOccupationSelectListItem_ByParentId(
            int sectorId,
          int parentId)
        {
           var resultSet = await GetTaskGrades(sectorId);

            var model = resultSet.Where(a=>a.OccupationGroupId == parentId);

            return model.Select(a => new
            {
                Value = a.OccupationId.ToString(),
                Text = a.Occupation ?? string.Empty
            }).ToList().Distinct().ToSelectListItem(a => a.Text, a => a.Value);
        }
    }
}
