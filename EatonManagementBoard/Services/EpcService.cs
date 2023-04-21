using EatonManagementBoard.Database;
using EatonManagementBoard.Dtos;
using EatonManagementBoard.Enums;
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
        public EpcService(ConnectionRepositoryManager connection, IMemoryCache memoryCache)
        {
            _connection = connection;
            _localMemoryCache = new LocalMemoryCache(memoryCache);
        }

        private readonly LocalMemoryCache _localMemoryCache;
        private readonly ConnectionRepositoryManager _connection;
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
            var epcCount = _connection.QueryEpcCount();
            if (epcCount == 0)
            {
                return GetEpcGetResultDto(ResultEnum.True, ErrorEnum.None, new DashboardDto());
            }

            // Determine to query data from database
            var cacheEpcCount = _localMemoryCache.ReadEpcCount();
            var cacheSearchState = _localMemoryCache.ReadSearchState();
            if (cacheEpcCount == epcCount && cacheSearchState == isSearchState)
            {
                // Same data count in database so return local memory cache
                var dashboard = _localMemoryCache.ReadDashboardDto();
                return GetEpcGetResultDto(ResultEnum.True, ErrorEnum.None, dashboard);
            }

            // Connect to database
            List<EpcContext> realTimeEpcContext = _connection.QueryRealTimeEpcContext();
            List<EpcContext> historyEpcContext = _connection.QueryHistoryEpcContext();

            // Check database were throw timeout
            if (realTimeEpcContext == null || historyEpcContext == null)
            {
                return GetEpcGetResultDto(ResultEnum.False, ErrorEnum.MsSqlTimeout, null);
            }

            // Get epc context in each location
            List<EpcContext> warehouseAEpcContext = realTimeEpcContext
                .Where(epc => epc.reader_id == ReaderIdEnum.WareHouseA.ToString())
                .ToList();
            List<EpcContext> warehouseBEpcContext = realTimeEpcContext
                .Where(epc => epc.reader_id == ReaderIdEnum.WareHouseB.ToString())
                .ToList();
            List<EpcContext> warehouseCEpcContext = realTimeEpcContext
                .Where(epc => epc.reader_id == ReaderIdEnum.WareHouseC.ToString())
                .ToList();
            List<EpcContext> warehouseDEpcContext = realTimeEpcContext
                .Where(epc => epc.reader_id == ReaderIdEnum.WareHouseD.ToString())
                .ToList();
            List<EpcContext> warehouseEEpcContext = realTimeEpcContext
                .Where(epc => epc.reader_id == ReaderIdEnum.WareHouseE.ToString())
                .ToList();
            List<EpcContext> warehouseFEpcContext = realTimeEpcContext
                .Where(epc => epc.reader_id == ReaderIdEnum.WareHouseF.ToString())
                .ToList();
            List<EpcContext> warehouseGEpcContext = realTimeEpcContext
                .Where(epc => epc.reader_id == ReaderIdEnum.WareHouseG.ToString())
                .ToList();
            List<EpcContext> warehouseHEpcContext = realTimeEpcContext
                .Where(epc => epc.reader_id == ReaderIdEnum.WareHouseH.ToString())
                .ToList();
            List<EpcContext> warehouseIEpcContext = realTimeEpcContext
                .Where(epc => epc.reader_id == ReaderIdEnum.WareHouseI.ToString())
                .ToList();
            List<EpcContext> elevatorEpcContext = realTimeEpcContext
                .Where(epc => epc.reader_id == ReaderIdEnum.Elevator.ToString())
                .ToList();
            List<EpcContext> secondFloorEpcContext = realTimeEpcContext
                .Where(epc => epc.reader_id == ReaderIdEnum.SecondFloorA.ToString())
                .ToList();
            List<EpcContext> thirdFloorAEpcContext = realTimeEpcContext
                .Where(epc => epc.reader_id == ReaderIdEnum.ThirdFloorA.ToString())
                .ToList();
            List<EpcContext> thirdFloorBEpcContext = realTimeEpcContext
                .Where(epc => epc.reader_id == ReaderIdEnum.ThirdFloorB.ToString())
                .ToList();
            List<EpcContext> terminalEpcContext = realTimeEpcContext
                .Where(epc => epc.reader_id == ReaderIdEnum.Terminal.ToString() || epc.reader_id == ReaderIdEnum.ManualTerminal.ToString())
                .ToList();
            List<EpcContext> handheldEpcContext = realTimeEpcContext
                .Where(epc => epc.reader_id == ReaderIdEnum.Handheld.ToString())
                .ToList();

            List<EpcDto> warehouseAEpcDtos = GetEpcDtos(ref historyEpcContext, ref warehouseAEpcContext, wo, pn, palletId);
            List<EpcDto> warehouseBEpcDtos = GetEpcDtos(ref historyEpcContext, ref warehouseBEpcContext, wo, pn, palletId);
            List<EpcDto> warehouseCEpcDtos = GetEpcDtos(ref historyEpcContext, ref warehouseCEpcContext, wo, pn, palletId);
            List<EpcDto> warehouseDEpcDtos = GetEpcDtos(ref historyEpcContext, ref warehouseDEpcContext, wo, pn, palletId);
            List<EpcDto> warehouseEEpcDtos = GetEpcDtos(ref historyEpcContext, ref warehouseEEpcContext, wo, pn, palletId);
            List<EpcDto> warehouseFEpcDtos = GetEpcDtos(ref historyEpcContext, ref warehouseFEpcContext, wo, pn, palletId);
            List<EpcDto> warehouseGEpcDtos = GetEpcDtos(ref historyEpcContext, ref warehouseGEpcContext, wo, pn, palletId);
            List<EpcDto> warehouseHEpcDtos = GetEpcDtos(ref historyEpcContext, ref warehouseHEpcContext, wo, pn, palletId);
            List<EpcDto> warehouseIEpcDtos = GetEpcDtos(ref historyEpcContext, ref warehouseIEpcContext, wo, pn, palletId);
            List<EpcDto> elevatorEpcDtos = GetEpcDtos(ref historyEpcContext, ref elevatorEpcContext, wo, pn, palletId);
            List<EpcDto> secondFloorEpcDtos = GetEpcDtos(ref historyEpcContext, ref secondFloorEpcContext, wo, pn, palletId);
            List<EpcDto> thirdFloorAEpcDtos = GetEpcDtos(ref historyEpcContext, ref thirdFloorAEpcContext, wo, pn, palletId);
            List<EpcDto> thirdFloorBEpcDtos = GetEpcDtos(ref historyEpcContext, ref thirdFloorBEpcContext, wo, pn, palletId);
            List<EpcDto> terminalEpcDtos = GetEpcDtos(ref historyEpcContext, ref terminalEpcContext, wo, pn, palletId);
            List<EpcDto> handheldEpcDtos = GetEpcDtos(ref historyEpcContext, ref handheldEpcContext, wo, pn, palletId);

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
            _localMemoryCache.SaveEpcCount(epcCount);
            _localMemoryCache.SaveRealTimeEpcContext(realTimeEpcContext);
            _localMemoryCache.SaveHistoryEpcContext(historyEpcContext);
            _localMemoryCache.SaveDashboardDto(dashboardDto);

            return GetEpcGetResultDto(ResultEnum.True, ErrorEnum.None, dashboardDto);
        }

        private string GetHexToAscii(string hexString)
        {
            // Return if hexString is null or hexString is not double
            if (string.IsNullOrEmpty(hexString) == true)
            {
                return null;
            }
            // Return if hexString is ascii string already
            if (hexString.Contains("#") == true || hexString.Contains("&") == true)
            {
                return hexString;
            }
            // Return if hexString is not double characters
            if (hexString.Count() % 2 != 0)
            {
                return null;
            }

            // Transfer hex to ascii string
            try 
            {
                string asciiString = "";
                for (int i = 0; i < hexString.Length; i += 2)
                {
                    string tempString = hexString.Substring(i, 2);
                    asciiString += Convert.ToChar(Convert.ToUInt32(tempString, 16));
                }

                return asciiString;
            }
            catch
            {
                return null;
            }
        }

        private EpcDataDto GetEpcDataDto(string asciiEpcString)
        {
            try
            {
                bool isNewEpcFormat = asciiEpcString.Contains(singleHash) == true && asciiEpcString.Contains(singleAnd) == true ? true : false;
                bool isOldEpcFormat = asciiEpcString.Contains(doubleHash) == true && asciiEpcString.Contains(doubleAnd) == true ? true : false;
                if (isNewEpcFormat == true && isOldEpcFormat == false)
                {
                    // New epc string format
                    var newEpcFormats = asciiEpcString.Split(singleHash);
                    if (string.IsNullOrEmpty(newEpcFormats[0]) == false
                        || string.IsNullOrEmpty(newEpcFormats[2]) == false
                        || newEpcFormats[1].Split(singleAnd).Count() != 5)
                    {
                        return null;
                    }

                    // Check epc context format
                    var properties = newEpcFormats[1].Split(singleAnd);
                    if (properties[0].Length > 8
                        || properties[1].Length > 3
                        || properties[2].Length > 20
                        || properties[3].Length > 3
                        || properties[4].Length > 10)
                    {
                        return null;
                    }

                    return new EpcDataDto
                    {
                        wo = properties[0],
                        qty = properties[1],
                        pn = properties[2],
                        line = properties[3],
                        pallet_id = properties[4],
                    };
                }
                else if (isNewEpcFormat == false && isOldEpcFormat == true)
                {
                    // Old epc string format
                    var oldEpcFormats = asciiEpcString.Split(doubleHash);
                    if (string.IsNullOrEmpty(oldEpcFormats[0]) == false
                        || string.IsNullOrEmpty(oldEpcFormats[2]) == false
                        || oldEpcFormats[1].Split(doubleAnd).Count() != 5)
                    {
                        return null;
                    }

                    // Check epc context format
                    var properties = oldEpcFormats[1].Split(doubleAnd);
                    if (properties[0].Length > 8
                        || properties[1].Length > 3
                        || properties[2].Length > 20
                        || properties[3].Length > 3
                        || properties[4].Length > 10)
                    {
                        return null;
                    }

                    return new EpcDataDto
                    {
                        wo = properties[0],
                        qty = properties[1],
                        pn = properties[2],
                        line = properties[3],
                        pallet_id = properties[4],
                    };
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }

        private List<EpcDto> GetEpcDtos(ref List<EpcContext> historyEpcContext, ref List<EpcContext> realTimeEpcContext, string woString, string pnString, string palletIdString)
        {
            List<EpcDto> epcDtos = new List<EpcDto>();
            foreach (var realTimeEpc in realTimeEpcContext)
            {
                string asciiEpcString = GetHexToAscii(realTimeEpc.epc);
                if (string.IsNullOrEmpty(asciiEpcString) == true)
                {
                    continue;
                }

                EpcDataDto epcDataDto = GetEpcDataDto(asciiEpcString);
                if (epcDataDto == null)
                {
                    continue;
                }

                EpcContext epcContext = GetEpcContext(realTimeEpc);

                List<EpcContext> tracedEpcContext = historyEpcContext
                    .Where(tracedEpc => tracedEpc.epc == realTimeEpc.epc)
                    .ToList();
                List<LocationTimeDto> locationTimeDtos = GetLocationTimeDtos(tracedEpcContext);

                string epcState = GetEpcState(Enumerable.Reverse(locationTimeDtos).ToList());

                if ((string.IsNullOrEmpty(woString) && string.IsNullOrEmpty(pnString) && string.IsNullOrEmpty(palletIdString)) ||
                    (!string.IsNullOrEmpty(woString) && string.IsNullOrEmpty(pnString) && string.IsNullOrEmpty(palletIdString) && epcDataDto.wo.Contains(woString)) ||
                    (string.IsNullOrEmpty(woString) && !string.IsNullOrEmpty(pnString) && string.IsNullOrEmpty(palletIdString) && epcDataDto.pn.Contains(pnString)) ||
                    (string.IsNullOrEmpty(woString) && string.IsNullOrEmpty(pnString) && !string.IsNullOrEmpty(palletIdString) && epcDataDto.pallet_id.Contains(palletIdString)) ||
                    (!string.IsNullOrEmpty(woString) && string.IsNullOrEmpty(pnString) && !string.IsNullOrEmpty(palletIdString) && epcDataDto.wo.Contains(woString) && epcDataDto.pallet_id.Contains(palletIdString)) ||
                    (!string.IsNullOrEmpty(woString) && !string.IsNullOrEmpty(pnString) && string.IsNullOrEmpty(palletIdString) && epcDataDto.wo.Contains(woString) && epcDataDto.pn.Contains(pnString)) ||
                    (string.IsNullOrEmpty(woString) && !string.IsNullOrEmpty(pnString) && !string.IsNullOrEmpty(palletIdString) && epcDataDto.pn.Contains(pnString) && epcDataDto.pallet_id.Contains(palletIdString)) ||
                    (!string.IsNullOrEmpty(woString) && !string.IsNullOrEmpty(pnString) && !string.IsNullOrEmpty(palletIdString) && epcDataDto.wo.Contains(woString) && epcDataDto.pn.Contains(pnString) && epcDataDto.pallet_id.Contains(palletIdString)))
                {
                    // Without parameters
                    // With wo
                    // With pn
                    // With barcode
                    // With wo, barcode
                    // With wo, pn
                    // With pn, barcode
                    // With wo, pn, barcode
                    epcDtos.Add(new EpcDto
                    {
                        EpcContext = epcContext,
                        EpcDataDto = epcDataDto,
                        LocationTimeDtos = locationTimeDtos,
                        EpcState = epcState,
                    });
                }
            }
            return epcDtos;
        }

        private EpcContext GetEpcContext(EpcContext context)
        {
            context.timestamp = GetDateTimeString(context.timestamp);
            return context;
        }

        private string GetEpcState(List<LocationTimeDto> locationTimeDtos)
        {
            bool isArrivedProduction = false;
            bool isArrivedElevator = false;
            bool isArrivedWareHouse = false;
            bool isArrivedTerminal = false;
            EpcStateEnum state = EpcStateEnum.OK;
            if (locationTimeDtos.Count() == 0) { return ""; }

            for(var i = 0; i < locationTimeDtos.Count(); i++)
            {
                if (locationTimeDtos[i].Location == ReaderIdEnum.ThirdFloorA.ToChineseString() ||
                    locationTimeDtos[i].Location == ReaderIdEnum.ThirdFloorB.ToChineseString() ||
                    locationTimeDtos[i].Location == ReaderIdEnum.SecondFloorA.ToChineseString())
                {
                    isArrivedProduction = true;

                    // Arrived at production after arriving at elevator or warehouse or terminal
                    if (i > 0 && (isArrivedElevator == true || isArrivedWareHouse == true || isArrivedTerminal == true)) 
                    {
                        state = EpcStateEnum.Return;
                    }
                }
                else if (locationTimeDtos[i].Location == ReaderIdEnum.Elevator.ToChineseString())
                {
                    isArrivedElevator = true;

                    // Arrived at elevator without arriving at production
                    if (i == 0 && isArrivedProduction == false) 
                    { 
                        state = EpcStateEnum.NG;
                    }
                }
                else if (locationTimeDtos[i].Location == ReaderIdEnum.WareHouseA.ToChineseString() ||
                    locationTimeDtos[i].Location == ReaderIdEnum.WareHouseB.ToChineseString() ||
                    locationTimeDtos[i].Location == ReaderIdEnum.WareHouseC.ToChineseString() ||
                    locationTimeDtos[i].Location == ReaderIdEnum.WareHouseD.ToChineseString() ||
                    locationTimeDtos[i].Location == ReaderIdEnum.WareHouseE.ToChineseString() ||
                    locationTimeDtos[i].Location == ReaderIdEnum.WareHouseF.ToChineseString() ||
                    locationTimeDtos[i].Location == ReaderIdEnum.WareHouseG.ToChineseString() ||
                    locationTimeDtos[i].Location == ReaderIdEnum.WareHouseH.ToChineseString() ||
                    locationTimeDtos[i].Location == ReaderIdEnum.WareHouseI.ToChineseString())
                {
                    isArrivedWareHouse = true;

                    // Arrived at warehouse without arriving at production
                    if (isArrivedProduction == false)
                    {
                        state = EpcStateEnum.NG;
                    }

                    // Arrived at warehouse after arriving at terminal
                    if (isArrivedTerminal == true)
                    {
                        state = EpcStateEnum.Return;
                    }
                }
                else if (locationTimeDtos[i].Location == ReaderIdEnum.Terminal.ToChineseString() ||
                    locationTimeDtos[i].Location == ReaderIdEnum.ManualTerminal.ToChineseString())
                {
                    isArrivedTerminal = true;
                }
                else if (locationTimeDtos[i].Location == ReaderIdEnum.Handheld.ToChineseString())
                {
                    return EpcStateEnum.OK.ToString();
                }
            }
            return state.ToString();
        }

        private List<LocationTimeDto> GetLocationTimeDtos(List<EpcContext> epcContext)
        {
            List<LocationTimeDto> locationTimeDtos = new List<LocationTimeDto>();
            foreach (var epc in epcContext)
            {
                if (locationTimeDtos.Count == 0)
                {
                    locationTimeDtos.Insert(0, new LocationTimeDto()
                    {
                        Location = GetLocationString(epc.reader_id),
                        TransTime = GetDateTimeString(epc.timestamp),
                        DurationTime = "",
                    });
                }
                else
                {
                    LocationTimeDto lastLocationTimeDto = locationTimeDtos.First();
                    if (DateTime.Parse(epc.timestamp) == DateTime.Parse(lastLocationTimeDto.TransTime))
                    {
                        continue;
                    }
                    if (GetLocationString(epc.reader_id) != lastLocationTimeDto.Location)
                    {
                        DateTime lastTransTime = DateTime.Parse(lastLocationTimeDto.TransTime);
                        lastLocationTimeDto.DurationTime = GetTimeSpanString(DateTime.Parse(epc.timestamp).Subtract(lastTransTime));
                        locationTimeDtos.Insert(0, new LocationTimeDto
                        {
                            Location = GetLocationString(epc.reader_id),
                            TransTime = GetDateTimeString(epc.timestamp),
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

        private string GetDateTimeString(string dateTime)
        {
            return DateTime.Parse(dateTime).ToString("yyyy/MM/dd HH:mm:ss");
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

            // Check readerId is valid
            if (IsValidReaderId(epcPostDto.ReaderId) == false)
            {
                return GetPostResultDto(ResultEnum.False, ErrorEnum.InvalidReaderId);
            }


            EpcDataDto epcDataDto;
            // Check epc format is valid
            try
            {
                string asciiEpcString = GetHexToAscii(epcPostDto.Epc);
                if (string.IsNullOrEmpty(asciiEpcString) == true)
                {
                    return GetPostResultDto(ResultEnum.False, ErrorEnum.InvalidEpcFormat);
                }

                epcDataDto = GetEpcDataDto(asciiEpcString);
                if (epcDataDto == null)
                {
                    return GetPostResultDto(ResultEnum.False, ErrorEnum.InvalidEpcContextFormat);
                }
            }
            catch
            {
                return GetPostResultDto(ResultEnum.False, ErrorEnum.InvalidEpcFormat);
            }

            // Check epc is effective data
            var realTimeEpcContext = _localMemoryCache.ReadRealTimeEpcContext();
            if (realTimeEpcContext != null)
            {
                var sameRealTimeEpc = realTimeEpcContext.FirstOrDefault(epc => epc.epc == epcPostDto.Epc && epc.reader_id == epcPostDto.ReaderId);
                if (sameRealTimeEpc != null)
                {
                    return GetPostResultDto(ResultEnum.False, ErrorEnum.NoEffectiveData);
                }
            }

            // Check epc hasnt manually moved to terminal
            bool hasMovedToTerminal = _connection.QueryEpcContexts(epcPostDto.Epc, ReaderIdEnum.ManualTerminal.ToString()).Count() > 0;
            if (hasMovedToTerminal == true)
            {
                return GetPostResultDto(ResultEnum.False, ErrorEnum.DidMoveToTerminal);
            }

            // Insert into database in eaton_epc
            if (epcPostDto.ReaderId == ReaderIdEnum.ManualTerminal.ToString())
            {
                epcPostDto.TransTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            }
            bool result = _connection.InsertEpcContext(epcPostDto.Epc, epcPostDto.ReaderId, epcPostDto.TransTime);
            if (result == false)
            {
                return GetPostResultDto(ResultEnum.False, ErrorEnum.MsSqlTimeout);
            }

            // Insert into database in eaton_epc_data 
            EpcContext epcContext = _connection.QueryEpcContext(epcPostDto.Epc, epcPostDto.ReaderId, epcPostDto.TransTime);
            EpcDataContext epcDataContext = _connection.QueryEpcDataContext(epcDataDto.pallet_id);
            if (epcDataContext == null)
            {
                result = _connection.InsertEpcDataContext(epcContext.id, epcDataDto);
            }

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

        #endregion

        #region Delete

        public ResultDto Delete(string id)
        {
            if (string.IsNullOrEmpty(id) == true)
            {
                return EpcResultDto(ResultEnum.False, ErrorEnum.InvalidParameters);
            }

            if (int.TryParse(id, out int sid) == false)
            {
                return EpcResultDto(ResultEnum.False, ErrorEnum.InvalidParameters);
            }

            var result = _connection.DeleteEpcContext(sid);
            if (result == false)
            {
                return EpcResultDto(ResultEnum.False, ErrorEnum.MsSqlTimeout);
            }

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
