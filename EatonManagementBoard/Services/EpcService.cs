using EatonManagementBoard.Database;
using EatonManagementBoard.Database.Dapper;
using EatonManagementBoard.Dtos;
using EatonManagementBoard.Enums;
using EatonManagementBoard.HttpClients;
using EatonManagementBoard.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LocalMemoryCache = EatonManagementBoard.Database.LocalMemoryCache;

namespace EatonManagementBoard.Services
{
    public class EpcService
    {
        public EpcService(ConnectionRepositoryManager manager, IMemoryCache memoryCache, IHttpClientFactory httpClientFactory)
        {
            _manager = manager;
            _localMemoryCache = new LocalMemoryCache(memoryCache);
            _httpClientManager = new HttpClientManager(httpClientFactory);
        }

        private readonly ConnectionRepositoryManager _manager;
        private readonly HttpClientManager _httpClientManager;
        private readonly LocalMemoryCache _localMemoryCache;
        private readonly string doubleHash = "##";
        private readonly string doubleAnd = "&&";
        private readonly string singleHash = "#";
        private readonly string singleAnd = "&";
        private readonly int woSize = 8;
        private readonly int qtySize = 3;
        private readonly int pnSize = 20;
        private readonly int lineSize = 3;
        private readonly int palletIdSize = 10;

        #region Public

        public RtcResultDto GetRtc()
        {
            return GetRtcResultDto(ResultEnum.True, ErrorEnum.None, DateTime.Now);
        }

