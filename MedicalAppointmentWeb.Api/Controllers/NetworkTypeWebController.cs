using MedicalAppointment.Application.Contracts.InsuranceContracts;
using MedicalAppointment.Application.Dto.Dtosappointments.DoctorAvailabilityDtos;
using MedicalAppointment.Application.Dto.DtosInsurance.NetworkTypeDtos;
using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Domain.Entities.Insurance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentWeb.Api.Controllers
{
    public class NetworkTypeWebController : Controller
    {
        private readonly INetworkTypeService _networkTypeService;

        public NetworkTypeWebController(INetworkTypeService networkTypeService)
        {
            _networkTypeService = networkTypeService;
        }


        [Route("NetworkType")]
        public async Task<IActionResult> Index()
        {
            var result = await _networkTypeService.GetAll();

            if (result.IsSuccess && result.Data is List<NetworkType> networkType)
            {
                return View(networkType);
            }

            ViewBag.Message = result.Message;
            return View(new List<NetworkType>());
        }


        public async Task<IActionResult> Details(int id)
        {
            var result = await _networkTypeService.GetById(id);

            if (result.IsSuccess && result.Data is NetworkType networkType)
            {
                return View(networkType);
            }

            ViewBag.Message = result.Message;
            return View(new NetworkType());
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NetworkTypeSaveDto networkTypeSaveDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Datos inválidos.";
                return View();
            }

            var result = await _networkTypeService.SaveAsync(networkTypeSaveDto);

            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = result.Message;
            return View();

        }


        public async Task<IActionResult> Edit(int id)
        {
            var result = await _networkTypeService.GetById(id);

            if (result.IsSuccess && result.Data is NetworkType networkType)
            {
                var dto = new NetworkTypeUpdateDto
                {
                    NetworkTypeId = networkType.NetworkTypeId    ,
                    Name = networkType.Name ,
                    Description = networkType.Description ,

                };

                return View(dto);
            }

            ViewBag.Message = result.Message;
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NetworkTypeUpdateDto networkTypeUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Datos inválidos.";
                return View(networkTypeUpdateDto);
            }

            var result = await _networkTypeService.UpdateAsync(networkTypeUpdateDto);

            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = result.Message;
            return View(networkTypeUpdateDto);
        }
    }
}
