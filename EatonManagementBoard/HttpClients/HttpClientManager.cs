﻿using EatonManagementBoard.Database;
using EatonManagementBoard.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EatonManagementBoard.HttpClients
{
    public static class HttpClientManager
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static bool PostToServerWithDeliveryTerminal(EpcRawContext epcRawContext, EpcDataContext epcDataContext)
        {
            _httpClient.BaseAddress = new Uri("http://localhost/");
            DeliveryTerminalPostDto dto = new DeliveryTerminalPostDto
            {
                epc_raw_id = epcRawContext.id,
                epc_data_id = epcDataContext.id,
                wo = epcDataContext.wo,
                qty = epcDataContext.qty,
                pn = epcDataContext.pn,
                line = epcDataContext.line,
                pallet_id = epcDataContext.pallet_id,
            };
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("api/delivery/terminal", content).Result;
            return response.IsSuccessStatusCode;
        }
    }
}
