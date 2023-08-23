using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Database
{
    public class EpcRawJoinEpcDataContext
    {
        // eaton_epc_data
        public int epc_data_id { get; set; }
        public string wo { get; set; }
        public string pn { get; set; }
        public string line { get; set; }
        public int qty { get; set; }
        public string pallet_id { get; set; }

        // eaton_epc_raw
        public int epc_raw_id { get; set; }
        public string reader_id { get; set; }
        public DateTime timestamp { get; set; }
    }
}