        public EpcResultDto Get(string wo = null, string pn = null, string palletId = null)
        {
            // Check value
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

            using (var connection = _manager.MsSqlConnectionRepository.InitConnection())
            {
                connection.Open();

                using (var transaction = _manager.MsSqlConnectionRepository.BeginTransaction())
                {
                    try
                    {
                        // Check there were new data inserted in database
                        var epcCount = _manager.QueryEpcCount();
                        if (epcCount == 0)
                        {
                            // Commit the transaction if everything is successful
                            transaction.Commit();

                            return GetEpcGetResultDto(ResultEnum.True, ErrorEnum.None.ToDescription(), new DashboardDto());
                        }

                        // Determine to query data from database
                        var cacheEpcCount = _localMemoryCache.ReadEpcCount();
                        var cacheSearchState = _localMemoryCache.ReadSearchState();
                        if (cacheEpcCount == epcCount && cacheSearchState == isSearchState)
                        {
                            // Same data count in database so return local memory cache
                            var dashboard = _localMemoryCache.ReadDashboardDto();

                            // Commit the transaction if everything is successful
                            transaction.Commit();

                            return GetEpcGetResultDto(ResultEnum.True, ErrorEnum.None.ToDescription(), dashboard);
                        }

                        // Connect to database
                        List<EpcRawContext> realTimeEpcRawContext = _manager.QueryRealTimeEpcRawContext();
                        List<EpcRawContext> historyEpcRawContext = _manager.QueryHistoryEpcRawContext();

                        // Check database were throw timeout
                        if (realTimeEpcRawContext == null || historyEpcRawContext == null)
                        {
                            throw new Exception(ErrorEnum.FailToAccessDatabase.ToDescription());
                        }

                        // Get epc context in each location
                        List<EpcRawContext> warehouseAEpcRawContext = realTimeEpcRawContext.Where(epc => epc.reader_id == ReaderIdEnum.WareHouseA.ToString()).ToList();
                        List<EpcRawContext> warehouseBEpcRawContext = realTimeEpcRawContext.Where(epc => epc.reader_id == ReaderIdEnum.WareHouseB.ToString()).ToList();
                        List<EpcRawContext> warehouseCEpcRawContext = realTimeEpcRawContext.Where(epc => epc.reader_id == ReaderIdEnum.WareHouseC.ToString()).ToList();
                        List<EpcRawContext> warehouseDEpcRawContext = realTimeEpcRawContext.Where(epc => epc.reader_id == ReaderIdEnum.WareHouseD.ToString()).ToList();
                        List<EpcRawContext> warehouseEEpcRawContext = realTimeEpcRawContext.Where(epc => epc.reader_id == ReaderIdEnum.WareHouseE.ToString()).ToList();
                        List<EpcRawContext> warehouseFEpcRawContext = realTimeEpcRawContext.Where(epc => epc.reader_id == ReaderIdEnum.WareHouseF.ToString()).ToList();
                        List<EpcRawContext> warehouseGEpcRawContext = realTimeEpcRawContext.Where(epc => epc.reader_id == ReaderIdEnum.WareHouseG.ToString()).ToList();
                        List<EpcRawContext> warehouseHEpcRawContext = realTimeEpcRawContext.Where(epc => epc.reader_id == ReaderIdEnum.WareHouseH.ToString()).ToList();
                        List<EpcRawContext> warehouseIEpcRawContext = realTimeEpcRawContext.Where(epc => epc.reader_id == ReaderIdEnum.WareHouseI.ToString()).ToList();
                        List<EpcRawContext> elevatorEpcRawContext = realTimeEpcRawContext.Where(epc => epc.reader_id == ReaderIdEnum.Elevator.ToString()).ToList();
                        List<EpcRawContext> secondFloorEpcRawContext = realTimeEpcRawContext.Where(epc => epc.reader_id == ReaderIdEnum.SecondFloorA.ToString()).ToList();
                        List<EpcRawContext> thirdFloorAEpcRawContext = realTimeEpcRawContext.Where(epc => epc.reader_id == ReaderIdEnum.ThirdFloorA.ToString()).ToList();
                        List<EpcRawContext> thirdFloorBEpcRawContext = realTimeEpcRawContext.Where(epc => epc.reader_id == ReaderIdEnum.ThirdFloorB.ToString()).ToList();
                        List<EpcRawContext> terminalEpcRawContext = realTimeEpcRawContext.Where(
                            epc => epc.reader_id == ReaderIdEnum.Terminal.ToString() ||
                            epc.reader_id == ReaderIdEnum.TerminalLeft.ToString() ||
                            epc.reader_id == ReaderIdEnum.TerminalRight.ToString() ||
                            epc.reader_id == ReaderIdEnum.ManualTerminal.ToString()).ToList();
                        List<EpcRawContext> handheldEpcRawContext = realTimeEpcRawContext.Where(epc => epc.reader_id == ReaderIdEnum.Handheld.ToString()).ToList();

                        List<EpcDto> warehouseAEpcDtos = GetEpcDtos(ref historyEpcRawContext, ref warehouseAEpcRawContext, wo, pn, palletId);
                        List<EpcDto> warehouseBEpcDtos = GetEpcDtos(ref historyEpcRawContext, ref warehouseBEpcRawContext, wo, pn, palletId);
                        List<EpcDto> warehouseCEpcDtos = GetEpcDtos(ref historyEpcRawContext, ref warehouseCEpcRawContext, wo, pn, palletId);
                        List<EpcDto> warehouseDEpcDtos = GetEpcDtos(ref historyEpcRawContext, ref warehouseDEpcRawContext, wo, pn, palletId);
                        List<EpcDto> warehouseEEpcDtos = GetEpcDtos(ref historyEpcRawContext, ref warehouseEEpcRawContext, wo, pn, palletId);
                        List<EpcDto> warehouseFEpcDtos = GetEpcDtos(ref historyEpcRawContext, ref warehouseFEpcRawContext, wo, pn, palletId);
                        List<EpcDto> warehouseGEpcDtos = GetEpcDtos(ref historyEpcRawContext, ref warehouseGEpcRawContext, wo, pn, palletId);
                        List<EpcDto> warehouseHEpcDtos = GetEpcDtos(ref historyEpcRawContext, ref warehouseHEpcRawContext, wo, pn, palletId);
                        List<EpcDto> warehouseIEpcDtos = GetEpcDtos(ref historyEpcRawContext, ref warehouseIEpcRawContext, wo, pn, palletId);
                        List<EpcDto> elevatorEpcDtos = GetEpcDtos(ref historyEpcRawContext, ref elevatorEpcRawContext, wo, pn, palletId);
                        List<EpcDto> secondFloorEpcDtos = GetEpcDtos(ref historyEpcRawContext, ref secondFloorEpcRawContext, wo, pn, palletId);
                        List<EpcDto> thirdFloorAEpcDtos = GetEpcDtos(ref historyEpcRawContext, ref thirdFloorAEpcRawContext, wo, pn, palletId);
                        List<EpcDto> thirdFloorBEpcDtos = GetEpcDtos(ref historyEpcRawContext, ref thirdFloorBEpcRawContext, wo, pn, palletId);
                        List<EpcDto> terminalEpcDtos = GetEpcDtos(ref historyEpcRawContext, ref terminalEpcRawContext, wo, pn, palletId);
                        List<EpcDto> handheldEpcDtos = GetEpcDtos(ref historyEpcRawContext, ref handheldEpcRawContext, wo, pn, palletId);

                        DashboardDto dashboardDto = new DashboardDto()
                        {
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

                        // Memory cache
                        _localMemoryCache.SaveSearchState(isSearchState);
                        _localMemoryCache.SaveEpcCount(epcCount);
                        _localMemoryCache.SaveRealTimeEpcRawContext(realTimeEpcRawContext);
                        _localMemoryCache.SaveHistoryEpcRawContext(historyEpcRawContext);
                        _localMemoryCache.SaveDashboardDto(dashboardDto);

                        // Commit the transaction if everything is successful
                        transaction.Commit();

                        return GetEpcGetResultDto(ResultEnum.True, ErrorEnum.None.ToDescription(), dashboardDto);
                    }
                    catch (Exception exp)
                    {
                        // Handle exceptions and optionally roll back the transaction
                        transaction.Rollback();
                        return GetEpcGetResultDto(ResultEnum.False, exp.Message, null);
                    }
                }
            }
        }

        public IResultDto PostAsync(dynamic value)
        {
            // Time and refused
            DateTime now = DateTime.Now;
            if  (now.Hour > 19 && now.Hour < 5)
            {
                return GetPostResultDto(ResultEnum.False, "SLEEP");
            }

            EpcPostDto dto;

            // Check value
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,
                    MissingMemberHandling = MissingMemberHandling.Error
                };
                dto = JsonConvert.DeserializeObject<EpcPostDto>(value.ToString(), settings);
            }
            catch (Exception exp)
            {
                return GetPostResultDto(ResultEnum.False, exp.Message);
            }

