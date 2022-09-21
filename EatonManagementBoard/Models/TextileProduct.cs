using System;
using System.Collections.Generic;

#nullable disable

namespace EatonManagementBoard.Models
{
    public partial class TextileProduct
    {
        public int Sid { get; set; }
        public string Barcode { get; set; }
        public string Epc { get; set; }
        public string SerialNumber { get; set; }
        public string PType { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Vendor { get; set; }
    }
}
