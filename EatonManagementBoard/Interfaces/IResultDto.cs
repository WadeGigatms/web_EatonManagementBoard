﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Interfaces
{
    public interface IResultDto
    {
        public bool Result { get; set; }
        public string Error { get; set; }
    }
}
