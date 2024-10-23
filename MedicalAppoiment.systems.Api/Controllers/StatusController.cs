using Medical.Domain.Entities.Confi.Systems;
using MedicalAppointment.Persistance.Interfaces.Configuration.SystemIntefaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppoiment.systems.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        // GET: api/<StatusControllers>

        private readonly IStatusInterfaces _statusrepositorie;

        public StatusController(IStatusInterfaces statusrepositorie)
        {
            _statusrepositorie = statusrepositorie;
        }

            [HttpGet("GetStatus")]
        public  async Task<IActionResult> Get()
        {
          var result = await _statusrepositorie.Getall();
          if (!result.Sucess) return BadRequest(result); 

          return Ok(result);
        }

        // GET api/<StatusControllers>/5
        [HttpGet("GetStatusByID")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _statusrepositorie.GetEntitiebyId(id);
            if (!result.Sucess) return BadRequest(result); 

            return Ok(result);
        }

        // POST api/<StatusControllers>
        [HttpPost("SaveStatus")]
        public async Task<IActionResult> Post([FromBody]  Status value)
        {
            var result = await _statusrepositorie.Add(value);
            if(!result.Sucess) return BadRequest(result);

            return Ok(result);
        }

        // PUT api/<StatusControllers>/5
        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> Put([FromBody] Status value)
        {
            var result = await _statusrepositorie.Update(value);
            if (!result.Sucess) return BadRequest(result);

            return Ok(result);
        }

 
    }
}
