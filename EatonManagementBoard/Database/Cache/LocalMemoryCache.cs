using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EatonManagementBoard.Dtos;

namespace EatonManagementBoard.Database
{
    public class LocalMemoryCache
    {
        private readonly IMemoryCache _cache;

        public LocalMemoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool ReadSearchState()
        {
            if (!_cache.TryGetValue(LocalMemoryCacheKey.SearchState, out bool cacheEntry))
            {
                cacheEntry = false;
            }
            return cacheEntry;
        }

        public int ReadEpcCount()
        {
            if (!_cache.TryGetValue(LocalMemoryCacheKey.EpcCount, out int cacheEntry))
            {
                cacheEntry = -1;
            }
            return cacheEntry;
        }

        public List<EpcRawContext> ReadRealTimeEpcRawContext()
        {
            if (!_cache.TryGetValue(LocalMemoryCacheKey.RealTimeEpcRawContext, out List<EpcRawContext> cacheEntry)) 
            {
                cacheEntry = null;
            }
            return cacheEntry;
        }

        public List<EpcRawContext> ReadHistoryEpcRawContext()
        {
            if (!_cache.TryGetValue(LocalMemoryCacheKey.HistoryEpcRawContext, out List<EpcRawContext> cacheEntry))
            {
                cacheEntry = null;
            }
            return cacheEntry;
        }

        public DashboardDto ReadDashboardDto()
        {
            if(!_cache.TryGetValue(LocalMemoryCacheKey.DashboardDto, out DashboardDto cacheEntry))
            {
                cacheEntry = null;
            }
            return cacheEntry;
        }

        public void SaveSearchState(bool state)
        {
            _cache.Set(LocalMemoryCacheKey.SearchState, state, TimeSpan.FromHours(1));
        }

        public void SaveEpcCount(int dataCount)
        {
            _cache.Set(LocalMemoryCacheKey.EpcCount, dataCount, TimeSpan.FromHours(1));
        }

        public void SaveRealTimeEpcRawContext(List<EpcRawContext> context)
        {
            _cache.Set(LocalMemoryCacheKey.RealTimeEpcRawContext, context, TimeSpan.FromHours(1));
        }

        public void SaveHistoryEpcRawContext(List<EpcRawContext> context)
        {
            _cache.Set(LocalMemoryCacheKey.HistoryEpcRawContext, context, TimeSpan.FromHours(1));
        }

        public void SaveDashboardDto(DashboardDto dashboardDto)
        {
            _cache.Set(LocalMemoryCacheKey.DashboardDto, dashboardDto, TimeSpan.FromHours(1));
        }
    }
}
