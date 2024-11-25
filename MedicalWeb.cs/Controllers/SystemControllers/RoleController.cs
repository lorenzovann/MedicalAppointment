using Medical.Domain.Entities.Confi.Systems;
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
        public async Task<IActionResult> Details(int id)
        {
            var result = await _roleservices.GetById(id);
            if (result.success)
            {
                Role role = (Role)result.model!;
                return View(role);

            }
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
        public async Task<IActionResult> Create(SaveRolesDtos role)
        {
            try
            {
                role.CreatedAt = DateTime.Now;
                role.UpdatedAt = DateTime.Now;
                var result = await _roleservices.SaveAsync(role);

                if (result.success)
                {
                    return RedirectToAction(nameof(Index));

                }

                ViewBag.Menssage = result.success;
                return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: RoleController1/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _roleservices.GetById(id);
            if (result.success)
            {
                Role role = (Role)result.model!;
                return View(role);

            }
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

    }
}

