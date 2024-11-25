using MedicalCoreAplications.cs.Dtos.Systems.NotificationsDtos.cs;
using MedicalWeb.cs.Models.Base;
using MedicalWeb.cs.Models.SystemsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MedicalAppoinmentWeb.cs.Controllers.SystemControllers
{
    public class NotificationsAdmControllers : Controller
    {
        private readonly string _baseUrl = "http://localhost:5151/api/";

        // GET: NotificationsAdmControllers
        public async Task<IActionResult> Index()
        {
            NotificationsGetAllModel model = new NotificationsGetAllModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var responseTask = await client.GetAsync("Notifications/GetNotifications");

                if (responseTask.IsSuccessStatusCode)
                {
                    string response = await responseTask.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<NotificationsGetAllModel>(response)!;
                }
            }
            return View(model.data);
        }

        // GET: NotificationsAdmControllers/Details/5
        public async Task<IActionResult> Details(int id)
        {
              GetNotificationsByIdModelALl noti = new GetNotificationsByIdModelALl();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var responseTask = await client.GetAsync($"Notifications/GetNotificationById?id={id}");

                if (responseTask.IsSuccessStatusCode)
                {
                    string response = await responseTask.Content.ReadAsStringAsync();
                    noti = JsonConvert.DeserializeObject<GetNotificationsByIdModelALl>(response)!;
                }
            }
            return View(noti.data);
        }

        // GET: NotificationsAdmControllers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NotificationsAdmControllers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveNotificationsDto notification)
        {
            BaseApiResponse model = new BaseApiResponse();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUrl);
                    var responseTask = await client.PostAsJsonAsync("Notifications/CreateNotification", notification);

                    if (responseTask.IsSuccessStatusCode)
                    {
                        string response = await responseTask.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<BaseApiResponse>(response)!;

                        if (!model.Success)
                        {
                            ViewBag.Message = model.menssage;
                            return View(notification);
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        string response = await responseTask.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<BaseApiResponse>(response)!;

                        ViewBag.Message = model.menssage;
                        return View(notification);
                    }
                }
            }
            catch
            {
                ViewBag.Message = "An error occurred while creating the notification.";
                return View(notification);
            }
        }

        // GET: NotificationsAdmControllers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GetNotificationsByIdModelALl noti = new GetNotificationsByIdModelALl();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var responseTask = await client.GetAsync($"Notifications/GetNotificationById?id={id}");

                if (responseTask.IsSuccessStatusCode)
                {
                    string response = await responseTask.Content.ReadAsStringAsync();
                    noti = JsonConvert.DeserializeObject<GetNotificationsByIdModelALl>(response)!;
                }
            }
            return View(noti.data);
        }

        // POST: NotificationsAdmControllers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SaveNotificationsDto notification)
        {
            BaseApiResponse model = new BaseApiResponse();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUrl);
                    var responseTask = await client.PutAsJsonAsync("Notifications/UpdateNotification", notification);

                    if (responseTask.IsSuccessStatusCode)
                    {
                        string response = await responseTask.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<BaseApiResponse>(response)!;

                        if (!model.Success)
                        {
                            ViewBag.Message = model.menssage;
                            return View(notification);
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        string response = await responseTask.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<BaseApiResponse>(response)!;

                        ViewBag.Message = model.menssage;
                        return View(notification);
                    }
                }
            }
            catch
            {
                ViewBag.Message = "An error occurred while editing the notification.";
                return View(notification);
            }
        }

      
    }
}


