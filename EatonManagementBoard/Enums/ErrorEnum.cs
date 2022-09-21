using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Enums
{
    public enum ErrorEnum
    {
        None,
        InvalidParameters
    }

    public static class ErrorEnumExtensions
    {
        public static string ToString(this ErrorEnum error)
        {
            switch (error)
            {
                case ErrorEnum.None:
                    return "None";
                case ErrorEnum.InvalidParameters:
                    return "InvalidParameters";
                default:
                    return "Unknown";
            }
        }
    }

}
