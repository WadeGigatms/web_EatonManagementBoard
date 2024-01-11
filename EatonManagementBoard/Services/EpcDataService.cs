using EatonManagementBoard.Database;
using EatonManagementBoard.Database.Dapper;
using EatonManagementBoard.Dtos;
using EatonManagementBoard.Enums;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
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

        public EpcDataService(ConnectionRepositoryManager manager)
        {
            Manager = manager;
        }

        public ConnectionRepositoryManager Manager { get; }

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
                return GetEpcDataResultDto(ResultEnum.False, ErrorEnum.InvalidParameters.ToDescription(), null);
            }

            using (var connection = Manager.MsSqlConnectionRepository.InitConnection())
            {
                connection.Open();

                using (var transaction = Manager.MsSqlConnectionRepository.BeginTransaction())
                {
                    try
                    {
                        List<EpcRawJoinEpcDataContext> contexts = new List<EpcRawJoinEpcDataContext>();

                        if (!string.IsNullOrEmpty(startDate) ||
                            !string.IsNullOrEmpty(endDate) ||
                            !string.IsNullOrEmpty(pastDays) && int.Parse(pastDays) >= 0)
                        {
                            DatePickerEnum datepicker = HandleDatePicker(startDate, endDate, pastDays);
                            if (datepicker == DatePickerEnum.StartDate)
                            {
                                DateTime date = DateTime.ParseExact(startDate, "yyyy-MM-dd", null).Date;

                                contexts = Manager.QueryEpcRawJoinEpcDataContextByStartDate(date);
                            }
                            else if (datepicker == DatePickerEnum.DateRange)
                            {
                                DateTime start = DateTime.ParseExact(startDate, "yyyy-MM-dd", null).Date;
                                DateTime end = DateTime.ParseExact(endDate, "yyyy-MM-dd", null).Date;

                                contexts = Manager.QueryEpcRawJoinEpcDataContextByStartAndEndDate(start, end);
                            }
                            else if (datepicker == DatePickerEnum.PastDays)
                            {
                                DateTime end = DateTime.Today;
                                DateTime start = GetPastDate(end, int.Parse(pastDays)).Date;

                                contexts = Manager.QueryEpcRawJoinEpcDataContextByStartAndEndDate(start, end);
                            }
                        }
                        else if (!string.IsNullOrEmpty(wo))
                        {
                            contexts = Manager.QueryEpcRawJoinEpcDataContextByWo(wo);
                        }
                        else if (!string.IsNullOrEmpty(pn))
                        {
                            contexts = Manager.QueryEpcRawJoinEpcDataContextByPn(pn);
                        }
                        else if (!string.IsNullOrEmpty(palletId))
                        {
                            contexts = Manager.QueryEpcRawJoinEpcDataContextByPalletId(palletId);
                        }

                        // context to dto
                        List<EpcFullDataDto> epcFullDataDtos = new List<EpcFullDataDto>();
                        if (contexts == null || contexts.Count() == 0)
                        {
                            // Commit the transaction if everything is successful
                            transaction.Commit();

                            return GetEpcDataResultDto(ResultEnum.True, ErrorEnum.None.ToDescription(), null);
                        }

                        foreach (var context in contexts)
                        {
                            var dto = GetEpcFullDataDto(context);
                            epcFullDataDtos.Add(dto);
                        }

                        // Get duration from dtos
                        GetDuration(ref epcFullDataDtos);

                        // Get json
                        var json = GetJson(epcFullDataDtos);

                        // Commit the transaction if everything is successful
                        transaction.Commit();

                        return GetEpcDataResultDto(ResultEnum.True, ErrorEnum.None.ToDescription(), epcFullDataDtos);
                    }
                    catch (Exception exp)
                    {
                        // Handle exceptions and optionally roll back the transaction
                        transaction.Rollback();
                        return GetEpcDataResultDto(ResultEnum.False, exp.Message, null);
                    }
                }
            }
        }

        #endregion

        #region Private

        private EpcDataResultDto GetEpcDataResultDto(ResultEnum result, string error, List<EpcFullDataDto> dtos)
        {
            return new EpcDataResultDto
            {
                Result = result.ToBoolean(),
                Error = error,
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
                timestamp = context.timestamp.ToString("yyyy-MM-dd HH:mm:ss"),
                duration = "",
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
                case "TerminalLeft":
                    return ReaderIdEnum.TerminalLeft.ToChineseString();
                case "TerminalRight":
                    return ReaderIdEnum.TerminalRight.ToChineseString();
                case "ManualTerminal":
                    return ReaderIdEnum.ManualTerminal.ToChineseString();
                case "Handheld":
                    return ReaderIdEnum.Handheld.ToChineseString();
                default:
                    return "";

            }
        }

        private string GetJson(List<EpcFullDataDto> epcFullDataDtos)
        {
            try
            {
                var json = JsonConvert.SerializeObject(epcFullDataDtos);
                json = json.Replace("wo", "工單");
                json = json.Replace("pn", "料號");
                json = json.Replace("qty", "數量");
                json = json.Replace("line", "線別");
                json = json.Replace("pallet_id", "棧板編碼");
                json = json.Replace("location", "站別");
                json = json.Replace("timestamp", "經過時間");
                json = json.Replace("duration", "儲存時間");
                return json;
            }
            catch
            {
                return "";
            }
        }

        private void GetDuration(ref List<EpcFullDataDto> dtos)
        {
            var groupByPalletIdDtos = dtos.GroupBy(dto => dto.pallet_id).Select(dto => dto);
            foreach (var groupByPalletIdDto in groupByPalletIdDtos)
            {
                groupByPalletIdDto.OrderBy(d => d.timestamp);
                var firstTimestamp = DateTime.Parse(groupByPalletIdDto.FirstOrDefault().timestamp);
                var lastTimestamp = DateTime.Parse(groupByPalletIdDto.LastOrDefault().timestamp);
                var totalHours = lastTimestamp.Subtract(firstTimestamp).TotalHours;
                var hours = totalHours % 10 >= 5 ? $"{Math.Round(totalHours).ToString()} hours" : $"{Math.Floor(totalHours)}.5 hours";
                foreach (var dto in groupByPalletIdDto)
                {
                    dto.duration = hours;
                }
            }
        }

        #endregion
    }
}
