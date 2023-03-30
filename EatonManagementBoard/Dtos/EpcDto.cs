using EatonManagementBoard.Enums;
using EatonManagementBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Dtos
{
    public class EpcDto
    {
        public EpcContext EpcContext { get; set; }
        public EpcDataDto EpcDataDto { get; set; }
        public List<LocationTimeDto> LocationTimeDtos { get; set; }
        public string EpcState { get; set; }
    }
}
