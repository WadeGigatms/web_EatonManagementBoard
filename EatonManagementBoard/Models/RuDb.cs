using System;
using System.Collections.Generic;

#nullable disable

namespace EatonManagementBoard.Models
{
    public partial class RuDb
    {
        public int Sid { get; set; }
        public string Bundle { get; set; }
        public string ReaderId { get; set; }
        public DateTime? TransTime { get; set; }
        public string AntennaId { get; set; }
        public string Tid { get; set; }
        public string Usermemory { get; set; }
    }
}
