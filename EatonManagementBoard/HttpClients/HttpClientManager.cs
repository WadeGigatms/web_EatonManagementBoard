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
    public class HttpClientManager
    {
        public HttpClientManager(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private readonly IHttpClientFactory _httpClientFactory;

        public bool PostToServerWithDeliveryTerminal(EpcRawContext epcRawContext, EpcDataContext epcDataContext)
        {
            var httpClient = _httpClientFactory.CreateClient();
            //httpClient.BaseAddress = new Uri("https://localhost:44361/");
            httpClient.BaseAddress = new Uri("http://localhost:84/");
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
            var response = httpClient.PostAsync("api/delivery/terminal", content).Result;
            return response.IsSuccessStatusCode;
        }
    }
}
