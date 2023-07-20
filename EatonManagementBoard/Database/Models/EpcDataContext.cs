using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Database
{
    public class EpcDataContext
    {
        public int id { get; set; } 
        public string f_epc_raw_ids { get; set; }
        public string wo { get; set; }
        public string pn { get; set; }
        public string line { get; set; }
        public int qty { get; set; }
        public string pallet_id { get; set; }
    }
}
