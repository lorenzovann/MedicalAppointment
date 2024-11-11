using Medical.Domain.Entities.Confi.Systems;
using MedicalAppointment.Persistance.Interfaces.Configuration.SystemIntefaces;
using MedicalCoreAplications.cs.Contracts.systems;
using MedicalCoreAplications.cs.Dtos.Systems.RolesDtos.cs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppoiment.systems.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase 
    {
        private readonly IRoleServices _roleservices; 

        public RoleController(IRoleServices roleservices)
        {
            _roleservices = roleservices; 


        }

        
        [HttpGet("GetRoles")]
        public async Task<IActionResult>  Get()
        { 
            var result = await _roleservices.getall();
            if(!result.success) return BadRequest(result);
            return Ok(result);
        }


        [HttpGet("GetEntitieByID")]
        public async Task<IActionResult> Get(int id)
        {
           var result = await _roleservices.GetById(id);
            if (!result.success) return BadRequest(result);


         return Ok(result); 


        }

     
        [HttpPost("SaveRoles")]
        public async Task<ActionResult> Post([FromBody] SaveRolesDtos value)
        {

            var result = await _roleservices.SaveAsync(value);
            if (!result.success) return BadRequest(result);

            return Ok(result);  
        }

        [HttpPut("UpdateRoles")]
        public async Task<IActionResult> Put([FromBody] RolesUpdateDtos value)
        {
            var result = await _roleservices.UpdateAsync(value);
            if (!result.success) return BadRequest(result); 

            return Ok(result);
       
        }
   
      }
  }
