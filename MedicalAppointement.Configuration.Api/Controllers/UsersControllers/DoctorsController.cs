using Medical.Domain.Entities.Confi.Users;
using MedicalCoreAplications.cs.Contracts.Configurations;
using MedicalCoreAplications.cs.Dtos.Configurations.DoctorDtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppointement.Users.Api.Controllers.UsersControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase 
    {

        private readonly IDoctorServices _doctorservies; 


        public DoctorsController(IDoctorServices doctorservies)
        {
            _doctorservies = doctorservies;
        }

       
        // GET: api/<DoctorsController>
        [HttpGet("GetDoctor")]
        public async Task<IActionResult> Get()
        {
            var result = await _doctorservies.getall();
            if (!result.success)  return BadRequest(result); 

            return Ok(result);
        }
            
        // GET api/<DoctorsController>/5
        [HttpGet("GetDoctorById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _doctorservies.GetById(id);
            if (!result.success) return BadRequest(result); 

            return Ok(result);
        }

        // POST api/<DoctorsController>
        [HttpPost("SaveUsers")]
        public async Task<IActionResult> Post([FromBody] SaveDoctorDtos doctor)
        { 
            var result = await _doctorservies.SaveAsync(doctor);
            if (!result.success) return BadRequest(result); 

            return Ok(result);  

        }

        // PUT api/<DoctorsController>/5
        [HttpPut("UpdateUsers")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateDoctorDtos doctor)
        {

            var result = await _doctorservies.UpdateAsync(doctor);
            if (!result.success) return BadRequest(result);

            return Ok(result);  
        }

      



    }
}
