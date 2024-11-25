using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppoinmentWeb.cs.Controllers.UsersControllers
{
    public class UserAdmController : Controller
    {
        // GET: UserAdmController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserAdmController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserAdmController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserAdmController/Create
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

        // GET: UserAdmController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserAdmController/Edit/5
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

        // GET: UserAdmController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserAdmController/Delete/5
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
