using EatonManagementBoard.Dtos;
using EatonManagementBoard.Enums;
using EatonManagementBoard.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Services
{
    public class EpcService
    {
        public EpcService(EatonManagementBoardDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly EatonManagementBoardDbContext _dbContext;

        public GetEpcResultDto Get(string wo = null, string pn = null, string palletId = null)
        {
            string allSqlCommand = "select * from scannel.dbo.eaton_epc;";
            string realTimeSqlCommand = "select * from eaton_epc result where " +
                "readerId = (select TOP(1) readerId from eaton_epc location where location.epc = result.epc and transTime = (select MAX(transTime) from eaton_epc maxtime where maxtime.epc = location.epc)) and " +
                "transTime = (select MIN(transTime) from eaton_epc mintime where mintime.epc = result.epc and mintime.readerId = result.readerId) and " +
                "Sid=(select MAX(Sid) from eaton_epc maxsid where maxsid.epc=result.epc and maxsid.readerId=result.readerId and maxsid.transTime=result.transTime) " +
                "order by result.transTime;";
            List<EatonEpc> dbEatonEpcs = _dbContext.EatonEpcs
                .FromSqlRaw(allSqlCommand)
                .ToList();
            List<EatonEpc> realTimeEatonEpcs = _dbContext.EatonEpcs
                .FromSqlRaw(realTimeSqlCommand)
                .ToList();
            List<EatonEpc> warehouseAEatonEpcs = realTimeEatonEpcs
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseA.ToString())
                .ToList();
            List<EatonEpc> warehouseBEatonEpcs = realTimeEatonEpcs
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseB.ToString())
                .ToList();
            List<EatonEpc> warehouseCEatonEpcs = realTimeEatonEpcs
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseC.ToString())
                .ToList();
            List<EatonEpc> warehouseDEatonEpcs = realTimeEatonEpcs
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseD.ToString())
                .ToList();
            List<EatonEpc> warehouseEEatonEpcs = realTimeEatonEpcs
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseE.ToString())
                .ToList();
            List<EatonEpc> warehouseFEatonEpcs = realTimeEatonEpcs
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseF.ToString())
                .ToList();
            List<EatonEpc> warehouseGEatonEpcs = realTimeEatonEpcs
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseG.ToString())
                .ToList();
            List<EatonEpc> warehouseHEatonEpcs = realTimeEatonEpcs
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseH.ToString())
                .ToList();
            List<EatonEpc> warehouseIEatonEpcs = realTimeEatonEpcs
                .Where(epc => epc.ReaderId == ReaderIdEnum.WareHouseI.ToString())
                .ToList();
            List<EatonEpc> elevatorEatonEpcs = realTimeEatonEpcs
                .Where(epc => epc.ReaderId == ReaderIdEnum.Elevator.ToString())
                .ToList();
            List<EatonEpc> secondFloorEatonEpcs = realTimeEatonEpcs
                .Where(epc => epc.ReaderId == ReaderIdEnum.SecondFloorA.ToString())
                .ToList();
            List<EatonEpc> thirdFloorAEatonEpcs = realTimeEatonEpcs
                .Where(epc => epc.ReaderId == ReaderIdEnum.ThirdFloorA.ToString())
                .ToList();
            List<EatonEpc> thirdFloorBEatonEpcs = realTimeEatonEpcs
                .Where(epc => epc.ReaderId == ReaderIdEnum.ThirdFloorB.ToString())
                .ToList();
            List<EatonEpc> terminalEatonEpcs = realTimeEatonEpcs
                .Where(epc => epc.ReaderId == ReaderIdEnum.Terminal.ToString() || epc.ReaderId == ReaderIdEnum.ManualTerminal.ToString())
                .ToList();

            List<EpcDto> warehouseAEpcDtos = GetEpcDtos(dbEatonEpcs, warehouseAEatonEpcs, wo, pn, palletId);
            List<EpcDto> warehouseBEpcDtos = GetEpcDtos(dbEatonEpcs, warehouseBEatonEpcs, wo, pn, palletId);
            List<EpcDto> warehouseCEpcDtos = GetEpcDtos(dbEatonEpcs, warehouseCEatonEpcs, wo, pn, palletId);
            List<EpcDto> warehouseDEpcDtos = GetEpcDtos(dbEatonEpcs, warehouseDEatonEpcs, wo, pn, palletId);
            List<EpcDto> warehouseEEpcDtos = GetEpcDtos(dbEatonEpcs, warehouseEEatonEpcs, wo, pn, palletId);
            List<EpcDto> warehouseFEpcDtos = GetEpcDtos(dbEatonEpcs, warehouseFEatonEpcs, wo, pn, palletId);
            List<EpcDto> warehouseGEpcDtos = GetEpcDtos(dbEatonEpcs, warehouseGEatonEpcs, wo, pn, palletId);
            List<EpcDto> warehouseHEpcDtos = GetEpcDtos(dbEatonEpcs, warehouseHEatonEpcs, wo, pn, palletId);
            List<EpcDto> warehouseIEpcDtos = GetEpcDtos(dbEatonEpcs, warehouseIEatonEpcs, wo, pn, palletId);
            List<EpcDto> elevatorEpcDtos = GetEpcDtos(dbEatonEpcs, elevatorEatonEpcs, wo, pn, palletId);
            List<EpcDto> secondFloorEpcDtos = GetEpcDtos(dbEatonEpcs, secondFloorEatonEpcs, wo, pn, palletId);
            List<EpcDto> thirdFloorAEpcDtos = GetEpcDtos(dbEatonEpcs, thirdFloorAEatonEpcs, wo, pn, palletId);
            List<EpcDto> thirdFloorBEpcDtos = GetEpcDtos(dbEatonEpcs, thirdFloorBEatonEpcs, wo, pn, palletId);
            List<EpcDto> terminalEpcDtos = GetEpcDtos(dbEatonEpcs, terminalEatonEpcs, wo, pn, palletId);
            List<EpcDto> allEpcDtos = GetEpcDtos(dbEatonEpcs, dbEatonEpcs, null, null, null);

            // Mark out manual terminal on transTime
            foreach (var terminalEpcDto in terminalEpcDtos)
            {
                if (terminalEpcDto.ReaderId == ReaderIdEnum.ManualTerminal.ToString())
                {
                    terminalEpcDto.TransTime += " M";
                }
            }

            DashboardDto dashboardDto = GetDashboardDto(
                warehouseAEpcDtos,
                warehouseBEpcDtos,
                warehouseCEpcDtos,
                warehouseDEpcDtos,
                warehouseEEpcDtos,
                warehouseFEpcDtos,
                warehouseGEpcDtos,
                warehouseHEpcDtos,
                warehouseIEpcDtos,
                elevatorEpcDtos,
                secondFloorEpcDtos,
                thirdFloorAEpcDtos,
                thirdFloorBEpcDtos,
                terminalEpcDtos
                );

            SelectionDto selectionDto = GetSelectionDtos(allEpcDtos);

            return GetEpcGetResultDto(ResultEnum.True, ErrorEnum.None, dashboardDto, selectionDto);
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
            for (int i = 0; i < hexString.Length; i += 2)
            {
                string tempString = hexString.Substring(i, 2);
                asciiString += Convert.ToChar(Convert.ToUInt32(tempString, 16));
            }
            return asciiString;
        }

        private DashboardDto GetDashboardDto(List<EpcDto> warehouseAEpcDtos,
            List<EpcDto> warehouseBEpcDtos,
            List<EpcDto> warehouseCEpcDtos,
            List<EpcDto> warehouseDEpcDtos,
            List<EpcDto> warehouseEEpcDtos,
            List<EpcDto> warehouseFEpcDtos,
            List<EpcDto> warehouseGEpcDtos,
            List<EpcDto> warehouseHEpcDtos,
            List<EpcDto> warehouseIEpcDtos,
            List<EpcDto> elevatorEpcDtos,
            List<EpcDto> secondFloorEpcDtos,
            List<EpcDto> thirdFloorAEpcDtos,
            List<EpcDto> thirdFloorBEpcDtos,
            List<EpcDto> terminalEpcDtos
            )
        {
            return new DashboardDto
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
                TerminalEpcDtos = terminalEpcDtos
            };
        }

        private List<EpcDto> GetEpcDtos(List<EatonEpc> dbEatonEpcs, List<EatonEpc> eatonEpcDtos, string woString, string pnString, string palletIdString)
        {
            List<EpcDto> epcDtos = new List<EpcDto>();
            foreach (var eatonEpcDto in eatonEpcDtos)
            {
                string wo = "";
                string qty = "";
                string pn = "";
                string line = "";
                string barcode = "";
                string error = "";
                string epcString = GetHexToAscii(eatonEpcDto.Epc);
                bool isNewEpcStringFormat = false;
                // Detect epc string format is new or old
                isNewEpcStringFormat = epcString.Contains("##") == false && epcString.Contains("&&") == false ? true : false;
                if (isNewEpcStringFormat == false)
                {
                    // Old epc string format
                    // Error string format
                    if (epcString.Contains("##") == false ||
                        epcString.Split("##").Count() == 0 ||
                        epcString.Split("##")[1].Contains("&&") == false ||
                        epcString.Split("##")[1].Split("&&").Count() != 5)
                    {
                        continue;
                    }
                    // Correct string format
                    wo = epcString.Split("##")[1].Split("&&")[0];
                    qty = epcString.Split("##")[1].Split("&&")[1];
                    pn = epcString.Split("##")[1].Split("&&")[2];
                    line = epcString.Split("##")[1].Split("&&")[3];
                    barcode = epcString.Split("##")[1].Split("&&")[4];
                    error = "";
                }
                else
                {
                    // New epc string format
                    // Error string format
                    if (epcString.Contains("#") == false ||
                        epcString.Split("#").Count() == 0 ||
                        epcString.Split("#")[1].Contains("&") == false ||
                        epcString.Split("#")[1].Split("&").Count() != 5)
                    {
                        continue;
                    }
                    // Correct string format
                    wo = epcString.Split("#")[1].Split("&")[0];
                    qty = epcString.Split("#")[1].Split("&")[1];
                    pn = epcString.Split("#")[1].Split("&")[2];
                    line = epcString.Split("#")[1].Split("&")[3];
                    barcode = epcString.Split("#")[1].Split("&")[4];
                    error = "";
                }
                List<EatonEpc> dbSameEpcs = dbEatonEpcs
                    .Where(dbEatonEpc => dbEatonEpc.Epc == eatonEpcDto.Epc)
                    .OrderBy(dbEatonEpc => dbEatonEpc.TransTime)
                    .ToList();
                List<LocationTimeDto> locationTimeDtos = GetLocationTimeDtos(dbSameEpcs);
                string epcState = GetEpcState(Enumerable.Reverse(locationTimeDtos).ToList());
                if ((string.IsNullOrEmpty(woString) && string.IsNullOrEmpty(pnString) && string.IsNullOrEmpty(palletIdString)) ||
                    (!string.IsNullOrEmpty(woString) && string.IsNullOrEmpty(pnString) && string.IsNullOrEmpty(palletIdString) && wo == woString) ||
                    (string.IsNullOrEmpty(woString) && !string.IsNullOrEmpty(pnString) && string.IsNullOrEmpty(palletIdString) && pn == pnString) ||
                    (string.IsNullOrEmpty(woString) && string.IsNullOrEmpty(pnString) && !string.IsNullOrEmpty(palletIdString) && barcode == palletIdString) ||
                    (!string.IsNullOrEmpty(woString) && string.IsNullOrEmpty(pnString) && !string.IsNullOrEmpty(palletIdString) && wo == woString && barcode == palletIdString) ||
                    (!string.IsNullOrEmpty(woString) && !string.IsNullOrEmpty(pnString) && string.IsNullOrEmpty(palletIdString) && wo == woString && pn == pnString) ||
                    (string.IsNullOrEmpty(woString) && !string.IsNullOrEmpty(pnString) && !string.IsNullOrEmpty(palletIdString) && pn == pnString && barcode == palletIdString) ||
                    (!string.IsNullOrEmpty(woString) && !string.IsNullOrEmpty(pnString) && !string.IsNullOrEmpty(palletIdString) && wo == woString && pn == pnString && barcode == palletIdString))
                {
                    // Without parameters
                    // With wo
                    // With pn
                    // With barcode
                    // With wo, barcode
                    // With wo, pn
                    // With pn, barcode
                    // With wo, pn, barcode
                    string transTime = GetDateTimeString(eatonEpcDto.TransTime.Value);
                    epcDtos.Add(new EpcDto
                    {
                        Epc = eatonEpcDto.Epc,
                        ReaderId = eatonEpcDto.ReaderId,
                        TransTime = transTime,
                        Wo = wo,
                        Qty = qty,
                        Pn = pn,
                        Line = line,
                        Barcode = barcode,
                        Error = error,
                        LocationTimeDtos = locationTimeDtos,
                        EpcState = epcState,
                    });
                }
            }
            epcDtos = epcDtos.OrderByDescending(epcDto => DateTime.Parse(epcDto.TransTime)).ToList();
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
                            locationTimeDto.Location == ReaderIdEnum.ThirdFloorB.ToChineseString())
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

        private List<LocationTimeDto> GetLocationTimeDtos(List<EatonEpc> dbSameEpcs)
        {
            List<LocationTimeDto> locationTimeDtos = new List<LocationTimeDto>();
            foreach (var dbSameEpc in dbSameEpcs)
            {
                if (locationTimeDtos.Count == 0)
                {
                    locationTimeDtos.Insert(0, new LocationTimeDto()
                    {
                        Location = GetLocationString(dbSameEpc.ReaderId),
                        TransTime = GetDateTimeString(dbSameEpc.TransTime.GetValueOrDefault()),
                        DurationTime = "",
                    });
                }
                else
                {
                    LocationTimeDto lastLocationTimeDto = locationTimeDtos.First();
                    if (GetLocationString(dbSameEpc.ReaderId) != lastLocationTimeDto.Location)
                    {
                        DateTime lastTransTime = DateTime.Parse(lastLocationTimeDto.TransTime);
                        lastLocationTimeDto.DurationTime = GetTimeSpanString(dbSameEpc.TransTime.GetValueOrDefault().Subtract(lastTransTime));
                        locationTimeDtos.Insert(0, new LocationTimeDto
                        {
                            Location = GetLocationString(dbSameEpc.ReaderId),
                            TransTime = GetDateTimeString(dbSameEpc.TransTime.GetValueOrDefault()),
                            DurationTime = "",
                        });
                    }
                }
            }
            return locationTimeDtos;
        }

        private SelectionDto GetSelectionDtos(List<EpcDto> epcDtos)
        {
            var woEpcs = epcDtos
                .GroupBy(epc => epc.Wo)
                .Select(epcDto => new { key = epcDto.Key, wo = epcDto.Select(epc => epc.Wo) })
                .ToList();
            var pnEpcs = epcDtos
                .GroupBy(epc => epc.Pn)
                .Select(epcDto => new { key = epcDto.Key, pn = epcDto.Select(epc => epc.Pn) })
                .ToList();
            var palletIdEpcs = epcDtos
                .GroupBy(epc => epc.Barcode)
                .Select(epcDto => new { key = epcDto.Key, palletId = epcDto.Select(epc => epc.Barcode) })
                .ToList();
            SelectionDto selectionDto = new SelectionDto()
            {
                Wos = new List<string>(),
                Pns = new List<string>(),
                PalletIds = new List<string>()
            };
            foreach (var woEpc in woEpcs)
            {
                selectionDto.Wos.Add(woEpc.wo.First());
            }
            foreach (var pnEpc in pnEpcs)
            {
                selectionDto.Pns.Add(pnEpc.pn.First());
            }
            foreach (var palletIdEpc in palletIdEpcs)
            {
                selectionDto.PalletIds.Add(palletIdEpc.palletId.First());
            }
            selectionDto.Wos = selectionDto.Wos.OrderBy(wo => wo).ToList();
            selectionDto.Pns = selectionDto.Pns.OrderBy(pn => pn).ToList();
            selectionDto.PalletIds = selectionDto.PalletIds.OrderBy(palletId => palletId).ToList();
            return selectionDto;
        }

        private GetEpcResultDto GetEpcGetResultDto(ResultEnum result, ErrorEnum error, DashboardDto dashboardDto, SelectionDto selectionDto)
        {
            return new GetEpcResultDto
            {
                Result = result.ToBoolean(),
                Error = error.ToString(),
                DashboardDto = dashboardDto,
                SelectionDto = selectionDto
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
                default:
                    return "";

            }
        }

        public EpcResultDto Post(string epc)
        {
            if (string.IsNullOrEmpty(epc) == true)
            {
                return GetEpcResultDto(ResultEnum.False, ErrorEnum.InvalidParameters);
            }

            EatonEpc eatonEpc = _dbContext.EatonEpcs
                .Where(eatonEpc => eatonEpc.Epc == epc)
                .FirstOrDefault();

            if (eatonEpc == null)
            {
                return GetEpcResultDto(ResultEnum.False, ErrorEnum.InvalidParameters);
            }

            EatonEpc insertEatonEpc = GetEatonEpc(epc);
            _dbContext.EatonEpcs.Add(insertEatonEpc);
            _dbContext.SaveChanges();

            return GetEpcResultDto(ResultEnum.True, ErrorEnum.None);
        }

        private EpcResultDto GetEpcResultDto(ResultEnum result, ErrorEnum error)
        {
            return new EpcResultDto
            {
                Result = result.ToBoolean(),
                Error = error.ToString()
            };
        }

        private EatonEpc GetEatonEpc(string epc)
        {
            return new EatonEpc
            {
                Epc = epc,
                ReaderId = ReaderIdEnum.ManualTerminal.ToString(),
                TransTime = DateTime.Now,
            };
        }

        public EpcResultDto Delete(string epc)
        {
            if (string.IsNullOrEmpty(epc) == true)
            {
                return GetEpcResultDto(ResultEnum.False, ErrorEnum.InvalidParameters);
            }

            EatonEpc eatonEpc = _dbContext.EatonEpcs
                .Where(eatonEpc => eatonEpc.Epc == epc && eatonEpc.ReaderId == ReaderIdEnum.ManualTerminal.ToString())
                .FirstOrDefault();

            if (eatonEpc == null)
            {
                return GetEpcResultDto(ResultEnum.False, ErrorEnum.InvalidParameters);
            }

            _dbContext.EatonEpcs.Remove(eatonEpc);
            _dbContext.SaveChanges();

            return GetEpcResultDto(ResultEnum.True, ErrorEnum.None);
        }
    }
}
