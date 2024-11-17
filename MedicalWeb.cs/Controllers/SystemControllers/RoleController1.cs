using MedicalCoreAplications.cs.Contracts.Systems;
using MedicalCoreAplications.cs.Dtos.Systems.RolesDtos.cs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalWeb.cs.Controllers.SystemControllers
{
    public class RoleController1 : Controller
    {
        private readonly IRoleServices _roleservices;

        public RoleController1(IRoleServices roleservices)
        {
            _roleservices = roleservices;

        }
        [Route("Roles")]
        public async Task<IActionResult> Index()
        {
            var result = await _roleservices.getall();

            if (result.success)
            {
                List<GetRolesDtos> roles = (List<GetRolesDtos>)result.model!;

                return View(roles);

            }
            return View();
        }

        // GET: RoleController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoleController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleController1/Create
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

        // GET: RoleController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoleController1/Edit/5
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

        // GET: RoleController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoleController1/Delete/5
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
