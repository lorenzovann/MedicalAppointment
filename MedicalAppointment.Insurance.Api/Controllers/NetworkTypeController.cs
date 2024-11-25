using MedicalAppointment.Application.Contracts.InsuranceContracts;
using MedicalAppointment.Application.Dto.DtosInsurance.NetworkTypeDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Insurance.Api.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class NetworkTypeController : Controller
    {
        private readonly INetworkTypeService _networkTypeService;

        public NetworkTypeController(INetworkTypeService networkTypeService)
        {
            _networkTypeService = networkTypeService;
        }

        [HttpGet("NetworkTypeGetAll")]
        public async Task<IActionResult> Get()
        {
            var result = await _networkTypeService.GetAll();
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("GetNetworkTypeById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _networkTypeService.GetById(id);
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);


        }

        [HttpPost("NetworkTypeSave")]
        public async Task<IActionResult> Post([FromBody] NetworkTypeSaveDto dto)
        {
            var result = await _networkTypeService.SaveAsync(dto);
            if (!result.IsSuccess) return BadRequest();

            return Ok();

        }

        [HttpPut("NetworkTypeUpdate")]
        public async Task<IActionResult> put([FromBody] NetworkTypeUpdateDto dto)
        {
            var result = await _networkTypeService.UpdateAsync(dto);
            if (!result.IsSuccess) return BadRequest();

            return Ok();
        }


    }
}
