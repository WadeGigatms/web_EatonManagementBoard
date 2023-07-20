using EatonManagementBoard.Database;
using EatonManagementBoard.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Dtos
{
    public class EpcDto
    {
        public EpcRawContext EpcContext { get; set; }
        public EpcDataDto EpcDataDto { get; set; }
        public List<LocationTimeDto> LocationTimeDtos { get; set; }
        public string EpcState { get; set; }
    }
}
