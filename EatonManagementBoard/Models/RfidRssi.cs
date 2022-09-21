using System;
using System.Collections.Generic;

#nullable disable

namespace EatonManagementBoard.Models
{
    public partial class RfidRssi
    {
        public int Sid { get; set; }
        public string Epc { get; set; }
        public string ReaderId { get; set; }
        public DateTime? TransTime { get; set; }
        public int? Antenna { get; set; }
        public int? Rssi { get; set; }
    }
}
