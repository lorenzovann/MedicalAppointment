using MedicalAppointment.Application.Contracts.InsuranceContracts;
using MedicalAppointment.Application.Dto.DtosInsurance.InsuranceProvidersDtos;
using MedicalAppointment.Application.Services.InsuranceService;
using MedicalAppointment.Domain.Entities.Insurance;
using MedicalAppointment.Persistance.Interfaces.Insurance;
using MedicalAppointment.Persistance.Repositories.InsuranceRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Insurance.Api.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class InsuranceProvidersController : Controller
    {
        private readonly IInsuranceProvidersService _insuranceProvidersService;

        public InsuranceProvidersController(IInsuranceProvidersService insuranceProvidersService)
        {
            _insuranceProvidersService = insuranceProvidersService;
        }

        [HttpGet("GetInsuranceProviders")]
        public async Task<IActionResult> Get()
        {
            var result = await _insuranceProvidersService.GetAll();
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("GetInsuranceProvidersById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _insuranceProvidersService.GetById(id);
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);

        }

        [HttpPost("SaveInsuranceProviders")]
        public async Task<IActionResult> Post([FromBody] InsuranceProvidersSaveDto dto)
        {
            var result = await _insuranceProvidersService.SaveAsync(dto);
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("UpdateInsuranceProviders")]
        public async Task<IActionResult> Put(int id, [FromBody] InsuranceProvidersUpdateDto dto)
        {
            var result = await _insuranceProvidersService.UpdateAsync(dto);
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }


    }
}
