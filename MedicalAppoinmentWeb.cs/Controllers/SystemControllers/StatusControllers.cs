using MedicalCoreAplications.cs.Contracts.Systems;
using MedicalCoreAplications.cs.Dtos.Systems.StatusDtos.cs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppoinmentWeb.cs.Controllers.SystemControllers
{
    public class StatusController1 : Controller
    {
        private readonly IStatusServices _statusservices;

        public StatusController1(IStatusServices statusservices)
        {
            _statusservices = statusservices;

        }
        [Route("Status")]
        public async Task<IActionResult> Index()
        {
            var result = await _statusservices.getall();
            if (result.success)
            {

                List<GetStatusDtos> status = (List<GetStatusDtos>)result.model!;

                return View(status);

            }
            return View();
        }

        // GET: StatusController1/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _statusservices.GetById(id);
            if (result.success)
            {
                GetStatusDtos status = (GetStatusDtos)result.model!;
                return View(status);

            }
            return View();
        }

        // GET: StatusController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create(SaveStatusDtos status)
        {
            try
            {
                status.CreateAt = DateTime.Now;
                var result = await _statusservices.SaveAsync(status);
                if (result.success)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ViewBag.Menssage = " ";
                    return View();


                }
            }
            catch
            {
                return View();
            }
        }

        // GET: StatusController1/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _statusservices.GetById(id);
            if (result.success)
            {
                GetStatusDtos status = (GetStatusDtos)result.model!;
                return View(status);
            }
            return View();
        }

        // POST: StatusController1/Edit/5
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