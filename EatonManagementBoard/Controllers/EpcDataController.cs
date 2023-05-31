using EatonManagementBoard.Dtos;
using EatonManagementBoard.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatonManagementBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpcDataController : ControllerBase
    {
        public EpcDataController(EpcDataService service)
        {
            _service = service;
        }

        private readonly EpcDataService _service;

        // GET api/<EpcDataController>?wo=..&pn=..&startDate=..&endDate=..&pastDays=..
        [HttpGet]
        public IActionResult Get([FromQuery] string wo, string pn, string palletId, string startDate, string endDate, string pastDays)
        {
            EpcDataResultDto epcDataResultDto = _service.Get(wo, pn, palletId, startDate, endDate, pastDays);
            return epcDataResultDto.Result == true ? Ok(epcDataResultDto) : BadRequest(epcDataResultDto);
        }
    }
}
