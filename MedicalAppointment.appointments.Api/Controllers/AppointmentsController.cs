using MedicalAppointment.Application.Contracts.appointmentsContracts;
using MedicalAppointment.Application.Dto.Dtosappointments.Appointments;
using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Persistance.Interfaces.appointments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace MedicalAppointment.Insurance.Api.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsService _appointmentsService;

        public AppointmentsController(IAppointmentsService appointmentsService)
        {
            _appointmentsService = appointmentsService;

        }
        // GET: api/<DoctorsController>
        [HttpGet("GetAppointments")]
        public async Task<IActionResult> Get()
        {
            var result = await _appointmentsService.GetAll();
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }

        // GET api/<DoctorsController>/5
        [HttpGet("GetAppointmentsById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _appointmentsService.GetById(id);
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }

        // POST api/<DoctorsController>
        [HttpPost("SaveAppointments")]
        public async Task<IActionResult> Post([FromBody] AppointmentsSaveDto dto)
        {
            var result = await _appointmentsService.SaveAsync(dto);
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);

        }

        // PUT api/<DoctorsController>/5
        [HttpPut("UpdateAppointments")]
        public async Task<IActionResult> Put(int id, [FromBody] AppointmentsUpdateDto dto)
        {

            var result = await _appointmentsService.UpdateAsync(dto);
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }


    }
}