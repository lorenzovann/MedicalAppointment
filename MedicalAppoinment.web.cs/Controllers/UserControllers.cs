using MedicalCoreAplications.cs.Contracts.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppoinment.web.cs.Controllers
{
    public class UserControllers : Controller
    {
        private readonly IUserServices _userservices;

        public  UserControllers(IUserServices userservices)
        {
            _userservices = userservices;
        }

        public ActionResult Index()
        {
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

        // GET: IUserControllers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IUserControllers/Edit/5
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

        // GET: IUserControllers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IUserControllers/Delete/5
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
