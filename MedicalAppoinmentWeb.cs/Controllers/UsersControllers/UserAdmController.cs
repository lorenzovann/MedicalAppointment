using MedicalCoreAplications.cs.Dtos.Configurations.UserDtos.cs;
using MedicalWeb.cs.Models.Base;
using MedicalWeb.cs.Models.BaseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MedicalAppoinmentWeb.cs.Controllers.UsersControllers
{
    public class UserAdmController : Controller
    {
        private readonly string _baseUrl = "http://localhost:5298/api/";

        // GET: UserAdmController
        public async Task<IActionResult> Index()
        {
            UserGetAllModel model = new UserGetAllModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var responseTask = await client.GetAsync("Users/GetUsers");

                if (responseTask.IsSuccessStatusCode)
                {
                    string response = await responseTask.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<UserGetAllModel>(response)!;
                }
            }
            return View(model.data);
        }

        // GET: UserAdmController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            UserGetByIdModel model = new UserGetByIdModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var responseTask = await client.GetAsync($"Users/GetUserById?id={id}");

                if (responseTask.IsSuccessStatusCode)
                {
                    string response = await responseTask.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<UserGetByIdModel>(response)!;
                }
            }
            return View(model.data);
        }

        // GET: UserAdmController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserAdmController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveUserDtos user)
        {
            BaseApiResponse model = new BaseApiResponse();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUrl);
                    var responseTask = await client.PostAsJsonAsync("Users/SaveUser", user);

                    if (responseTask.IsSuccessStatusCode)
                    {
                        string response = await responseTask.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<BaseApiResponse>(response)!;

                        if (!model.Success)
                        {
                            ViewBag.Message = model.menssage;
                            return View();
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        string response = await responseTask.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<BaseApiResponse>(response)!;

                        ViewBag.Message = model.menssage;
                        return View();
                    }
                }
            }
            catch
            {
                ViewBag.Message = "An error occurred while creating the user.";
                return View();
            }
        }

        // GET: UserAdmController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            UserGetByIdModel model = new UserGetByIdModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var responseTask = await client.GetAsync($"Users/GetUserById?id={id}");

                if (responseTask.IsSuccessStatusCode)
                {
                    string response = await responseTask.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<UserGetByIdModel>(response)!;
                }
            }
            return View(model.data);
        }

        // POST: UserAdmController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SaveUserDtos user)
        {
            BaseApiResponse model = new BaseApiResponse();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUrl);
                    var responseTask = await client.PutAsJsonAsync("Users/UpdateUser", user);

                    if (responseTask.IsSuccessStatusCode)
                    {
                        string response = await responseTask.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<BaseApiResponse>(response)!;

                        if (!model.Success)
                        {
                            ViewBag.Message = model.menssage;
                            return View(user);
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        string response = await responseTask.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<BaseApiResponse>(response)!;

                        ViewBag.Message = model.menssage;
                        return View(user);
                    }
                }
            }
            catch
            {
                ViewBag.Message = "An error occurred while updating the user.";
                return View(user);
            }
        }

        // GET: UserAdmController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            UserGetByIdModel model = new UserGetByIdModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var responseTask = await client.GetAsync($"Users/GetUserById?id={id}");

                if (responseTask.IsSuccessStatusCode)
                {
                    string response = await responseTask.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<UserGetByIdModel>(response)!;
                }
            }
            return View(model.data);
        }

        // POST: UserAdmController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            BaseApiResponse model = new BaseApiResponse();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUrl);
                    var responseTask = await client.DeleteAsync($"Users/DeleteUser?id={id}");

                    if (responseTask.IsSuccessStatusCode)
                    {
                        string response = await responseTask.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<BaseApiResponse>(response)!;

                        if (!model.Success)
                        {
                            ViewBag.Message = model.menssage;
                            return View();
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        string response = await responseTask.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<BaseApiResponse>(response)!;

                        ViewBag.Message = model.menssage;
                        return View();
                    }
                }
            }
            catch
            {
                ViewBag.Message = "An error occurred while deleting the user.";
                return View();
            }
        }
    }
}



