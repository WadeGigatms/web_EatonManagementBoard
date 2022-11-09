using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Dtos
{
    public class GetEpcResultDto: EpcResultDto
    {
        public DashboardDto DashboardDto { get; set; }
        public SelectionDto SelectionDto { get; set; }
    }
}
