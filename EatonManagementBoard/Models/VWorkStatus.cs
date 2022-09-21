using System;
using System.Collections.Generic;

#nullable disable

namespace EatonManagementBoard.Models
{
    public partial class VWorkStatus
    {
        public int Sid { get; set; }
        public string WorkType { get; set; }
        public string EmployeeName { get; set; }
        public string ProjectId { get; set; }
        public string StyleNo { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int? OrderQuantity { get; set; }
        public int? FinishQuantity { get; set; }
        public int? CutQuantity { get; set; }
        public int? SewQuantity { get; set; }
        public int? QcQuantity { get; set; }
        public int? PackQuantity { get; set; }
        public int? StoreQuantity { get; set; }
        public int? DeliveryQuantity { get; set; }
        public int? DiffQuantity { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ScheduleBegin { get; set; }
        public DateTime? ScheduleEnd { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Status { get; set; }
        public string ReaderType { get; set; }
    }
}
