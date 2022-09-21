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
            EpcGetResultDto epcGetResultDto = _service.Get(wo, pn, palletId);
            return epcGetResultDto.Result == true ? Ok(epcGetResultDto) : BadRequest(epcGetResultDto);
        }
    }
}
