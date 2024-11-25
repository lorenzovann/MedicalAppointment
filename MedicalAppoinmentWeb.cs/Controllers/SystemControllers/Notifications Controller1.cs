using Medical.Domain.Entities.Confi.Systems;
using MedicalCoreAplications.cs.Contracts.Systems;
using MedicalCoreAplications.cs.Dtos.Configurations.UserDtos.cs;
using MedicalCoreAplications.cs.Dtos.Systems.NotificationsDtos.cs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace MedicalAppoinmentWeb.cs.Controllers.SystemControllers
{
    public class NotificationsController1 : Controller
    {
        private readonly INotificationServices _notificationServices;

        public NotificationsController1(INotificationServices notificationServices)
        {
            _notificationServices = notificationServices;

        }
        [Route("Notifications")]
        public async Task<IActionResult> Index()
        {

            var result = await _notificationServices.getall();
            if (result.success)
            {
                List<GetNotificationsDtos> noti = (List<GetNotificationsDtos>)result.model!;

                return View(noti);

            }
            return View();
        }

        // GET: NotificationsController1/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _notificationServices.GetById(id);
            if (result.success)
            {
                GetNotificationsDtos notifications = (GetNotificationsDtos)result.model!;

                return View(notifications);

            }
            return View();
        }

        // GET: NotificationsController1/Create

        public IActionResult Create()
        {

            return View();


        }


        // POST: NotificationsController1/Create
        [HttpPost("Notifications")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveNotificationsDto noti)
        {
            try
            {
                noti.SentAt = DateTime.Now;
                var result = await _notificationServices.SaveAsync(noti);
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

        // GET: NotificationsController1/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _notificationServices.GetById(id);
            if (result.success)
            {
                GetNotificationsDtos noti = (GetNotificationsDtos)result.model!;

                return View(noti);
            }

            return View();
        }

        // POST: NotificationsController1/Edit/5
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

        // GET: NotificationsController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NotificationsController1/Delete/5
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