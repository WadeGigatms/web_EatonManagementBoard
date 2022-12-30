using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Models
{
    public class EatonEpcContext
    {
        public int Sid { get; set; }
        public string Epc { get; set; }
        public string ReaderId { get; set; }
        public DateTime? TransTime { get; set; }
        public int RowId { get; set; }
    }
}
