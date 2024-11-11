using Medical.Domain.Entities.Confi.Systems;
using MedicalAppointment.Persistance.Interfaces.Configuration.SystemIntefaces;
using MedicalCoreAplications.cs.Contracts.systems;
using MedicalCoreAplications.cs.Dtos.Systems.NotificationsDtos.cs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppoiment.systems.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Notifcations : ControllerBase
    {

        private readonly INotificationsServices _notificationsServices;
                          // inyeccion de mi servicio 
        public Notifcations(INotificationsServices notificationsServices)
        {
           
            _notificationsServices = notificationsServices;
        }

        [HttpGet("GetNotifications")] // mis endpoint
         public async Task<IActionResult> Get()
        {
            var result = await _notificationsServices.getall();
            if(!result.success) return BadRequest(result);  

            return Ok(result);
        }

        // GET api/<NotificationsControlles>/5
        [HttpGet("GetNotificationsById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _notificationsServices.GetById(id);
            if (!result.success) return BadRequest(result);

            return Ok(result);  

        }

        // POST api/<NotificationsControlles>
        [HttpPost("SaveNotifications")]
        public async Task<IActionResult> Post([FromBody] SaveNotifications value)
        {
                var result = await _notificationsServices.SaveAsync(value);  
                if (!result.success) return BadRequest(result);

                return Ok(result);
      }

        [HttpPut("UpdateNotifiacations")]
        public async Task<IActionResult> Put([FromBody] UpdateNotifications value)
        {
            var result = await _notificationsServices.UpdateAsync(value);
            if (!result.success) return BadRequest(result);

            return Ok(result);

        }

    }
}
