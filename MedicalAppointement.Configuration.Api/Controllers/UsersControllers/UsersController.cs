using Medical.Domain.Entities.Confi.Users;
using MedicalAppointment.Persistance.Interfaces;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using MedicalCoreAplications.cs.Contracts.Configurations;
using MedicalCoreAplications.cs.Dtos.Configurations.UserDtos.cs;
using MedicalCoreAplications.cs.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointement.Users.Api.Controllers.UsersControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserServices _userservices; 


        public UsersController(IUserServices userservices)
        {
            this._userservices = userservices;
        }   



        [HttpGet("GetUsers")]
        public  async Task<IActionResult> Get()
        {
            var result = await _userservices.getall();
            if (!result.success) return BadRequest(result); 


            return Ok(result);
        }

        // GET api/<UsersController>/5
        [HttpGet("GetUserById")]
        public async Task<IActionResult> Get(int id)
        { 

            var result = await _userservices.GetById(id);
            if (!result.success) return BadRequest(result); 

            return Ok(result); 

        }

        // POST api/<UsersController>
        [HttpPost("SaveUsers")]
        public async Task<IActionResult> Post([FromBody] SaveUserDtos user)
        { 

            var result = await _userservices.SaveAsync(user);
            if (!result.success) return BadRequest(result);

            return Ok(result);

        }

        // PUT api/<UsersController>/5
        [HttpPut("UpdateUsers")]
        public async Task<IActionResult> Put([FromBody]  UpdateUserDtos  value)
        {
            var result = await _userservices.UpdateAsync(value);
            if (!result.success) return BadRequest(result);

            return Ok(value);

        }

     
    }
}
