using MedicalAppointment.Application.Contracts.InsuranceContracts;
using MedicalAppointment.Application.Dto.Dtosappointments.DoctorAvailabilityDtos;
using MedicalAppointment.Application.Dto.DtosInsurance.InsuranceProvidersDtos;
using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Domain.Entities.Insurance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentWeb.Api.Controllers
{
    public class InsuranceProvidersWebController : Controller
    {
        private readonly IInsuranceProvidersService _insuranceProvidersService;

        public InsuranceProvidersWebController(IInsuranceProvidersService insuranceProvidersService)
        {
            _insuranceProvidersService = insuranceProvidersService;
        }

        [Route("InsuranceProviders")]
        public async Task<IActionResult> Index()
        {
            var result = await _insuranceProvidersService.GetAll();
            if (result.IsSuccess && result.Data is List<InsuranceProviders> insuranceProviders)
            {
                return View(insuranceProviders);
            }

            ViewBag.Message = result.Message;
            return View(new List<InsuranceProviders>());
        }


        public async Task<IActionResult> Details(int id)
        {
            var result = await _insuranceProvidersService.GetById(id);

            if (result.IsSuccess && result.Data is InsuranceProviders insuranceProviders)
            {
                return View(insuranceProviders);
            }

            ViewBag.Message = result.Message;
            return View(new InsuranceProviders());
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsuranceProvidersSaveDto insuranceProvidersSaveDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Datos inválidos.";
                return View();
            }

            var result = await _insuranceProvidersService.SaveAsync(insuranceProvidersSaveDto);

            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = result.Message;
            return View();

        }



        public async Task<IActionResult> Edit(int id)
        {
            var result = await _insuranceProvidersService.GetById(id);

            if (result.IsSuccess && result.Data is InsuranceProviders insuranceProviders)
            {
                var dto = new InsuranceProvidersUpdateDto
                {
                    InsuranceProviderID = insuranceProviders.InsuranceProviderID,
                    Name = insuranceProviders.Name,
                    ContactNumber = insuranceProviders.ContactNumber,
                    Email = insuranceProviders.Email,
                    Website = insuranceProviders.Website,
                    Address = insuranceProviders.Address,
                    City = insuranceProviders.City,
                    State = insuranceProviders.State,
                    Country = insuranceProviders.Country,
                    ZipCode = insuranceProviders.ZipCode,
                    CoverageDetails = insuranceProviders.CoverageDetails,
                    LogoUrl = insuranceProviders.LogoUrl,
                    IsPreferred = insuranceProviders.IsPreferred,
                    NetworkTypeId = insuranceProviders.NetworkTypeId,
                    AcceptedRegions = insuranceProviders.AcceptedRegions,
                    MaxCoverageAmount = insuranceProviders.MaxCoverageAmount,
                    UpdatedAt = insuranceProviders.UpdatedAt,

                    

                };

                return View(dto);
            }

            ViewBag.Message = result.Message;
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id, InsuranceProvidersUpdateDto insuranceProvidersUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Datos inválidos.";
                return View(insuranceProvidersUpdateDto);
            }

            var result = await _insuranceProvidersService.UpdateAsync(insuranceProvidersUpdateDto);

            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = result.Message;
            return View(insuranceProvidersUpdateDto);
        }


    }
}
