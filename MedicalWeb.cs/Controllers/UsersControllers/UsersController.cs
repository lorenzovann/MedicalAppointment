
using MedicalAppointment.Persistance.Model;
using MedicalCoreAplications.cs.Contracts.Configurations;
using MedicalCoreAplications.cs.Dtos.Configurations.UserDtos.cs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalWeb.cs.Controllers.UsersCotrollers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userservices;

        public UserController(IUserServices userservices)
        {
            _userservices = userservices;

        }
        [Route("Users")]
        public async Task<IActionResult> Index()
        {

            var result = await _userservices.getall();
            if (result.success)
            {
                List<UserModel> users = (List<UserModel>)result.model!;
                return View(users);

            }
            return View();
        }

        // GET: UserController/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _userservices.GetById(id);
            if (result.success)
            {
                UserModel userfind = (UserModel)result.model!;

                return View(userfind);
            }
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveUserDtos user)
        {
            try
            {
                user.UpdatedAt = DateTime.Now;

                var result = await _userservices.SaveAsync(user);
                if (result.success)
                {

                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ViewBag.Menssage = result.Menssaje;
                    return View();

                }
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _userservices.GetById(id);
            if (result.success)
            {
                UserModel user = (UserModel)result.model!;
                return View(user);


            }
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateUserDtos user)
        {
            try
            {

                user.UpdatedAt = DateTime.Now;
                user.UserId = 1;


                var result = await _userservices.UpdateAsync(user);

                if (result.success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Menssage = result.Menssaje;
                    return View();

                }

            }
            catch
            {
                return View();
            }
        }



    }
}