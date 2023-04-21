using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Database
{
    public class EpcDataContext
    {
        public int id { get; set; }
        public int f_eaton_epc_id { get; set; }
        public string wo { get; set; }
        public int qty { get; set; }
        public string pn { get; set; }
        public string line { get; set; }
        public string pallet_id { get; set; }
    }
}
