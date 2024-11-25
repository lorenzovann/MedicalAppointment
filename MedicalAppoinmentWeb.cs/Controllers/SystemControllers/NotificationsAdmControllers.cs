using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppoinmentWeb.cs.Controllers.SystemControllers
{
    public class NotificationsAdmControllers : Controller
    {
        // GET: NotificationsAdmControllers
        public ActionResult Index()
        {
            return View();
        }

        // GET: NotificationsAdmControllers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NotificationsAdmControllers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NotificationsAdmControllers/Create
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

        // GET: NotificationsAdmControllers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NotificationsAdmControllers/Edit/5
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

        // GET: NotificationsAdmControllers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NotificationsAdmControllers/Delete/5
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
