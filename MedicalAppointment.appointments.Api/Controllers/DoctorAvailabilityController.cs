using MedicalAppointment.Application.Contracts.appointmentsContracts;
using MedicalAppointment.Application.Dto.Dtosappointments.DoctorAvailabilityDtos;
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
        private readonly IDoctorAvailabilityService _doctorAvailabilityService;

        public DoctorAvailabilityController(IDoctorAvailabilityService doctorAvailabilityService)
        {
            _doctorAvailabilityService = doctorAvailabilityService;
        }



        [HttpGet("GetDoctorAvailability")]
        public async Task<IActionResult> Get()
        {
            var result = await _doctorAvailabilityService.GetAll();
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("GetDoctorAvailabilityById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _doctorAvailabilityService.GetById(id);
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("DoctorAvailabilitySave")]
        public async Task<IActionResult> Post([FromBody] DoctorAvailabilitySaveDto entity)
        {

            var result = await _doctorAvailabilityService.SaveAsync(entity);
            if (!result.IsSuccess) return BadRequest(result);


            return Ok(result);
        }


        [HttpPut("DoctorAvailabilityUpdate")]
        public async Task<IActionResult> put(int id, [FromBody] DoctorAvailabilityUpdateDto entity)
        {
            var result = await _doctorAvailabilityService.UpdateAsync(entity);
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }





    }
}
