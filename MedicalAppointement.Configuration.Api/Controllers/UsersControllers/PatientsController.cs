using Medical.Domain.Entities.Confi.Users;
using MedicalAppointment.Persistance.Interfaces.Configurations;
using MedicalCoreAplications.cs.Contracts.Configurations;
using MedicalCoreAplications.cs.Dtos.Configurations.PatientDtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppointement.Users.Api.Controllers.UsersControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {

        private readonly IPatientServices _patientservicers;


        public PatientsController(IPatientServices patientrepositories)
        {
            _patientservicers = patientrepositories;
        }
        // GET: api/<PatientsController>
        [HttpGet("GetPatients")]
        public  async Task<IActionResult> Get()
        {
           var result = await _patientservicers.getall();
           if (!result.success) return BadRequest(result); 

           return Ok(result);  
        }

        // GET api/<PatientsController>/5
        [HttpGet("GetPatientById")]
        public async Task<IActionResult> Get(int id)
        { 

            var result = await _patientservicers.GetById(id);
            if (!result.success) return BadRequest(result);

            return Ok(result);
        }

        // POST api/<PatientsController>
        [HttpPost("SavePatients")]
        public async Task<IActionResult> Post([FromBody] SavePatientsDtos patient)
        {
            var result = await _patientservicers.SaveAsync(patient); 
        if (!result.success) return BadRequest(result);

        return Ok(result);  
      
        }

        // PUT api/<PatientsController>/5
        [HttpPut("UpdatePatient")]
        public async Task<IActionResult> Put([FromBody] UpdatePatientsDtos patient)
        {
            var result = await _patientservicers.UpdateAsync(patient);
            if (!result.success) return BadRequest(result);

            return Ok(result);
        }


    }
}
