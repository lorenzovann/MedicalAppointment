using Medical.Domain.Entities.Confi.Systems;
using MedicalAppointment.Persistance.Interfaces.Configuration.SystemIntefaces;

using MedicalCoreAplications.cs.Contracts.Systems;
using MedicalCoreAplications.cs.Dtos.Systems.StatusDtos.cs;
using Microsoft.AspNetCore.Mvc;



namespace MedicalAppoiment.systems.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        

        private readonly IStatusServices _statusservices; 

        public StatusController(IStatusServices Statusservices)
        {
            _statusservices = Statusservices; 
        }

            [HttpGet("GetStatus")]
        public  async Task<IActionResult> Get()
        {
          var result = await _statusservices.getall();
          if (!result.success) return BadRequest(result); 

          return Ok(result);
        }

        // GET api/<StatusControllers>/5
        [HttpGet("GetStatusByID")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _statusservices.GetById(id);
            if (!result.success) return BadRequest(result); 

            return Ok(result);
        }

        // POST api/<StatusControllers>
        [HttpPost("SaveStatus")]
        public async Task<IActionResult> Post([FromBody]  SaveStatusDtos value)
        {
            var result = await _statusservices.SaveAsync(value);
            if (!result.success) return BadRequest(result);

            return Ok(result);
        }

        // PUT api/<StatusControllers>/5
        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> Put([FromBody] UpdateStatusDtos value)
        {
            var result = await _statusservices.UpdateAsync(value);  
            if (!result.success) return BadRequest(result);

            return Ok(result);
        }

 
    }
}
