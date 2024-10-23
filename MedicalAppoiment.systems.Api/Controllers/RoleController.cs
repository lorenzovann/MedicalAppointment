using Medical.Domain.Entities.Confi.Systems;
using MedicalAppointment.Persistance.Interfaces.Configuration.SystemIntefaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppoiment.systems.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase 
    {
        private readonly IRoleInterfaces _rolerepositorie;

        public RoleController(IRoleInterfaces rolerepositorie)
        {
            _rolerepositorie = rolerepositorie;
        }


            // GET: api/<RoleController>
        [HttpGet("GetRoles")]
        public async Task<IActionResult>  Get()
        { 
            var result = await _rolerepositorie.Getall();
            if(!result.Sucess) return BadRequest(result);
            return Ok(result);
        }

        // GET api/<RoleController>/5
        [HttpGet("GetEntitieByID")]
        public async Task<IActionResult> Get(int id)
        {
           var result = await _rolerepositorie.GetEntitiebyId(id);
            if (!result.Sucess) return BadRequest(result);


         return Ok(result); 


        }

        // POST api/<RoleController>
        [HttpPost("SaveRoles")]
        public async Task<ActionResult> Post([FromBody] Role value)
        {

            var result = await _rolerepositorie.Add(value);
            if (!result.Sucess) return BadRequest(result);

            return Ok(result);  
        }

        // PUT api/<RoleController>/5
        [HttpPut("UpdateRoles")]
        public async Task<IActionResult> Put([FromBody] Role value)
        { 
            var result = await _rolerepositorie.Update(value);
            if (!result.Sucess) return BadRequest(result); 

            return Ok(result);
       
        }
   
      }
  }