            using (var connection = _manager.MsSqlConnectionRepository.InitConnection())
            {
                connection.Open();

                using (var transaction = _manager.MsSqlConnectionRepository.BeginTransaction())
                {
                    try
                    {
                        // Check readerId is valid
                        if (IsValidReaderId(dto.ReaderId) == false)
                        {
                            throw new Exception(ErrorEnum.InvalidReaderId.ToDescription());
                        }

                        // Check epc format is valid
                        // Convert hex string to ascii string
                        string asciiEpcString = GetHexToAscii(dto.Epc);
                        if (string.IsNullOrEmpty(asciiEpcString) == true)
                        {
                            throw new Exception(ErrorEnum.InvalidEpcFormat.ToDescription());
                        }

                        // Get EpcDataDto
                        EpcDataDto epcDataDto = GetEpcDataDto(asciiEpcString);
                        if (epcDataDto == null)
                        {
                            throw new Exception(ErrorEnum.InvalidEpcContextFormat.ToDescription());
                        }
                        bool isTest = epcDataDto.pn.Contains("test") || epcDataDto.pallet_id.Contains("test") ? true : false;

                        // Check readerId is from terminal and it is delivery time
                        if (dto.ReaderId == ReaderIdEnum.Terminal.ToString() ||
                            dto.ReaderId == ReaderIdEnum.TerminalLeft.ToString() ||
                            dto.ReaderId == ReaderIdEnum.TerminalRight.ToString())
                        {
                            int deliveringCount = _manager.QueryDeliveryingNumberContextsCount();
                            if (deliveringCount <= 0 && isTest == false)
                            {
                                throw new Exception(ErrorEnum.NotDuringDeliverying.ToDescription());
                            }
                        }

                        // Check epc is effective data
                        var realTimeEpcRawContext = _manager.QueryRealTimeEpcRawContext();
                        if (realTimeEpcRawContext != null)
                        {
                            var sameRealTimeEpcRawContext = realTimeEpcRawContext.FirstOrDefault(epc => epc.epc == dto.Epc);
                            if (sameRealTimeEpcRawContext != null)
                            {
                                if (IsDuplicatedEpcFromSameReader(dto.ReaderId, sameRealTimeEpcRawContext.reader_id) == true && isTest == false)
                                {
                                    throw new Exception(ErrorEnum.NoEffectiveData.ToDescription());
                                }
                            }
                        }

                        // Check epc hasnt manually moved to terminal
                        bool hasMovedToTerminal = _manager.QueryEpcRawContexts(dto.Epc, ReaderIdEnum.ManualTerminal.ToString()).Count() > 0;
                        if (hasMovedToTerminal == true)
                        {
                            throw new Exception(ErrorEnum.DidMoveToTerminal.ToDescription());
                        }

                        // Insert into [eaton_epc_raw]
                        UpdateTransTimeEpcPost(ref dto);
                        bool result = _manager.InsertEpcRawContext(dto);
                        if (result == false)
                        {
                            throw new Exception(ErrorEnum.FailToAccessDatabase.ToDescription());
                        }

                        // Insert into [eaton_epc_data]
                        EpcRawContext epcRawContext = _manager.QueryEpcRawContext(dto);
                        EpcDataContext epcDataContext = _manager.QueryEpcDataContextByEpcDataDto(epcDataDto);
                        if (epcDataContext == null)
                        {
                            // Insert into [eaton_epc_data]
                            result = _manager.InsertEpcDataContext(epcRawContext.id, epcDataDto);
                            if (result == false)
                            {
                                throw new Exception(ErrorEnum.FailToAccessDatabase.ToDescription());
                            }
                            epcDataContext = _manager.QueryEpcDataContextByEpcDataDto(epcDataDto);
                        }
                        else
                        {
                            // Update f_epc_raw_ids 
                            result = _manager.UpdateEpcDataContext(epcRawContext.id, epcDataContext.pallet_id);
                        }

                        // Call api for delivery
                        if (dto.ReaderId == ReaderIdEnum.Terminal.ToString() ||
                            dto.ReaderId == ReaderIdEnum.TerminalLeft.ToString() ||
                            dto.ReaderId == ReaderIdEnum.TerminalRight.ToString())
                        {
                            result = _httpClientManager.PostToServerWithDeliveryTerminal(epcRawContext, epcDataContext);
                        }

                        // Commit the transaction if everything is successful
                        transaction.Commit();

                        return GetPostResultDto(ResultEnum.True, ErrorEnum.None.ToDescription());
                    }
                    catch (Exception exp)
                    {
                        // Handle exceptions and optionally roll back the transaction
                        transaction.Rollback();
                        return GetPostResultDto(ResultEnum.False, exp.Message);
                    }
                }
            }
        }

