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
        private readonly IInsuranceProvidersRepository _insuranceProvidersRepository;

        public InsuranceProvidersController(IInsuranceProvidersRepository insuranceProvidersRepository)
        {
            _insuranceProvidersRepository = insuranceProvidersRepository;
        }

        [HttpGet("GetInsuranceProviders")]
        public async Task<IActionResult> Get()
        {
            var result = await _insuranceProvidersRepository.GetAll();
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("GetInsuranceProvidersById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _insuranceProvidersRepository.GetEntityBy(id);
            if (!result.Success) return BadRequest(result);

            return Ok(result);

        }

        [HttpPost("SaveInsuranceProviders")]
        public async Task<IActionResult> Post([FromBody] InsuranceProviders entity)
        {
            var result = await _insuranceProvidersRepository.Save(entity);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("UpdateInsuranceProviders")]
        public async Task<IActionResult> Put(int id, [FromBody] InsuranceProviders entity)
        {
            var result = await _insuranceProvidersRepository.Update(entity);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }


    } 
}
