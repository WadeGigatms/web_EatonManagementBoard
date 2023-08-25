using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Database
{
    public static class LocalMemoryCacheKey
    {
        public static readonly string SearchState = "SearchState";
        public static readonly string EpcCount = "EpcCount";
        public static readonly string RealTimeEpcRawContext = "RealTimeEpcRawContext";
        public static readonly string HistoryEpcRawContext = "HistoryEpcRawContext";
        public static readonly string DashboardDto = "DashboardDto";
    }
}
