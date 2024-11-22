using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentWeb.Api.Controllers
{
    public class DoctorAvailabilityAdmController : Controller
    {
        // GET: DoctorAvailabilityAdmController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DoctorAvailabilityAdmController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DoctorAvailabilityAdmController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorAvailabilityAdmController/Create
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

        // GET: DoctorAvailabilityAdmController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DoctorAvailabilityAdmController/Edit/5
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

        // GET: DoctorAvailabilityAdmController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DoctorAvailabilityAdmController/Delete/5
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
