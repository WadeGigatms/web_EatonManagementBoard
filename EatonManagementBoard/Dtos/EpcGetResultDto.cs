using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Dtos
{
    public class EpcGetResultDto
    {
        public bool Result { get; set; }
        public string Error { get; set; }
        public DashboardDto DashboardDto { get; set; }
        public SelectionDto SelectionDto { get; set; }
    }
}
