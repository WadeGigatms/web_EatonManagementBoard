using EatonManagementBoard.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Dtos
{
    public class EpcDto
    {
        public int Id { get; set; }
        public string Wo { get; set; }
        public string Qty { get; set; }
        public string Pn { get; set; }
        public string Line { get; set; }
        public string Barcode { get; set; }
        public string Epc { get; set; }
        public string ReaderId { get; set; }
        public string TransTime { get; set; }
        public string Error { get; set; }
        public string EpcState { get; set; }
        public List<LocationTimeDto> LocationTimeDtos { get; set; }
    }
}
