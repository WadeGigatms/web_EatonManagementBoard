using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Dtos
{
    public class DashboardDto
    {
        public List<EpcDto> WarehouseAEpcDtos { get; set; }
        public List<EpcDto> WarehouseBEpcDtos { get; set; }
        public List<EpcDto> WarehouseCEpcDtos { get; set; }
        public List<EpcDto> WarehouseDEpcDtos { get; set; }
        public List<EpcDto> WarehouseEEpcDtos { get; set; }
        public List<EpcDto> WarehouseFEpcDtos { get; set; }
        public List<EpcDto> WarehouseGEpcDtos { get; set; }
        public List<EpcDto> WarehouseHEpcDtos { get; set; }
        public List<EpcDto> WarehouseIEpcDtos { get; set; }
        public List<EpcDto> ElevatorEpcDtos { get; set; }
        public List<EpcDto> SecondFloorEpcDtos { get; set; }
        public List<EpcDto> ThirdFloorAEpcDtos { get; set; }
        public List<EpcDto> ThirdFloorBEpcDtos { get; set; }
        public List<EpcDto> TerminalEpcDtos { get; set; }
        public List<EpcDto> HandheldEpcDtos { get; set; }
    }
}
