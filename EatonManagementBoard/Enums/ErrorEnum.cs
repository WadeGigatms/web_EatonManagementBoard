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
        InvalidReaderId,
        NoEffectiveData,
        MsSqlTimeout,
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
                case ErrorEnum.InvalidReaderId:
                    return "InvalidReaderId";
                case ErrorEnum.NoEffectiveData:
                    return "NoEffectiveData";
                case ErrorEnum.MsSqlTimeout:
                    return "MsSqlTimeout";
                default:
                    return "Unknown";
            }
        }
    }

}
