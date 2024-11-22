using MedicalAppointment.Application.Contracts.appointmentsContracts;
using MedicalAppointment.Application.Dto.Dtosappointments.Appointments;
using MedicalAppointment.Application.Dto.Dtosappointments.AppointmentsDtos;
using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Persistance.Models.appointments.AppointmentsCRUD;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MedicalAppointmentWeb.Api.Controllers
{

    namespace MedicalAppointmentWeb.Api.Controllers
    {
        namespace MedicalAppointmentWeb.Api.Controllers
        {
            public class AppointmentsWebController : Controller
            {
                private readonly IAppointmentsService _appointmentsService;

                public AppointmentsWebController(IAppointmentsService appointmentsService)
                {
                    _appointmentsService = appointmentsService;
                }

                [Route("Appointments")]
                public async Task<IActionResult> Index()
                {
                    var result = await _appointmentsService.GetAll();

                    if (result.IsSuccess && result.Data is List<AppoinmentsGetDto> dto)
                    {
                        return View(dto);
                    }

                    ViewBag.Message = result.Message;
                    return View(new List<AppoinmentsGetDto>());
                }

                public async Task<IActionResult> Details(int id)
                {
                    var result = await _appointmentsService.GetById(id);

                    if (result.IsSuccess && result.Data is Appointments appointment)
                    {
                        return View(appointment);
                    }

                    ViewBag.Message = result.Message;
                    return View();
                }

                public IActionResult Create()
                {
                    return View();
                }

                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Create(AppointmentsSaveDto appointmentsSave)
                {
                    if (!ModelState.IsValid)
                    {
                        ViewBag.Message = "Datos inválidos.";
                        return View();
                    }

                    var result = await _appointmentsService.SaveAsync(appointmentsSave);

                    if (result.IsSuccess)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ViewBag.Message = result.Message;
                    return View();
                }

                public async Task<IActionResult> Edit(int id)
                {
                    var result = await _appointmentsService.GetById(id);

                    if (result.IsSuccess && result.Data is Appointments appointment)
                    {
                        var dto = new AppointmentsUpdateDto
                        {
                            AppointmentID = appointment.AppointmentID,
                            PatientID = appointment.PatientID,
                            DoctorID = appointment.DoctorID,
                            AppointmentDate = appointment.AppointmentDate,
                            StatusID = appointment.StatusID
                        };

                        return View(dto);  
                    }

                    ViewBag.Message = result.Message;
                    return RedirectToAction(nameof(Index));
                }


                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Edit(int id, AppointmentsUpdateDto appointmentsUpdate)
                {
                    if (!ModelState.IsValid)
                    {
                        ViewBag.Message = "Datos inválidos.";
                        return View(appointmentsUpdate);  
                    }

                    var result = await _appointmentsService.UpdateAsync(appointmentsUpdate);

                    if (result.IsSuccess)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ViewBag.Message = result.Message;
                    return View(appointmentsUpdate);  
                }

            }
        }

    }

}
