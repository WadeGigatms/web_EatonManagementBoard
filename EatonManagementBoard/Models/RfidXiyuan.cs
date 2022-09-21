using System;
using System.Collections.Generic;

#nullable disable

namespace EatonManagementBoard.Models
{
    public partial class RfidXiyuan
    {
        public int Sid { get; set; }
        public string ProductName { get; set; }
        public string ReaderId { get; set; }
        public int? AntennaId { get; set; }
        public DateTime? TransTime { get; set; }
        public double? ReadTime { get; set; }
    }
}
