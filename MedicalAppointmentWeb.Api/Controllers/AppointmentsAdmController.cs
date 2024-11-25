using MedicalAppointmentWeb.Api.Models.AppointmentsWebModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MedicalAppointmentWeb.Api.Controllers
{
    public class AppointmentsAdmController : Controller
    {
        
       
        public async Task<IActionResult> Index()
        {
            String url = "http://localhost:5031/Api/";

            AppointmentsGetAllModel appointmentsGetAllModel = new AppointmentsGetAllModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var responseTask = await client.GetAsync("Appointments/GetAppointments");

                if (responseTask.IsSuccessStatusCode)
                {
                    string response = await responseTask.Content.ReadAsStringAsync();
                    appointmentsGetAllModel = JsonConvert.DeserializeObject<AppointmentsGetAllModel>(response); 
                }
                else 
                { 
                    ViewBag.Mesagge = "";
                
                }
            }
            return View(appointmentsGetAllModel.data);

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

       
        public ActionResult Edit(int id)
        {
            return View();
        }

       
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

       
        public ActionResult Delete(int id)
        {
            return View();
        }

        
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
