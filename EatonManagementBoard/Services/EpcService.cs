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

        public EpcGetResultDto Get(string wo = null, string pn = null, string palletId = null)
        {
            // Without parameters
            // Valid
            List<EatonEpc> warehouseAEatonEpcs = _dbContext.EatonEpcs
                .FromSqlRaw("select Sid, epc, readerId, transTime from scannel.dbo.eaton_epc table1 where transTime=(select Max(table2.transTime) from scannel.dbo.eaton_epc table2 where table1.epc=table2.epc) and readerId={0}"
                , ReaderIdEnum.WareHouseA.ToString())
                .ToList();
            List<EatonEpc> warehouseBEatonEpcs = _dbContext.EatonEpcs
                .FromSqlRaw("select Sid, epc, readerId, transTime from scannel.dbo.eaton_epc table1 where transTime=(select Max(table2.transTime) from scannel.dbo.eaton_epc table2 where table1.epc=table2.epc) and readerId={0}"
                , ReaderIdEnum.WareHouseB.ToString())
                .ToList();
            List<EatonEpc> warehouseCEatonEpcs = _dbContext.EatonEpcs
                .FromSqlRaw("select Sid, epc, readerId, transTime from scannel.dbo.eaton_epc table1 where transTime=(select Max(table2.transTime) from scannel.dbo.eaton_epc table2 where table1.epc=table2.epc) and readerId={0}"
                , ReaderIdEnum.WareHouseC.ToString())
                .ToList();
            List<EatonEpc> warehouseDEatonEpcs = _dbContext.EatonEpcs
                .FromSqlRaw("select Sid, epc, readerId, transTime from scannel.dbo.eaton_epc table1 where transTime=(select Max(table2.transTime) from scannel.dbo.eaton_epc table2 where table1.epc=table2.epc) and readerId={0}"
                , ReaderIdEnum.WareHouseD.ToString())
                .ToList();
            List<EatonEpc> warehouseEEatonEpcs = _dbContext.EatonEpcs
                .FromSqlRaw("select Sid, epc, readerId, transTime from scannel.dbo.eaton_epc table1 where transTime=(select Max(table2.transTime) from scannel.dbo.eaton_epc table2 where table1.epc=table2.epc) and readerId={0}"
                , ReaderIdEnum.WareHouseE.ToString())
                .ToList();
            List<EatonEpc> warehouseFEatonEpcs = _dbContext.EatonEpcs
                .FromSqlRaw("select Sid, epc, readerId, transTime from scannel.dbo.eaton_epc table1 where transTime=(select Max(table2.transTime) from scannel.dbo.eaton_epc table2 where table1.epc=table2.epc) and readerId={0}"
                , ReaderIdEnum.WareHouseF.ToString())
                .ToList();
            List<EatonEpc> warehouseGEatonEpcs = _dbContext.EatonEpcs
                .FromSqlRaw("select Sid, epc, readerId, transTime from scannel.dbo.eaton_epc table1 where transTime=(select Max(table2.transTime) from scannel.dbo.eaton_epc table2 where table1.epc=table2.epc) and readerId={0}"
                , ReaderIdEnum.WareHouseG.ToString())
                .ToList();
            List<EatonEpc> warehouseHEatonEpcs = _dbContext.EatonEpcs
                .FromSqlRaw("select Sid, epc, readerId, transTime from scannel.dbo.eaton_epc table1 where transTime=(select Max(table2.transTime) from scannel.dbo.eaton_epc table2 where table1.epc=table2.epc) and readerId={0}"
                , ReaderIdEnum.WareHouseH.ToString())
                .ToList();
            List<EatonEpc> warehouseIEatonEpcs = _dbContext.EatonEpcs
                .FromSqlRaw("select Sid, epc, readerId, transTime from scannel.dbo.eaton_epc table1 where transTime=(select Max(table2.transTime) from scannel.dbo.eaton_epc table2 where table1.epc=table2.epc) and readerId={0}"
                , ReaderIdEnum.WareHouseI.ToString())
                .ToList();
            List<EatonEpc> elevatorEatonEpcs = _dbContext.EatonEpcs
                .FromSqlRaw("select Sid, epc, readerId, transTime from scannel.dbo.eaton_epc table1 where transTime=(select Max(table2.transTime) from scannel.dbo.eaton_epc table2 where table1.epc=table2.epc) and readerId={0}"
                , ReaderIdEnum.Elevator.ToString())
                .ToList();
            List<EatonEpc> secondFloorEatonEpcs = _dbContext.EatonEpcs
                .FromSqlRaw("select Sid, epc, readerId, transTime from scannel.dbo.eaton_epc table1 where transTime=(select Max(table2.transTime) from scannel.dbo.eaton_epc table2 where table1.epc=table2.epc) and readerId={0}"
                , ReaderIdEnum.SecondFloorA.ToString())
                .ToList();
            List<EatonEpc> thirdFloorAEatonEpcs = _dbContext.EatonEpcs
                .FromSqlRaw("select Sid, epc, readerId, transTime from scannel.dbo.eaton_epc table1 where transTime=(select Max(table2.transTime) from scannel.dbo.eaton_epc table2 where table1.epc=table2.epc) and readerId={0}"
                , ReaderIdEnum.ThirdFloorA.ToString())
                .ToList();
            List<EatonEpc> thirdFloorBEatonEpcs = _dbContext.EatonEpcs
                .FromSqlRaw("select Sid, epc, readerId, transTime from scannel.dbo.eaton_epc table1 where transTime=(select Max(table2.transTime) from scannel.dbo.eaton_epc table2 where table1.epc=table2.epc) and readerId={0}"
                , ReaderIdEnum.ThirdFloorB.ToString())
                .ToList();
            List<EatonEpc> terminalEatonEpcs = _dbContext.EatonEpcs
                .FromSqlRaw("select Sid, epc, readerId, transTime from scannel.dbo.eaton_epc table1 where transTime=(select Max(table2.transTime) from scannel.dbo.eaton_epc table2 where table1.epc=table2.epc) and readerId={0}"
                , ReaderIdEnum.Terminal.ToString())
                .ToList();
            List<EatonEpc> dbEatonEpcs = _dbContext.EatonEpcs
                .FromSqlRaw("select * from scannel.dbo.eaton_epc")
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
            List<EpcDto> elevatorEpcDtos   = GetEpcDtos(dbEatonEpcs, elevatorEatonEpcs, wo, pn, palletId);
            List<EpcDto> secondFloorEpcDtos = GetEpcDtos(dbEatonEpcs, secondFloorEatonEpcs, wo, pn, palletId);
            List<EpcDto> thirdFloorAEpcDtos = GetEpcDtos(dbEatonEpcs, thirdFloorAEatonEpcs, wo, pn, palletId);
            List<EpcDto> thirdFloorBEpcDtos = GetEpcDtos(dbEatonEpcs, thirdFloorBEatonEpcs, wo, pn, palletId);
            List<EpcDto> terminalEpcDtos = GetEpcDtos(dbEatonEpcs, terminalEatonEpcs, wo, pn, palletId);
            List<EpcDto> allEpcDtos = GetEpcDtos(dbEatonEpcs, dbEatonEpcs, null, null, null);

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
            if (string.IsNullOrEmpty(hexString) == true)
            {
                return "";
            }
            string asciiString = "";
            for(int i = 0; i < hexString.Length; i += 2)
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
            foreach(var eatonEpcDto in eatonEpcDtos)
            {
                string epcString = GetHexToAscii(eatonEpcDto.Epc);
                bool isCorrectStringFormat = true;
                // Error string format
                if (epcString.Contains("##") == false || epcString.Split("##").Count() == 0 || epcString.Split("##")[1].Split("&&").Count() != 5)
                {
                    isCorrectStringFormat = false;
                }
                // Correct string format
                string wo = isCorrectStringFormat == true ? epcString.Split("##")[1].Split("&&")[0] : "Error: " + eatonEpcDto.Epc;
                string qty = isCorrectStringFormat == true ? epcString.Split("##")[1].Split("&&")[1] : "Error: " + eatonEpcDto.Epc;
                string pn = isCorrectStringFormat == true ? epcString.Split("##")[1].Split("&&")[2] : "Error " + eatonEpcDto.Epc;
                string line = isCorrectStringFormat == true ? epcString.Split("##")[1].Split("&&")[3] : "Error: " + eatonEpcDto.Epc;
                string barcode = isCorrectStringFormat == true ? epcString.Split("##")[1].Split("&&")[4] : "Error: " + eatonEpcDto.Epc;
                string error = isCorrectStringFormat == true ? "" : eatonEpcDto.Epc;
                List <EatonEpc> dbSameEpcs = dbEatonEpcs
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
                    epcDtos.Add(new EpcDto
                    {
                        Epc = eatonEpcDto.Epc,
                        ReaderId = eatonEpcDto.ReaderId,
                        TransTime = GetDateTimeString(eatonEpcDto.TransTime.Value),
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
                bool gotProduction = false;
                bool gotElevator = false;
                bool gotWarehouse = false;
                foreach(var locationTimeDto in locationTimeDtos)
                {
                    if (locationTimeDto == locationTimeDtos.First())
                    {
                        // First
                        if (locationTimeDto.Location != ReaderIdEnum.ThirdFloorA.ToChineseString() &&
                            locationTimeDto.Location != ReaderIdEnum.ThirdFloorB.ToChineseString() &&
                            locationTimeDto.Location != ReaderIdEnum.SecondFloorA.ToChineseString())
                        {
                            return EpcStateEnum.NG.ToString();
                        }
                        else
                        {
                            gotProduction = true;
                        }
                    }
                    else
                    {
                        // Others
                        // Production -> Elevator -> Warehouse, correct
                        // Correct sequence
                        if (locationTimeDto.Location == ReaderIdEnum.Elevator.ToChineseString() && gotElevator == false && gotWarehouse == false)
                        {
                            gotElevator = true;
                        }
                        else if (locationTimeDto.Location.Contains("1F 成品區") == true && gotElevator == true && gotWarehouse == false)
                        {
                            gotWarehouse = true;
                        }
                        // Incorrect sequence
                        if (locationTimeDto.Location == ReaderIdEnum.Elevator.ToChineseString() && gotElevator == true && gotWarehouse == true)
                        {
                            return EpcStateEnum.Return.ToString();
                        }
                        else if (locationTimeDto.Location == ReaderIdEnum.Elevator.ToChineseString() && gotProduction == false)
                        {
                            return EpcStateEnum.NG.ToString();
                        }
                    }
                }
                return EpcStateEnum.OK.ToString();
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
            return selectionDto;
        }

        private EpcGetResultDto GetEpcGetResultDto(ResultEnum result, ErrorEnum error, DashboardDto dashboardDto, SelectionDto selectionDto)
        {
            return new EpcGetResultDto
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
                default:
                    return "";

            }
        }
    }
}
