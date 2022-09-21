using System;
using System.Collections.Generic;

#nullable disable

namespace EatonManagementBoard.Models
{
    public partial class PreparedTag
    {
        public string ProjectId { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? WriteTime { get; set; }
    }
}
