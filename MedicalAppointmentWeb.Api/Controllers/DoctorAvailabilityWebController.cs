using MedicalAppointment.Application.Contracts.appointmentsContracts;
using MedicalAppointment.Application.Dto.Dtosappointments.Appointments;
using MedicalAppointment.Application.Dto.Dtosappointments.AppointmentsDtos;
using MedicalAppointment.Application.Dto.Dtosappointments.DoctorAvailabilityDtos;
using MedicalAppointment.Application.Services.appointmentsService;
using MedicalAppointment.Domain.Entities.appointments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentWeb.Api.Controllers
{
    public class DoctorAvailabilityWebController : Controller
    {
        private readonly IDoctorAvailabilityService _doctorAvailabilityService;

        public DoctorAvailabilityWebController(IDoctorAvailabilityService doctorAvailabilityService)
        {
            _doctorAvailabilityService = doctorAvailabilityService;
        }

        [Route("DoctorAvailability")]
        public async Task<IActionResult> Index()
        {
            var result = await _doctorAvailabilityService.GetAll();

            if (result.IsSuccess && result.Data is List<DoctorAvailabilityGetDto> dto)
            {
                return View(dto);
            }

            ViewBag.Message = result.Message;
            return View(new List<DoctorAvailabilityGetDto>());
        }


        public async Task<IActionResult> Details(int id)
        {
            var result = await _doctorAvailabilityService.GetById(id);

            if (result.IsSuccess && result.Data is DoctorAvailability doctorAvailability)
            {
                return View(doctorAvailability);
            }

            ViewBag.Message = result.Message;
            return View(new DoctorAvailability());
        }


        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorAvailabilitySaveDto doctorAvailabilitySaveDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Datos inválidos.";
                return View();
            }

            var result = await _doctorAvailabilityService.SaveAsync(doctorAvailabilitySaveDto);

            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = result.Message;
            return View();

        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _doctorAvailabilityService.GetById(id);

            if (result.IsSuccess && result.Data is DoctorAvailability doctorAvailability)
            {
                var dto = new DoctorAvailabilityUpdateDto
                {
                    AvailabilityID = doctorAvailability.AvailabilityID,
                    DoctorID = doctorAvailability.DoctorID,
                    AvailableDate = doctorAvailability.AvailableDate,
                    StartTime = doctorAvailability.StartTime,
                    EndTime = doctorAvailability.EndTime,
                   
                };

                return View(dto);
            }

            ViewBag.Message = result.Message;
            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DoctorAvailabilityUpdateDto doctorAvailabilityUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Datos inválidos.";
                return View(doctorAvailabilityUpdateDto);
            }

            var result = await _doctorAvailabilityService.UpdateAsync(doctorAvailabilityUpdateDto);

            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = result.Message;
            return View(doctorAvailabilityUpdateDto);
        }

       
        
    }
}
