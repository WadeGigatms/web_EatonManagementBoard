using System;
using System.Collections.Generic;

#nullable disable

namespace EatonManagementBoard.Models
{
    public partial class MonthlyVisualize
    {
        public string YearMonth { get; set; }
        public string Kind { get; set; }
        public int? Outsource { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string WorkRole { get; set; }
        public string ShowName { get; set; }
        public double? Quantity { get; set; }
        public double? Amount { get; set; }
        public double? WorkHours { get; set; }
    }
}
