using Medical.Domain.Entities.Confi.Users;
using MedicalAppointment.Persistance.Interfaces.Configuration.UsersInterfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppointement.Users.Api.Controllers.UsersControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {

        private readonly IPatientInterfaces _patientrepositories;

        public PatientsController(IPatientInterfaces patientrepositories)
        {
            _patientrepositories = patientrepositories;
        }
        // GET: api/<PatientsController>
        [HttpGet("GetPatients")]
        public  async Task<IActionResult> Get()
        {
           var result = await _patientrepositories.Getall();
           if (!result.Sucess) return BadRequest(result); 

           return Ok(result);  
        }

        // GET api/<PatientsController>/5
        [HttpGet("GetPatientById")]
        public async Task<IActionResult> Get(int id)
        { 

            var result = await _patientrepositories.GetEntitiebyId(id);
            if (!result.Sucess) return BadRequest(result);

            return Ok(result);
        }

        // POST api/<PatientsController>
        [HttpPost("SavePatients")]
        public async Task<IActionResult> Post([FromBody] Patient patient)
        { 
         var result = await _patientrepositories.Add(patient);
        if (!result.Sucess) return BadRequest(result);

        return Ok(result);  
      
        }

        // PUT api/<PatientsController>/5
        [HttpPut("UpdatePatient")]
        public async Task<IActionResult> Put([FromBody] Patient patient)
        {
            var result = await _patientrepositories.Update(patient);
            if (!result.Sucess) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("DisablePatient")]
        public async Task<IActionResult> Disable([FromBody] Patient patient)
        {
            var result = await _patientrepositories.Delete(patient);
            if (!result.Sucess) return BadRequest(result);

            return Ok(result);

        }

    }
}
