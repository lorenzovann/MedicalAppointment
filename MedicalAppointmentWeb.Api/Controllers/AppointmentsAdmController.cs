using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentWeb.Api.Controllers
{
    public class AppointmentsAdmController : Controller
    {
        // GET: AppointmentsAdmController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AppointmentsAdmController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppointmentsAdmController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppointmentsAdmController/Create
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

        // GET: AppointmentsAdmController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AppointmentsAdmController/Edit/5
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

        // GET: AppointmentsAdmController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppointmentsAdmController/Delete/5
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
