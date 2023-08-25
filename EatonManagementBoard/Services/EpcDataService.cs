using EatonManagementBoard.Database;
using EatonManagementBoard.Dtos;
using EatonManagementBoard.Enums;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EatonManagementBoard.Services
{
    public class EpcDataService
    {
        public EpcDataService(ConnectionRepositoryManager connection, IMemoryCache memoryCache)
        {
            _connection = connection;
            _localMemoryCache = new LocalMemoryCache(memoryCache);
        }

        private readonly LocalMemoryCache _localMemoryCache;
        private readonly ConnectionRepositoryManager _connection;

        #region Public

        public EpcDataResultDto Get(string wo, string pn, string palletId, string startDate, string endDate, string pastDays)
        {
            if (string.IsNullOrEmpty(wo) &&
                string.IsNullOrEmpty(pn) &&
                string.IsNullOrEmpty(palletId) &&
                string.IsNullOrEmpty(startDate) &&
                string.IsNullOrEmpty(endDate) &&
                string.IsNullOrEmpty(pastDays))
            {
                return GetEpcDataResultDto(ResultEnum.False, ErrorEnum.InvalidParameters, null);
            }

            List<EpcRawJoinEpcDataContext> contexts = new List<EpcRawJoinEpcDataContext>();

            if (!string.IsNullOrEmpty(startDate) ||
                !string.IsNullOrEmpty(endDate) ||
                !string.IsNullOrEmpty(pastDays) && int.Parse(pastDays) >= 0)
            {
                DatePickerEnum datepicker = HandleDatePicker(startDate, endDate, pastDays);
                if (datepicker == DatePickerEnum.StartDate)
                {
                    DateTime date = DateTime.ParseExact(startDate, "yyyy-MM-dd", null).Date;

                    contexts = _connection.QueryEpcRawJoinEpcDataContextByStartDate(date);
                }
                else if (datepicker == DatePickerEnum.DateRange)
                {
                    DateTime start = DateTime.ParseExact(startDate, "yyyy-MM-dd", null).Date;
                    DateTime end = DateTime.ParseExact(endDate, "yyyy-MM-dd", null).Date;

                    contexts = _connection.QueryEpcRawJoinEpcDataContextByStartAndEndDate(start, end);
                }
                else if (datepicker == DatePickerEnum.PastDays)
                {
                    DateTime end = DateTime.Today;
                    DateTime start = GetPastDate(end, int.Parse(pastDays)).Date;

                    contexts = _connection.QueryEpcRawJoinEpcDataContextByStartAndEndDate(start, end);
                }
            }
            else if (!string.IsNullOrEmpty(wo))
            {
                contexts = _connection.QueryEpcRawJoinEpcDataContextByWo(wo);
            }
            else if (!string.IsNullOrEmpty(pn))
            {
                contexts = _connection.QueryEpcRawJoinEpcDataContextByPn(pn);
            }
            else if (!string.IsNullOrEmpty(palletId))
            {
                contexts = _connection.QueryEpcRawJoinEpcDataContextByPalletId(palletId);
            }

            // context to dto
            List<EpcFullDataDto> epcFullDataDtos = new List<EpcFullDataDto>();
            if (contexts == null || contexts.Count() == 0)
            {
                return GetEpcDataResultDto(ResultEnum.True, ErrorEnum.None, null);
            }

            foreach (var context in contexts)
            {
                var dto = GetEpcFullDataDto(context);
                epcFullDataDtos.Add(dto);
            }

            // Json
            var epcFullDataJson = JsonSerializer.Serialize(epcFullDataDtos);

            return GetEpcDataResultDto(ResultEnum.True, ErrorEnum.None, epcFullDataDtos);
        }

        #endregion

        #region Private

        private EpcDataResultDto GetEpcDataResultDto(ResultEnum result, ErrorEnum error, List<EpcFullDataDto> dtos)
        {
            return new EpcDataResultDto
            {
                Result = result.ToBoolean(),
                Error = error.ToDescription(),
                EpcFullDataDtos = dtos
            };
        }

        private DatePickerEnum HandleDatePicker(string startDate, string endDate, string pastDays)
        {
            if (!string.IsNullOrEmpty(startDate) &&
                string.IsNullOrEmpty(endDate) &&
                string.IsNullOrEmpty(pastDays))
            {
                // startDate
                return DatePickerEnum.StartDate;
            }
            else if (!string.IsNullOrEmpty(startDate) &&
                !string.IsNullOrEmpty(endDate) &&
                string.IsNullOrEmpty(pastDays))
            {
                // startDate, endDate
                return DatePickerEnum.DateRange;
            }
            else if (string.IsNullOrEmpty(startDate) &&
               string.IsNullOrEmpty(endDate) &&
               !string.IsNullOrEmpty(pastDays))
            {
                // pastDays
                return DatePickerEnum.PastDays;
            }
            else
            {
                return DatePickerEnum.None;
            }
        }

        private DateTime GetPastDate(DateTime date, int pastDays)
        {
            switch (pastDays)
            {
                case 0:
                    return date;
                case 1:
                    return date.AddDays(-pastDays);
                case 7:
                    return date.AddDays(-pastDays);
                case 30:
                    return date.AddMonths(-1);
                case 180:
                    return date.AddMonths(-6);
                case 365:
                    return date.AddYears(-1);
                default:
                    return date;
            }
        }

        private EpcFullDataDto GetEpcFullDataDto(EpcRawJoinEpcDataContext context)
        {
            return new EpcFullDataDto
            {
                wo = context.wo,
                qty = context.qty.ToString(),
                pn = context.pn,
                line = context.line,
                pallet_id = context.pallet_id,
                location = GetLocationString(context.reader_id),
                timestamp = context.timestamp.ToString("yyyy-MM-dd HH:mm:ss")
            };
        }

        private string GetLocationString(string readerId)
        {
            switch (readerId)
            {
                case "ThirdFloorA":
                    return ReaderIdEnum.ThirdFloorA.ToChineseString();
                case "ThirdFloorB":
                    return ReaderIdEnum.ThirdFloorB.ToChineseString();
                case "SecondFloorA":
                    return ReaderIdEnum.SecondFloorA.ToChineseString();
                case "Elevator":
                    return ReaderIdEnum.Elevator.ToChineseString();
                case "WareHouseA":
                    return ReaderIdEnum.WareHouseA.ToChineseString();
                case "WareHouseB":
                    return ReaderIdEnum.WareHouseB.ToChineseString();
                case "WareHouseC":
                    return ReaderIdEnum.WareHouseC.ToChineseString();
                case "WareHouseD":
                    return ReaderIdEnum.WareHouseD.ToChineseString();
                case "WareHouseE":
                    return ReaderIdEnum.WareHouseE.ToChineseString();
                case "WareHouseF":
                    return ReaderIdEnum.WareHouseF.ToChineseString();
                case "WareHouseG":
                    return ReaderIdEnum.WareHouseG.ToChineseString();
                case "WareHouseH":
                    return ReaderIdEnum.WareHouseH.ToChineseString();
                case "WareHouseI":
                    return ReaderIdEnum.WareHouseI.ToChineseString();
                case "Terminal":
                    return ReaderIdEnum.Terminal.ToChineseString();
                case "ManualTerminal":
                    return ReaderIdEnum.ManualTerminal.ToChineseString();
                case "Handheld":
                    return ReaderIdEnum.Handheld.ToChineseString();
                default:
                    return "";

            }
        }

        #endregion
    }
}
