using MedicalAppointment.Application.Contracts.appointmentsContracts;
using MedicalAppointment.Application.Dto.Dtosappointments.AppointmentsDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Web.Controllers
{
    public class AppointmentsWebController : Controller
    {
        private readonly IAppointmentsService _appointmentsService;

        public AppointmentsWebController(IAppointmentsService appointmentsService)
        {
            _appointmentsService = appointmentsService;

        }

        public async Task<IActionResult> Index()
        {
            var result = await _appointmentsService.GetAll();
            
            if (result.IsSuccess)
            {
                List<AppoinmentsGetDto> appoinmentsGetDtos = (List<AppoinmentsGetDto>)result.Data;
                
                return View(appoinmentsGetDtos);
            }
            return View();
        }


        public ActionResult Details(int id)
        {
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}
