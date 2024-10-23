using Medical.Domain.Entities.Confi.Users;
using MedicalAppointment.Persistance.Interfaces;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointement.Users.Api.Controllers.UsersControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserInterfaces _userepositorie;


        public UsersController(UserInterfaces userepositorie) { 
        
            _userepositorie = userepositorie;

        }    


        [HttpGet("GetUsers")]
        public  async Task<IActionResult> Get()
        { 
            var result = await _userepositorie.Getall();
            if (!result.Sucess) return BadRequest(result); 


            return Ok(result);
        }

        // GET api/<UsersController>/5
        [HttpGet("GetUserById")]
        public async Task<IActionResult> Get(int id)
        { 

            var result = await _userepositorie.GetEntitiebyId(id);
            if (!result.Sucess) return BadRequest(result); 

            return Ok(result); 

        }

        // POST api/<UsersController>
        [HttpPost("SaveUsers")]
        public async Task<IActionResult> Post([FromBody] User user)
        { 

            var result = await _userepositorie.Add(user);
            if (!result.Sucess) return BadRequest(result);

            return Ok(result);

        }

        // PUT api/<UsersController>/5
        [HttpPut("UpdateUsers")]
        public async Task<IActionResult> Put(int id, [FromBody] User value)
        {
            var result = await _userepositorie.Update(value);
            if (!result.Sucess) return BadRequest(result);

            return Ok(value);

        }

    
    }
}
