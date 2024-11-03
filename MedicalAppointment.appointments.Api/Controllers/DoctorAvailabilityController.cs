using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.appointments.Api.Controllers
{
    public class DoctorAvailabilityController : Controller
    {
        // GET: DoctorAvailability
        public ActionResult Index()
        {
            return View();
        }

        // GET: DoctorAvailability/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DoctorAvailability/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorAvailability/Create
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

        // GET: DoctorAvailability/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DoctorAvailability/Edit/5
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

        // GET: DoctorAvailability/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DoctorAvailability/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
