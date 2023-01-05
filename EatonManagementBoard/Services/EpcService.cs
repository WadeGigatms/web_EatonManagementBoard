using EatonManagementBoard.Database;
using EatonManagementBoard.Dtos;
using EatonManagementBoard.Enums;
using EatonManagementBoard.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using LocalMemoryCache = EatonManagementBoard.Database.LocalMemoryCache;

namespace EatonManagementBoard.Services
{
    public class EpcService
    {
        public EpcService(EatonManagementBoardDbContext dbContext, ConnectionRepositoryManager connection, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _connection = connection;
            _localMemoryCache = new LocalMemoryCache(memoryCache);
        }

        private readonly LocalMemoryCache _localMemoryCache;
        private readonly ConnectionRepositoryManager _connection;
        private readonly EatonManagementBoardDbContext _dbContext;
        private readonly string doubleHash = "##";
        private readonly string doubleAnd = "&&";
        private readonly string singleHash = "#";
        private readonly string singleAnd = "&";

        #region Get

        public EpcResultDto Get(string wo = null, string pn = null, string palletId = null)
        {
            var isSearchState = false;
            if (string.IsNullOrEmpty(wo) &&
                string.IsNullOrEmpty(pn) &&
                string.IsNullOrEmpty(palletId))
            {
                isSearchState = false;
            }
            else
            {
                isSearchState = true;
            }

            // Check there were new data inserted in database
            var dataCount = _connection.QueryDataCount();

            // Check database were throw timeout
            if (dataCount == 0)
            {
                return GetEpcGetResultDto(ResultEnum.False, ErrorEnum.MsSqlTimeout, new DashboardDto());
            }

            // Determine to query data from database
            var cacheDataCount = _localMemoryCache.ReadDataCount();
            var cacheSearchState = _localMemoryCache.ReadSearchState();
           if (cacheDataCount == dataCount && cacheSearchState == isSearchState)
            {
                // Same data count in database so return local memory cache
                var dashboard = _localMemoryCache.ReadDashboardDto();
                return GetEpcGetResultDto(ResultEnum.True, ErrorEnum.None, dashboard);
            }

            // Connect to database
            List<EatonEpcContext> realTimeEpcContext = _connection.QueryRealTimeEpc();
            List<EatonEpcContext> traceEpcContext = _connection.QueryTraceEpc();

            // Check database were throw timeout
            if (realTimeEpcContext == null || traceEpcContext == null)
            {
                return GetEpcGetResultDto(ResultEnum.True, ErrorEnum.MsSqlTimeout, new DashboardDto());
            }

            // Get epc context in each location
            List<EatonEpcContext> warehouseAEpcContext = realTimeEpcContext
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseA.ToString())
                .ToList();
            List<EatonEpcContext> warehouseBEpcContext = realTimeEpcContext
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseB.ToString())
                .ToList();
            List<EatonEpcContext> warehouseCEpcContext = realTimeEpcContext
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseC.ToString())
                .ToList();
            List<EatonEpcContext> warehouseDEpcContext = realTimeEpcContext
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseD.ToString())
                .ToList();
            List<EatonEpcContext> warehouseEEpcContext = realTimeEpcContext
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseE.ToString())
                .ToList();
            List<EatonEpcContext> warehouseFEpcContext = realTimeEpcContext
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseF.ToString())
                .ToList();
            List<EatonEpcContext> warehouseGEpcContext = realTimeEpcContext
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseG.ToString())
                .ToList();
            List<EatonEpcContext> warehouseHEpcContext = realTimeEpcContext
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseH.ToString())
                .ToList();
            List<EatonEpcContext> warehouseIEpcContext = realTimeEpcContext
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseI.ToString())
                .ToList();
            List<EatonEpcContext> elevatorEpcContext = realTimeEpcContext
                .Where(epc => epc.ReaderId == ReaderIdEnum.Elevator.ToString())
                .ToList();
            List<EatonEpcContext> secondFloorEpcContext = realTimeEpcContext
                .Where(epc => epc.ReaderId == ReaderIdEnum.SecondFloorA.ToString())
                .ToList();
            List<EatonEpcContext> thirdFloorAEpcContext = realTimeEpcContext
                .Where(epc => epc.ReaderId == ReaderIdEnum.ThirdFloorA.ToString())
                .ToList();
            List<EatonEpcContext> thirdFloorBEpcContext = realTimeEpcContext
                .Where(epc => epc.ReaderId == ReaderIdEnum.ThirdFloorB.ToString())
                .ToList();
            List<EatonEpcContext> terminalEpcContext = realTimeEpcContext
                .Where(epc => epc.ReaderId == ReaderIdEnum.Terminal.ToString() || epc.ReaderId == ReaderIdEnum.ManualTerminal.ToString())
                .ToList();
            List<EatonEpcContext> handheldEpcContext = realTimeEpcContext
                .Where(epc => epc.ReaderId == ReaderIdEnum.Handheld.ToString())
                .ToList();

            List<EpcDto> warehouseAEpcDtos = GetEpcDtos(ref traceEpcContext, ref warehouseAEpcContext, wo, pn, palletId);
            List<EpcDto> warehouseBEpcDtos = GetEpcDtos(ref traceEpcContext, ref warehouseBEpcContext, wo, pn, palletId);
            List<EpcDto> warehouseCEpcDtos = GetEpcDtos(ref traceEpcContext, ref warehouseCEpcContext, wo, pn, palletId);
            List<EpcDto> warehouseDEpcDtos = GetEpcDtos(ref traceEpcContext, ref warehouseDEpcContext, wo, pn, palletId);
            List<EpcDto> warehouseEEpcDtos = GetEpcDtos(ref traceEpcContext, ref warehouseEEpcContext, wo, pn, palletId);
            List<EpcDto> warehouseFEpcDtos = GetEpcDtos(ref traceEpcContext, ref warehouseFEpcContext, wo, pn, palletId);
            List<EpcDto> warehouseGEpcDtos = GetEpcDtos(ref traceEpcContext, ref warehouseGEpcContext, wo, pn, palletId);
            List<EpcDto> warehouseHEpcDtos = GetEpcDtos(ref traceEpcContext, ref warehouseHEpcContext, wo, pn, palletId);
            List<EpcDto> warehouseIEpcDtos = GetEpcDtos(ref traceEpcContext, ref warehouseIEpcContext, wo, pn, palletId);
            List<EpcDto> elevatorEpcDtos = GetEpcDtos(ref traceEpcContext, ref elevatorEpcContext, wo, pn, palletId);
            List<EpcDto> secondFloorEpcDtos = GetEpcDtos(ref traceEpcContext, ref secondFloorEpcContext, wo, pn, palletId);
            List<EpcDto> thirdFloorAEpcDtos = GetEpcDtos(ref traceEpcContext, ref thirdFloorAEpcContext, wo, pn, palletId);
            List<EpcDto> thirdFloorBEpcDtos = GetEpcDtos(ref traceEpcContext, ref thirdFloorBEpcContext, wo, pn, palletId);
            List<EpcDto> terminalEpcDtos = GetEpcDtos(ref traceEpcContext, ref terminalEpcContext, wo, pn, palletId);
            List<EpcDto> handheldEpcDtos = GetEpcDtos(ref traceEpcContext, ref handheldEpcContext, wo, pn, palletId);

            DashboardDto dashboardDto = new DashboardDto() {               
                WarehouseAEpcDtos = warehouseAEpcDtos,
                WarehouseBEpcDtos = warehouseBEpcDtos,
                WarehouseCEpcDtos = warehouseCEpcDtos,
                WarehouseDEpcDtos = warehouseDEpcDtos,
                WarehouseEEpcDtos = warehouseEEpcDtos,
                WarehouseFEpcDtos = warehouseFEpcDtos,
                WarehouseGEpcDtos = warehouseGEpcDtos,
                WarehouseHEpcDtos = warehouseHEpcDtos,
                WarehouseIEpcDtos = warehouseIEpcDtos,
                ElevatorEpcDtos = elevatorEpcDtos,
                SecondFloorEpcDtos = secondFloorEpcDtos,
                ThirdFloorAEpcDtos = thirdFloorAEpcDtos,
                ThirdFloorBEpcDtos = thirdFloorBEpcDtos,
                TerminalEpcDtos = terminalEpcDtos,
                HandheldEpcDtos = handheldEpcDtos
            };

            _localMemoryCache.SaveSearchState(isSearchState);
            _localMemoryCache.SaveDataCount(dataCount);
            _localMemoryCache.SaveRealTimeEpcContext(realTimeEpcContext);
            _localMemoryCache.SaveTraceEpcContext(traceEpcContext);
            _localMemoryCache.SaveDashboardDto(dashboardDto);

            return GetEpcGetResultDto(ResultEnum.True, ErrorEnum.None, dashboardDto);
        }

        private string GetHexToAscii(string hexString)
        {
            // Return if hexString is null or hexString is not double
            if (string.IsNullOrEmpty(hexString) == true)
            {
                return "";
            }
            // Return if hexString is ascii string already
            if (hexString.Contains("#") == true || hexString.Contains("&") == true)
            {
                return hexString;
            }
            // Return if hexString is not double characters
            if (hexString.Count() % 2 != 0)
            {
                return "";
            }
            string asciiString = "";
            try 
            {
                for (int i = 0; i < hexString.Length; i += 2)
                {
                    string tempString = hexString.Substring(i, 2);
                    asciiString += Convert.ToChar(Convert.ToUInt32(tempString, 16));
                }
            }
            catch
            {
                return "";
            }
            return asciiString;
        }

        private List<EpcDto> GetEpcDtos(ref List<EatonEpcContext> traceEpcContext, ref List<EatonEpcContext> realTimeEpcContext, string woString, string pnString, string palletIdString)
        {
            List<EpcDto> epcDtos = new List<EpcDto>();
            foreach (var epc in realTimeEpcContext)
            {
                string wo = "";
                string qty = "";
                string pn = "";
                string line = "";
                string barcode = "";
                string epcString = GetHexToAscii(epc.Epc);
                bool isNewEpcStringFormat = false;
                // Detect epc string format is new or old
                isNewEpcStringFormat = epcString.Contains(doubleHash) == false && epcString.Contains(doubleAnd) == false ? true : false;
                if (isNewEpcStringFormat == false)
                {
                    // Old epc string format
                    // Error string format
                    if (epcString.Contains(doubleHash) == false ||
                        epcString.Split(doubleHash).Count() == 0 ||
                        epcString.Split(doubleHash)[1].Contains(doubleAnd) == false ||
                        epcString.Split(doubleHash)[1].Split(doubleAnd).Count() != 5)
                    {
                        continue;
                    }
                    // Correct string format
                    var epcDatas = epcString.Split(doubleHash)[1].Split(doubleAnd);
                    wo = epcDatas[0];
                    qty = epcDatas[1];
                    pn = epcDatas[2];
                    line = epcDatas[3];
                    barcode = epcDatas[4];
                }
                else
                {
                    // New epc string format
                    // Error string format
                    if (epcString.Contains(singleHash) == false ||
                        epcString.Split(singleHash).Count() == 0 ||
                        epcString.Split(singleHash)[1].Contains(singleAnd) == false ||
                        epcString.Split(singleHash)[1].Split(singleAnd).Count() != 5)
                    {
                        continue;
                    }
                    // Correct string format
                    var epcDatas = epcString.Split(singleHash)[1].Split(singleAnd);
                    wo = epcDatas[0];
                    qty = epcDatas[1];
                    pn = epcDatas[2];
                    line = epcDatas[3];
                    barcode = epcDatas[4];
                }
                List<EatonEpcContext> tracedEpcContext = traceEpcContext
                    .Where(tracedEpc => tracedEpc.Epc == epc.Epc)
                    .ToList();
                List<LocationTimeDto> locationTimeDtos = GetLocationTimeDtos(tracedEpcContext);
                string epcState = GetEpcState(Enumerable.Reverse(locationTimeDtos).ToList());
                if ((string.IsNullOrEmpty(woString) && string.IsNullOrEmpty(pnString) && string.IsNullOrEmpty(palletIdString)) ||
                    (!string.IsNullOrEmpty(woString) && string.IsNullOrEmpty(pnString) && string.IsNullOrEmpty(palletIdString) && wo.Contains(woString)) ||
                    (string.IsNullOrEmpty(woString) && !string.IsNullOrEmpty(pnString) && string.IsNullOrEmpty(palletIdString) && pn.Contains(pnString)) ||
                    (string.IsNullOrEmpty(woString) && string.IsNullOrEmpty(pnString) && !string.IsNullOrEmpty(palletIdString) && barcode.Contains(palletIdString)) ||
                    (!string.IsNullOrEmpty(woString) && string.IsNullOrEmpty(pnString) && !string.IsNullOrEmpty(palletIdString) && wo.Contains(woString) && barcode.Contains(palletIdString)) ||
                    (!string.IsNullOrEmpty(woString) && !string.IsNullOrEmpty(pnString) && string.IsNullOrEmpty(palletIdString) && wo.Contains(woString) && pn.Contains(pnString)) ||
                    (string.IsNullOrEmpty(woString) && !string.IsNullOrEmpty(pnString) && !string.IsNullOrEmpty(palletIdString) && pn.Contains(pnString) && barcode.Contains(palletIdString)) ||
                    (!string.IsNullOrEmpty(woString) && !string.IsNullOrEmpty(pnString) && !string.IsNullOrEmpty(palletIdString) && wo.Contains(woString) && pn.Contains(pnString) && barcode.Contains(palletIdString)))
                {
                    // Without parameters
                    // With wo
                    // With pn
                    // With barcode
                    // With wo, barcode
                    // With wo, pn
                    // With pn, barcode
                    // With wo, pn, barcode
                    string transTime = GetDateTimeString(epc.TransTime.Value);
                    epcDtos.Add(new EpcDto
                    {
                        Id = epc.Sid,
                        Epc = epc.Epc,
                        ReaderId = epc.ReaderId,
                        TransTime = transTime,
                        Wo = wo,
                        Qty = qty,
                        Pn = pn,
                        Line = line,
                        Barcode = barcode,
                        Error = "",
                        LocationTimeDtos = locationTimeDtos,
                        EpcState = epcState,
                    });
                }
            }
            return epcDtos;
        }

        private string GetEpcState(List<LocationTimeDto> locationTimeDtos)
        {
            if (locationTimeDtos.Count == 0 ||
                locationTimeDtos.FirstOrDefault() == null)
            {
                return EpcStateEnum.NG.ToString();
            }
            else
            {
                bool gotFirstStep = false;
                bool gotSecondStep = false;
                bool gotThirdStep = false;
                EpcStateEnum state = EpcStateEnum.OK;
                foreach (var locationTimeDto in locationTimeDtos)
                {
                    if (locationTimeDto == locationTimeDtos.First())
                    {
                        // First step is 3A/3B/2A or not
                        // If not, NG
                        if (locationTimeDto.Location == ReaderIdEnum.ThirdFloorA.ToChineseString() ||
                            locationTimeDto.Location == ReaderIdEnum.SecondFloorA.ToChineseString() ||
                            locationTimeDto.Location == ReaderIdEnum.ThirdFloorB.ToChineseString() ||
                            locationTimeDto.Location == ReaderIdEnum.Handheld.ToChineseString())
                        {
                            gotFirstStep = true;
                        }
                        else
                        {
                            state = EpcStateEnum.NG;
                        }
                    }
                    else
                    {
                        // Other steps
                        if (locationTimeDto.Location == ReaderIdEnum.Elevator.ToChineseString() &&
                            gotFirstStep == true &&
                            gotThirdStep == false)
                        {
                            // Second step is elevator and has been arrived at first step
                            gotSecondStep = true;
                        }
                        else if (locationTimeDto.Location.Contains("1F 成品區") == true &&
                            gotFirstStep == true &&
                            gotSecondStep == true)
                        {
                            // Third step is warehouse and has been arrived at first and second steps
                            gotThirdStep = true;
                        }
                        else if (locationTimeDto.Location != ReaderIdEnum.Terminal.ToChineseString() &&
                            locationTimeDto.Location.Contains("1F 成品區") == false &&
                            gotThirdStep == true)
                        {
                            // This step is reproduct which means it had been arrived at warehouse but now it is at second or first step
                            return EpcStateEnum.Return.ToString();
                        }
                    }
                }
                return state.ToString();
            }
        }

        private List<LocationTimeDto> GetLocationTimeDtos(List<EatonEpcContext> epcContext)
        {
            List<LocationTimeDto> locationTimeDtos = new List<LocationTimeDto>();
            foreach (var epc in epcContext)
            {
                if (locationTimeDtos.Count == 0)
                {
                    locationTimeDtos.Insert(0, new LocationTimeDto()
                    {
                        Location = GetLocationString(epc.ReaderId),
                        TransTime = GetDateTimeString(epc.TransTime.GetValueOrDefault()),
                        DurationTime = "",
                    });
                }
                else
                {
                    LocationTimeDto lastLocationTimeDto = locationTimeDtos.First();
                    if (epc.TransTime == DateTime.Parse(lastLocationTimeDto.TransTime))
                    {
                        continue;
                    }
                    if (GetLocationString(epc.ReaderId) != lastLocationTimeDto.Location)
                    {
                        DateTime lastTransTime = DateTime.Parse(lastLocationTimeDto.TransTime);
                        lastLocationTimeDto.DurationTime = GetTimeSpanString(epc.TransTime.GetValueOrDefault().Subtract(lastTransTime));
                        locationTimeDtos.Insert(0, new LocationTimeDto
                        {
                            Location = GetLocationString(epc.ReaderId),
                            TransTime = GetDateTimeString(epc.TransTime.GetValueOrDefault()),
                            DurationTime = "",
                        });
                    }
                }
            }
            return locationTimeDtos;
        }

        private EpcResultDto GetEpcGetResultDto(ResultEnum result, ErrorEnum error, DashboardDto dashboardDto)
        {
            return new EpcResultDto
            {
                Result = result.ToBoolean(),
                Error = error.ToDescription(),
                DashboardDto = dashboardDto,
            };
        }

        private string GetDateTimeString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy/MM/dd HH:mm:ss");
        }

        private string GetTimeSpanString(TimeSpan time)
        {
            if (time.Days >= 10)
            {
                string dayString = time.ToString(@"dd");
                string timeString = time.ToString(@"hh\:mm\:ss");
                return dayString + " days " + timeString;
            }
            else if (time.Days > 0)
            {
                string dayString = time.ToString(@"dd");
                string timeString = time.ToString(@"hh\:mm\:ss");
                return dayString + " days " + timeString;
            }
            else
            {
                string timeString = time.ToString(@"hh\:mm\:ss");
                return "0 day " + timeString;
            }
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

        public RtcResultDto GetRtc()
        {
            return GetRtcResultDto(ResultEnum.True, ErrorEnum.None, DateTime.Now);
        }

        private RtcResultDto GetRtcResultDto(ResultEnum result, ErrorEnum error, DateTime timestamp)
        {
            return new RtcResultDto
            {
                Result = result.ToBoolean(),
                Error = error.ToDescription(),
                Timestamp = timestamp.ToString("yyyy/MM/dd HH:mm:ss"),
            };
        }

        #endregion

        #region Post

        public ResultDto Post(dynamic value)
        {
            EpcPostDto epcPostDto;

            // Check value
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,
                    MissingMemberHandling = MissingMemberHandling.Error
                };
                epcPostDto = JsonConvert.DeserializeObject<EpcPostDto>(value.ToString(), settings);
            }
            catch
            {
                return GetPostResultDto(ResultEnum.False, ErrorEnum.InvalidParameters);
            }

            // Check epc is valid format
            string decodedEpc = GetHexToAscii(epcPostDto.Epc);
            bool isNewEpcStringFormat = decodedEpc.Contains(doubleHash) == false && decodedEpc.Contains(doubleAnd) == false ? true : false;
            try
            {
                if (isNewEpcStringFormat == false)
                {
                    // Old epc string format
                    // Error string format
                    if (decodedEpc.Split(doubleHash)[1].Split(doubleAnd).Count() != 5)
                    {
                        throw null;
                    }
                }
                else
                {
                    // New epc string format
                    // Error string format
                    if (decodedEpc.Split(singleHash)[1].Split(singleAnd).Count() != 5)
                    {
                        throw null;
                    }
                }
            }
            catch
            {
                return GetPostResultDto(ResultEnum.False, ErrorEnum.InvalidEpcFormat);
            }

            if (IsValidReaderId(epcPostDto.ReaderId) == false)
            {
                return GetPostResultDto(ResultEnum.False, ErrorEnum.InvalidReaderId);
            }

            // Check epc is effective data
            var realTimeEpcContext = _localMemoryCache.ReadRealTimeEpcContext();
            var sameRealTimeEpc = realTimeEpcContext.FirstOrDefault(epc => epc.Epc == epcPostDto.Epc && epc.ReaderId == epcPostDto.ReaderId);
            if (sameRealTimeEpc != null)
            {
                return GetPostResultDto(ResultEnum.True, ErrorEnum.NoEffectiveData);
            }

            // Insert into database
            if (epcPostDto.ReaderId == ReaderIdEnum.ManualTerminal.ToString())
            {
                epcPostDto.TransTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            }
            EatonEpc insertEatonEpc = GetEatonEpc(epcPostDto.Epc, epcPostDto.ReaderId, DateTime.Parse(epcPostDto.TransTime));
            _dbContext.EatonEpcs.Add(insertEatonEpc);
            _dbContext.SaveChanges();

            return GetPostResultDto(ResultEnum.True, ErrorEnum.None); 
        }

        private ResultDto GetPostResultDto(ResultEnum result, ErrorEnum error)
        {
            return new ResultDto
            {
                Result = result.ToBoolean(),
                Error = error.ToDescription()
            };
        }

        private bool IsValidReaderId(string readerId)
        {
            if (ReaderIdEnum.WareHouseA.ToString() == readerId ||
                ReaderIdEnum.WareHouseB.ToString() == readerId ||
                ReaderIdEnum.WareHouseC.ToString() == readerId ||
                ReaderIdEnum.WareHouseD.ToString() == readerId ||
                ReaderIdEnum.WareHouseE.ToString() == readerId ||
                ReaderIdEnum.WareHouseF.ToString() == readerId ||
                ReaderIdEnum.WareHouseG.ToString() == readerId ||
                ReaderIdEnum.WareHouseH.ToString() == readerId ||
                ReaderIdEnum.WareHouseI.ToString() == readerId ||
                ReaderIdEnum.Elevator.ToString() == readerId ||
                ReaderIdEnum.SecondFloorA.ToString() == readerId ||
                ReaderIdEnum.ThirdFloorA.ToString() == readerId ||
                ReaderIdEnum.ThirdFloorB.ToString() == readerId ||
                ReaderIdEnum.Terminal.ToString() == readerId ||
                ReaderIdEnum.ManualTerminal.ToString() == readerId ||
                ReaderIdEnum.Handheld.ToString() == readerId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private EatonEpc GetEatonEpc(string epc, string readerId, DateTime transTime)
        {
            return new EatonEpc
            {
                Epc = epc,
                ReaderId = readerId,
                TransTime = transTime,
            };
        }

        #endregion

        #region Delete

        public ResultDto Delete(string id)
        {
            if (string.IsNullOrEmpty(id) == true)
            {
                return EpcResultDto(ResultEnum.False, ErrorEnum.InvalidParameters);
            }

            if (int.TryParse(id, out int Sid) == false)
            {
                return EpcResultDto(ResultEnum.False, ErrorEnum.InvalidParameters);
            }

            EatonEpc eatonEpc = _dbContext.EatonEpcs
                .Where(eatonEpc => eatonEpc.Sid == Sid)
                .FirstOrDefault();

            if (eatonEpc == null)
            {
                return EpcResultDto(ResultEnum.False, ErrorEnum.InvalidParameters);
            }

            _dbContext.EatonEpcs.Remove(eatonEpc);
            _dbContext.SaveChanges();

            return EpcResultDto(ResultEnum.True, ErrorEnum.None);
        }

        private ResultDto EpcResultDto(ResultEnum result, ErrorEnum error)
        {
            return new ResultDto
            {
                Result = result.ToBoolean(),
                Error = error.ToDescription()
            };
        }

        #endregion
    }
}
