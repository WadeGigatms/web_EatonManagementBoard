using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Enums
{
    public enum ReaderIdEnum
    {
        ThirdFloorA,
        ThirdFloorB,
        SecondFloorA,
        Elevator,
        WareHouseA,
        WareHouseB,
        WareHouseC,
        WareHouseD,
        WareHouseE,
        WareHouseF,
        WareHouseG,
        WareHouseH,
        WareHouseI,
        Terminal,
        ManualTerminal
    }

    public static class ReaderIdEnumExtenstion
    {
        public static string ToString(this ReaderIdEnum readerId)
        {
            switch (readerId)
            {
                case ReaderIdEnum.ThirdFloorA:
                    return "ThirdFloorA";
                case ReaderIdEnum.ThirdFloorB:
                    return "ThirdFloorB";
                case ReaderIdEnum.SecondFloorA:
                    return "SecondFloorA";
                case ReaderIdEnum.Elevator:
                    return "Elevator";
                case ReaderIdEnum.WareHouseA:
                    return "WareHouseA";
                case ReaderIdEnum.WareHouseB:
                    return "WareHouseB";
                case ReaderIdEnum.WareHouseC:
                    return "WareHouseC";
                case ReaderIdEnum.WareHouseD:
                    return "WareHouseD";
                case ReaderIdEnum.WareHouseE:
                    return "WareHouseE";
                case ReaderIdEnum.WareHouseF:
                    return "WareHouseF";
                case ReaderIdEnum.WareHouseG:
                    return "WareHouseG";
                case ReaderIdEnum.WareHouseH:
                    return "WareHouseH";
                case ReaderIdEnum.WareHouseI:
                    return "WareHouseI";
                case ReaderIdEnum.Terminal:
                    return "Terminal";
                case ReaderIdEnum.ManualTerminal:
                    return "ManualTerminal";
                default:
                    return "Unknown readerId";
            }
        }

        public static string ToChineseString(this ReaderIdEnum readerId)
        {
            switch (readerId)
            {
                case ReaderIdEnum.ThirdFloorA:
                    return "A-3F 成品區";
                case ReaderIdEnum.ThirdFloorB:
                    return "B-3F 成品區";
                case ReaderIdEnum.SecondFloorA:
                    return "A-2F 成品區";
                case ReaderIdEnum.Elevator:
                    return "A-1F 暫存區";
                case ReaderIdEnum.WareHouseA:
                    return "A-1F 成品區";
                case ReaderIdEnum.WareHouseB:
                    return "B-1F 成品區";
                case ReaderIdEnum.WareHouseC:
                    return "C-1F 成品區";
                case ReaderIdEnum.WareHouseD:
                    return "D-1F 成品區";
                case ReaderIdEnum.WareHouseE:
                    return "E-1F 成品區";
                case ReaderIdEnum.WareHouseF:
                    return "F-1F 成品區";
                case ReaderIdEnum.WareHouseG:
                    return "G-1F 成品區";
                case ReaderIdEnum.WareHouseH:
                    return "H-1F 成品區";
                case ReaderIdEnum.WareHouseI:
                    return "I-1F 成品區";
                case ReaderIdEnum.Terminal:
                    return "已出貨";
                case ReaderIdEnum.ManualTerminal:
                    return "已出貨";
                default: 
                    return "Unknown readerId";
            }
        }
    }
}