        public ResultDto Delete(string id)
        {

            if (string.IsNullOrEmpty(id) == true)
            {
                return EpcResultDto(ResultEnum.False, ErrorEnum.InvalidParameters.ToDescription());
            }

            if (int.TryParse(id, out int sid) == false)
            {
                return EpcResultDto(ResultEnum.False, ErrorEnum.InvalidParameters.ToDescription());
            }

            using (var connection = _manager.MsSqlConnectionRepository.InitConnection())
            {
                connection.Open();

                using (var transaction = _manager.MsSqlConnectionRepository.BeginTransaction())
                {
                    try
                    {
                        var result = _manager.DeleteEpcRawContext(sid);
                        if (result == false)
                        {
                            throw new Exception(ErrorEnum.FailToAccessDatabase.ToDescription());
                        }

                        // Commit the transaction if everything is successful
                        transaction.Commit();

                        return EpcResultDto(ResultEnum.True, ErrorEnum.None.ToDescription());
                    }
                    catch (Exception exp)
                    {
                        // Handle exceptions and optionally roll back the transaction
                        transaction.Rollback();
                        return EpcResultDto(ResultEnum.False, exp.Message);
                    }
                }
            }
        }

        #endregion

        #region Private


        private void UpdateTransTimeEpcPost(ref EpcPostDto dto)
        {
            dto.TransTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private bool IsDuplicatedEpcFromSameReader(string insertReaderId, string exsitedReaderId)
        {
            if (exsitedReaderId == ReaderIdEnum.Terminal.ToString() || exsitedReaderId == ReaderIdEnum.TerminalLeft.ToString() || exsitedReaderId == ReaderIdEnum.TerminalRight.ToString())
            {
                if (insertReaderId == ReaderIdEnum.Terminal.ToString() || insertReaderId == ReaderIdEnum.TerminalLeft.ToString() || insertReaderId == ReaderIdEnum.TerminalRight.ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return insertReaderId == exsitedReaderId ? true : false;
            }
        }

        private bool IsMatchGarbledString(string epcString)
        {
            // Check garbled text
            string pattern = @"^[a-zA-Z0-9&#-]+$";
            bool isMatch = Regex.IsMatch(epcString, pattern);
            return !isMatch;
        }

        private string GetHexToAscii(string hexString)
        {

            // Return if hexString is ascii string already
            if (hexString.Contains(singleHash) == true &&
                hexString.Contains(singleAnd) == true)
            {
                return hexString;
            }

            // Return if hexString is null or hexString is not double characters
            if (string.IsNullOrEmpty(hexString) == true || hexString.Count() % 2 != 0)
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

                    // Select and check the middle section string
                    var newEpcFormats = asciiEpcString.Split(singleHash);
                    if (newEpcFormats[1].Split(singleAnd).Count() != 5)
                    {
                        return null;
                    }

                    // Check epc context format from the middle section string
                    var properties = newEpcFormats[1].Split(singleAnd);
                    if (properties[0].Length > woSize
                        || properties[1].Length > qtySize
                        || properties[2].Length > pnSize
                        || properties[3].Length > lineSize
                        || properties[4].Length > palletIdSize)
                    {
                        return null;
                    }

                    // Check that all properties are not garbled text
                    foreach(var property in properties)
                    {
                        if (IsMatchGarbledString(property))
                        {
                            return null;
                        }
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
                else if (isOldEpcFormat == true)
                {
                    // Old epc string format

                    // Select and check the middle section string
                    var oldEpcFormats = asciiEpcString.Split(doubleHash);
                    if (oldEpcFormats[1].Split(doubleAnd).Count() != 5)
                    {
                        return null;
                    }

                    // Check epc context format from the middle section string
                    var properties = oldEpcFormats[1].Split(doubleAnd);
                    if (properties[0].Length > woSize
                        || properties[1].Length > qtySize
                        || properties[2].Length > pnSize
                        || properties[3].Length > lineSize
                        || properties[4].Length > palletIdSize)
                    {
                        return null;
                    }

                    // Check that all properties are not garbled text
                    foreach (var property in properties)
                    {
                        if (IsMatchGarbledString(property))
                        {
                            return null;
                        }
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

        private List<EpcDto> GetEpcDtos(ref List<EpcRawContext> historyEpcRawContext, ref List<EpcRawContext> realTimeEpcRawContext, string woString, string pnString, string palletIdString)
        {
            List<EpcDto> epcDtos = new List<EpcDto>();
            foreach (var realTimeEpcRaw in realTimeEpcRawContext)
            {
                string asciiEpcString = GetHexToAscii(realTimeEpcRaw.epc);
                if (string.IsNullOrEmpty(asciiEpcString) == true)
                {
                    continue;
                }

                EpcDataDto epcDataDto = GetEpcDataDto(asciiEpcString);
                if (epcDataDto == null)
                {
                    continue;
                }

                EpcRawContext epcRawContext = GetEpcRawContext(realTimeEpcRaw);

                List<EpcRawContext> tracedEpcRawContext = historyEpcRawContext.Where(tracedEpcRaw => tracedEpcRaw.epc == realTimeEpcRaw.epc).ToList();
                List<LocationTimeDto> locationTimeDtos = GetLocationTimeDtos(tracedEpcRawContext);

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
                        EpcContext = epcRawContext,
                        EpcDataDto = epcDataDto,
                        LocationTimeDtos = locationTimeDtos,
                        EpcState = epcState,
                    });
                }
            }
            return epcDtos;
        }

        private EpcRawContext GetEpcRawContext(EpcRawContext context)
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
                        //state = EpcStateEnum.NG;
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
                        //state = EpcStateEnum.NG;
                    }

                    // Arrived at warehouse after arriving at terminal
                    if (isArrivedTerminal == true)
                    {
                        state = EpcStateEnum.Return;
                    }
                }
                else if (locationTimeDtos[i].Location == ReaderIdEnum.Terminal.ToChineseString() ||
                    locationTimeDtos[i].Location == ReaderIdEnum.TerminalLeft.ToChineseString() ||
                    locationTimeDtos[i].Location == ReaderIdEnum.TerminalRight.ToChineseString() ||
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

        private List<LocationTimeDto> GetLocationTimeDtos(List<EpcRawContext> contexts)
        {
            List<LocationTimeDto> locationTimeDtos = new List<LocationTimeDto>();
            foreach (var context in contexts)
            {
                if (locationTimeDtos.Count == 0)
                {
                    locationTimeDtos.Insert(0, new LocationTimeDto()
                    {
                        Location = GetLocationString(context.reader_id),
                        TransTime = GetDateTimeString(context.timestamp),
                        DurationTime = "",
                    });
                }
                else
                {
                    LocationTimeDto lastLocationTimeDto = locationTimeDtos.First();
                    if (DateTime.Parse(context.timestamp) == DateTime.Parse(lastLocationTimeDto.TransTime))
                    {
                        continue;
                    }
                    if (GetLocationString(context.reader_id) != lastLocationTimeDto.Location)
                    {
                        DateTime lastTransTime = DateTime.Parse(lastLocationTimeDto.TransTime);
                        lastLocationTimeDto.DurationTime = GetTimeSpanString(DateTime.Parse(context.timestamp).Subtract(lastTransTime));
                        locationTimeDtos.Insert(0, new LocationTimeDto
                        {
                            Location = GetLocationString(context.reader_id),
                            TransTime = GetDateTimeString(context.timestamp),
                            DurationTime = "",
                        });
                    }
                }
            }
            return locationTimeDtos;
        }

        private EpcResultDto GetEpcGetResultDto(ResultEnum result, string error, DashboardDto dashboardDto)
        {
            return new EpcResultDto
            {
                Result = result.ToBoolean(),
                Error = error,
                DashboardDto = dashboardDto,
            };
        }

        private string GetDateTimeString(string dateTime)
        {
            return DateTime.Parse(dateTime).ToString("yyyy-MM-dd HH:mm:ss");
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

        private RtcResultDto GetRtcResultDto(ResultEnum result, ErrorEnum error, DateTime timestamp)
        {
            return new RtcResultDto
            {
                Result = result.ToBoolean(),
                Error = error.ToDescription(),
                Timestamp = timestamp.ToString("yyyy/MM/dd HH:mm:ss"),
            };
        }

        private IResultDto GetPostResultDto(ResultEnum result, string error)
        {
            return new ResultDto
            {
                Result = result.ToBoolean(),
                Error = error,
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
                ReaderIdEnum.TerminalLeft.ToString() == readerId ||
                ReaderIdEnum.TerminalRight.ToString() == readerId ||
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

        private ResultDto EpcResultDto(ResultEnum result, string error)
        {
            return new ResultDto
            {
                Result = result.ToBoolean(),
                Error = error,
            };
        }

        #endregion
    }
}
