using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Dtos
{
    public class SelectionDto
    {
        public List<string> Wos { get; set; }
        public List<string> Pns { get; set; }
        public List<string> PalletIds { get; set; }
    }
}
