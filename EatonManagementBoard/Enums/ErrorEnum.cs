using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Enums
{
    public enum ErrorEnum
    {
        None,
        InvalidParameters,
        InvalidEpcFormat,
        InvalidEpcContextFormat,
        InvalidReaderId,
        NoEffectiveData,
        FailToAccessDatabase,
        DidMoveToTerminal,
        FailToPostDelivery,
    }

    public static class ErrorEnumExtensions
    {
        public static string ToDescription(this ErrorEnum error)
        {
            switch (error)
            {
                case ErrorEnum.None:
                    return "";
                case ErrorEnum.InvalidParameters:
                    return "InvalidParameters";
                case ErrorEnum.InvalidEpcFormat:
                    return "InvalidEpcFormat";
                case ErrorEnum.InvalidEpcContextFormat:
                    return "InvalidEpcContextFormat";
                case ErrorEnum.InvalidReaderId:
                    return "InvalidReaderId";
                case ErrorEnum.NoEffectiveData:
                    return "NoEffectiveData";
                case ErrorEnum.FailToAccessDatabase:
                    return "FailToAccessDatabase";
                case ErrorEnum.DidMoveToTerminal:
                    return "DidMoveToTerminal";
                case ErrorEnum.FailToPostDelivery:
                    return "FailToPostDelivery";
                default:
                    return "Unknown";
            }
        }
    }

}
