using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Persistance.Interfaces.appointments;
using MedicalAppointment.Persistance.Repositories.appointmentsRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.appointments.Api.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class DoctorAvailabilityController : Controller
    {
        private readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;

        public DoctorAvailabilityController(IDoctorAvailabilityRepository doctorAvailabilityRepository)
        {
            _doctorAvailabilityRepository = doctorAvailabilityRepository;
        }



        [HttpGet("GetAppointments")]
        public async Task<IActionResult> Get()
        {
            var result = await _doctorAvailabilityRepository.GetAll();
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("GetAppointmentsById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _doctorAvailabilityRepository.GetEntityBy(id);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("DoctorAvailabilitySave")]
        public async Task<IActionResult> Post([FromBody] DoctorAvailability entity)
        {

            var result = await _doctorAvailabilityRepository.Save(entity);
            if (!result.Success) return BadRequest(result);


            return Ok(result);
        }


        [HttpPut("DoctorAvailabilityUpdate")]
        public async Task<IActionResult> put(int id, [FromBody] DoctorAvailability entity)
        {
            var result = await _doctorAvailabilityRepository.Update(entity);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }





    }
}
