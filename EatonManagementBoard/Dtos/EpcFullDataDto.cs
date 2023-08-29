using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EatonManagementBoard.Dtos
{
    public class EpcFullDataDto
    {
        // eaton_epc_data
        public string wo { get; set; }
        public string pn { get; set; }
        public string line { get; set; }
        public string qty { get; set; }
        public string pallet_id { get; set; }

        // eaton_epc_raw
        public string location { get; set; }
        public string timestamp { get; set; }
        public string duration { get; set; }
    }
}
