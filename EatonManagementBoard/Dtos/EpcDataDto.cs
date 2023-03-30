using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Dtos
{
    public class EpcDataDto
    {
        public string Wo { get; set; }
        public string Qty { get; set; }
        public string Pn { get; set; }
        public string Line { get; set; }
        public string PalletId { get; set; }
    }
}
