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
        ManualTerminal,
        Handheld,
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
                    return "WarehouseA";
                case ReaderIdEnum.WareHouseB:
                    return "WarehouseB";
                case ReaderIdEnum.WareHouseC:
                    return "WarehouseC";
                case ReaderIdEnum.WareHouseD:
                    return "WarehouseD";
                case ReaderIdEnum.WareHouseE:
                    return "WarehouseE";
                case ReaderIdEnum.WareHouseF:
                    return "WarehouseF";
                case ReaderIdEnum.WareHouseG:
                    return "WarehouseG";
                case ReaderIdEnum.WareHouseH:
                    return "WarehouseH";
                case ReaderIdEnum.WareHouseI:
                    return "WarehouseI";
                case ReaderIdEnum.Terminal:
                    return "Terminal";
                case ReaderIdEnum.ManualTerminal:
                    return "ManualTerminal";
                case ReaderIdEnum.Handheld:
                    return "Handheld";
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
                case ReaderIdEnum.Handheld:
                    return "1F 緩衝區";
                default: 
                    return "Unknown readerId";
            }
        }
    }
}
