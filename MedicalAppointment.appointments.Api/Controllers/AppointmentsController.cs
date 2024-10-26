using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Persistance.Interfaces.appointments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace MedicalAppointment.Insurance.Api.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsRepository _appointmentsRepository;

        public AppointmentsController(IAppointmentsRepository appointmentsRepository)
        {
            appointmentsRepository = _appointmentsRepository;

        }
        // GET: api/<DoctorsController>
        [HttpGet("GetAppointments")]
        public async Task<IActionResult> Get()
        {
            var result = await _appointmentsRepository.GetAll();
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        // GET api/<DoctorsController>/5
        [HttpGet("GetAppointmentsById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _appointmentsRepository.GetEntityBy(id);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        // POST api/<DoctorsController>
        [HttpPost("SaveAppointments")]
        public async Task<IActionResult> Save([FromBody] Appointments appointments)
        {
            var result = await _appointmentsRepository.Save(appointments);
            if (!result.Success) return BadRequest(result);

            return Ok(result);

        }

        // PUT api/<DoctorsController>/5
        [HttpPut("UpdateAppointments")]
        public async Task<IActionResult> Update(int id, [FromBody] Appointments appointments)
        {

            var result = await _appointmentsRepository.Update(appointments);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }


    }
}