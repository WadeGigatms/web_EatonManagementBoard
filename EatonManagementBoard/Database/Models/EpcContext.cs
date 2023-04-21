using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Database
{
    public class EpcContext
    {
        public int id { get; set; }
        public string epc { get; set; }
        public string reader_id { get; set; }
        public string timestamp { get; set; }
    }
}
