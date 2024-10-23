using Medical.Domain.Entities.Confi.Systems;
using MedicalAppointment.Persistance.Interfaces.Configuration.SystemIntefaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppoiment.systems.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Notifcations : ControllerBase
    {

        private readonly INotificationsinterfaces _notificationrepositorie;

        public Notifcations(INotificationsinterfaces notificationrepositorie)
        {
           _notificationrepositorie = notificationrepositorie;

        }

        [HttpGet("GetNotifications")] // mis endpoint
         public async Task<IActionResult> Get()
        {
            var result = await _notificationrepositorie.Getall();
            if(!result.Sucess) return BadRequest(result);  

            return Ok(result);
        }

        // GET api/<NotificationsControlles>/5
        [HttpGet("GetNotificationsById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _notificationrepositorie.GetEntitiebyId(id);
            if (!result.Sucess) return BadRequest(result);

            return Ok(result);  

        }

        // POST api/<NotificationsControlles>
        [HttpPost("SaveNotifications")]
        public async Task<IActionResult> Post([FromBody] Notifications value)
        {
                var result = await _notificationrepositorie.Add(value);
                if (!result.Sucess) return BadRequest(result);

                return Ok(result);
      }

        [HttpPut("UpdateNotifiacations")]
        public async Task<IActionResult> Put([FromBody] Notifications value)
        {
            var result = await _notificationrepositorie.Update(value);
            if (!result.Sucess) return BadRequest(result);

            return Ok(result);

        }

    }
}
