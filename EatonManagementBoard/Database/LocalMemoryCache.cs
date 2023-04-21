using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
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

        public List<EpcContext> ReadRealTimeEpcContext()
        {
            if (!_cache.TryGetValue(LocalMemoryCacheKey.RealTimeEpcContext, out List<EpcContext> cacheEntry)) 
            {
                cacheEntry = null;
            }
            return cacheEntry;
        }

        public List<EpcContext> ReadHistoryEpcContext()
        {
            if (!_cache.TryGetValue(LocalMemoryCacheKey.HistoryEpcContext, out List<EpcContext> cacheEntry))
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

        public void SaveRealTimeEpcContext(List<EpcContext> context)
        {
            _cache.Set(LocalMemoryCacheKey.RealTimeEpcContext, context, TimeSpan.FromHours(1));
        }

        public void SaveHistoryEpcContext(List<EpcContext> context)
        {
            _cache.Set(LocalMemoryCacheKey.HistoryEpcContext, context, TimeSpan.FromHours(1));
        }

        public void SaveDashboardDto(DashboardDto dashboardDto)
        {
            _cache.Set(LocalMemoryCacheKey.DashboardDto, dashboardDto, TimeSpan.FromHours(1));
        }
    }

    public static class LocalMemoryCacheKey
    {
        public static readonly string SearchState = "SearchState";
        public static readonly string EpcCount = "EpcCount";
        public static readonly string RealTimeEpcContext = "RealTimeEpcContext";
        public static readonly string HistoryEpcContext = "HistoryEpcContext";
        public static readonly string DashboardDto = "DashboardDto";
    }
}
