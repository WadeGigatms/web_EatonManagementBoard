using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Dtos
{
    public class EpcDataResultDto : ResultDto
    {
        public List<EpcFullDataDto> EpcFullDataDtos { get; set; }
        public string EpcFullDataJson { get; set; }
    }
}
