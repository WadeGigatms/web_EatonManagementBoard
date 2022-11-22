using EatonManagementBoard.Dtos;
using EatonManagementBoard.Models;
using EatonManagementBoard.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EatonManagementBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpcController : ControllerBase
    {
        public EpcController(EpcService service)
        {
            _service = service;
        }

        private readonly EpcService _service;

        // GET api/<EpcController>?taskNo=..&pn=..&pallet=..
        [HttpGet]
        public IActionResult Get([FromQuery] string wo, string pn, string palletId)
        {
            EpcResultDto getEpcResultDto = _service.Get(wo, pn, palletId);
            return getEpcResultDto.Result == true ? Ok(getEpcResultDto) : BadRequest(getEpcResultDto);
        }

        // GET api/<EpcController>/rtc
        [HttpGet("rtc")]
        public IActionResult GetRtc()
        {
            RtcResultDto getRtcResultDto = _service.GetRtc();
            return getRtcResultDto.Result == true ? Ok(getRtcResultDto) : BadRequest(getRtcResultDto);
        }

        // POST api/<EpcController>
        [HttpPost]
        public IActionResult Post([FromQuery] string epc)
        {
            ResultDto epcResultDto = _service.Post(epc);
            return epcResultDto.Result == true ? Ok(epcResultDto) : BadRequest(epcResultDto);
        }

        // DELETE api/<EpcController>
        [HttpDelete]
        public IActionResult Delete([FromQuery] string epc)
        {
            ResultDto epcResultDto = _service.Delete(epc);
            return epcResultDto.Result == true ? Ok(epcResultDto) : BadRequest(epcResultDto);
        }
    }
}
