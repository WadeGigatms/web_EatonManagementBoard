﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Dtos
{
    public class EpcDataDto
    {
        public string wo { get; set; }
        public string qty { get; set; }
        public string pn { get; set; }
        public string line { get; set; }
        public string pallet_id { get; set; }
    }
}
