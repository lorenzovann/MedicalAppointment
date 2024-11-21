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

    public class AppointmentsWebController : Controller
    { 
        private readonly IAppointmentsService _appointmentsService;

        public AppointmentsWebController (IAppointmentsService appointmentsService)
        {
            _appointmentsService = appointmentsService;
        }

        [Route("Appointments")]
        public async Task<IActionResult> Index()
        {
            var result = await _appointmentsService.GetAll();

            if(result.IsSuccess)
            {
                List<AppoinmentsGetDto> dto =(List<AppoinmentsGetDto>)result.Data!;
                return View(dto);
            }
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _appointmentsService.GetById(id);

            if (result.IsSuccess && result.Data is List<Appointments> appointments)
            {
                var ap = appointments.FirstOrDefault(a => a.AppointmentID == id);
                if (ap != null)
                {
                    return View(ap);
                }
            }

            return View(); 
        }

        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentsSaveDto appointmentsSave)
        {

            try
            {

                
                var result = await _appointmentsService.SaveAsync(appointmentsSave);

                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));

                }
                else 
                { 
                    ViewBag.Message = result.Message;
                    return View();
                
                }


            }
            catch
            {
                return View();
            }
        }


        public async Task<IActionResult>  Edit(int id)
        {
            var result = await _appointmentsService.GetById(id);

            if (result.IsSuccess && result.Data is List<Appointments> appointments)
            {
                var ap = appointments.FirstOrDefault(a => a.AppointmentID == id);
                if (ap != null)
                {
                    return View(ap);
                }
            }

            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppointmentsUpdateDto appointmentsUpdate)
        {
            try
            {


                var result = await _appointmentsService.UpdateAsync(appointmentsUpdate);

                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ViewBag.Message = result.Message;
                    return View();

                }


            }
            catch
            {
                return View();
            }
        }

        
    }
}
