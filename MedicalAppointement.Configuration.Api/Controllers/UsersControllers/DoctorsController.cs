using Medical.Domain.Entities.Confi.Users;
using MedicalAppointment.Persistance.Interfaces.Configuration.UsersInterfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppointement.Users.Api.Controllers.UsersControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase 
    {  

        private readonly IDoctorInterfaces _octorInterfaces;

        public DoctorsController(IDoctorInterfaces octorInterfaces)
        {
            _octorInterfaces = octorInterfaces;

        }
        // GET: api/<DoctorsController>
        [HttpGet("GetDoctor")]
        public async Task<IActionResult> Get()
        {
            var result = await _octorInterfaces.Getall();
            if (!result.Sucess)  return BadRequest(result); 

            return Ok(result);
        }
            
        // GET api/<DoctorsController>/5
        [HttpGet("GetDoctorById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _octorInterfaces.GetEntitiebyId(id);
            if (!result.Sucess) return BadRequest(result); 

            return Ok(result);
        }

        // POST api/<DoctorsController>
        [HttpPost("SaveUsers")]
        public async Task<IActionResult> Post([FromBody] Doctor doctor)
        { 
            var result = await _octorInterfaces.Add(doctor);
            if (!result.Sucess) return BadRequest(result); 

            return Ok(result);  

        }

        // PUT api/<DoctorsController>/5
        [HttpPut("UpdateUsers")]
        public async Task<IActionResult> Put(int id, [FromBody] Doctor doctor)
        {

            var result = await _octorInterfaces.Update(doctor);
            if (!result.Sucess) return BadRequest(result);

            return Ok(result);  
        }

        [HttpPost("DisableDoctor")]
        public async Task<IActionResult> DisableDoctor(Doctor doctor)
        {
            var result = await  _octorInterfaces.Delete(doctor);
            if (!result.Sucess) return BadRequest(result);

            return Ok(result);

        }



    }
}
