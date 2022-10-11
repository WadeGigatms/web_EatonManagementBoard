using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Enums
{
    public enum EpcStateEnum
    {
        OK,
        NG,
        Return,
    }

    public static class EpcStateEnumExtenstion
    {
        public static string ToString(this EpcStateEnum epcState)
        {
            switch (epcState)
            {
                case EpcStateEnum.OK:
                    return "OK";
                case EpcStateEnum.NG:
                    return "NG";
                case EpcStateEnum.Return:
                    return "Return";
                default:
                    return "Unknown";
            }
        }
    }
}
