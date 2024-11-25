

using MedicalAppointment.Application.Contracts.InsuranceContracts;
using MedicalAppointment.Application.Dto.Dtosappointments.Appointments;
using MedicalAppointment.Application.Dto.DtosInsurance.InsuranceProvidersDtos;
using MedicalAppointment.Application.Responses.appointmentsResponses;
using MedicalAppointment.Application.Responses.InsuranceResponses;
using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Domain.Entities.Insurance;
using MedicalAppointment.Persistance.Interfaces.Insurance;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Application.Services.InsuranceService
{
    public class InsuranceProvidersService : IInsuranceProvidersService
    {
        private readonly IInsuranceProvidersRepository _insuranceProvidersRepository;
        private readonly ILogger<InsuranceProvidersService> _logger;

        public InsuranceProvidersService(IInsuranceProvidersRepository insuranceProvidersRepository,
                                         ILogger<InsuranceProvidersService> logger)
        {
            _insuranceProvidersRepository = insuranceProvidersRepository;
            _logger = logger;
        }


        public async Task<InsuranceProvidersResponse> GetAll()
        {
            InsuranceProvidersResponse insuranceProvidersResponse = new InsuranceProvidersResponse();
            try
            {
                var result = await _insuranceProvidersRepository.GetAll();

                if (result.Data is List<InsuranceProviders> insuranceProvidersList)
                {
                    insuranceProvidersResponse.Data = insuranceProvidersList
                                                      .Select(insuranceProviders => new InsuranceProvidersGetDto
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
                                                          CustomerSupportContact = insuranceProviders.CustomerSupportContact,
                                                          AcceptedRegions = insuranceProviders.AcceptedRegions,
                                                          MaxCoverageAmount = insuranceProviders.MaxCoverageAmount,

                                                      }).ToList();
                    insuranceProvidersResponse.IsSuccess = true;
                    insuranceProvidersResponse.Message = "Listado de InsuranceProviders obtenido con éxito.";
                }
                else
                {
                    insuranceProvidersResponse.IsSuccess = false;
                    insuranceProvidersResponse.Message = "No se encontraron InsuranceProviders.";
                }

            }
            catch (Exception ex)
            {
                insuranceProvidersResponse.IsSuccess = false;
                insuranceProvidersResponse.Message = $"Error {ex.Message} obteniendo InsuranceProviders.";
                _logger.LogError(insuranceProvidersResponse.Message, ex.ToString());

            }
            return insuranceProvidersResponse;
        }

        public async Task<InsuranceProvidersResponse> GetById(int id)
        {
            InsuranceProvidersResponse insuranceProvidersResponse = new InsuranceProvidersResponse();

            try
            {
                var result = await _insuranceProvidersRepository.GetEntityBy(id);

                if (result.Data)
                {
                    insuranceProvidersResponse.Data = result.Success;
                    insuranceProvidersResponse.Message = result.Message;
                    return insuranceProvidersResponse;
                }
                insuranceProvidersResponse.Data = result.Data;

            }
            catch (Exception ex)
            {
                insuranceProvidersResponse.IsSuccess = false;
                insuranceProvidersResponse.Message = $"Error {ex.Message} obteniendo InsuranceProviders";
                _logger.LogError(insuranceProvidersResponse.Message, ex.ToString());

            }
            return insuranceProvidersResponse;
        }

        public async Task<InsuranceProvidersResponse> SaveAsync(InsuranceProvidersSaveDto dto)
        {
            InsuranceProvidersResponse insuranceProdiversResponse = new InsuranceProvidersResponse();

            try
            {
                InsuranceProviders insuranceProviders = new InsuranceProviders
                {


                };


                var saveResult = await _insuranceProvidersRepository.Save(insuranceProviders);

                if (saveResult.Success)
                {
                    InsuranceProvidersSaveDto saveDto = new InsuranceProvidersSaveDto
                    {


                    };

                    insuranceProdiversResponse.Data = saveDto;
                    insuranceProdiversResponse.IsSuccess = true;
                    insuranceProdiversResponse.Message = "Appointments guardado exitosamente.";
                }
                else
                {
                    insuranceProdiversResponse.IsSuccess = false;
                    insuranceProdiversResponse.Message = saveResult.Message;
                }
            }

            catch (Exception ex)
            {
                insuranceProdiversResponse.IsSuccess = false;
                insuranceProdiversResponse.Message = $"Error {ex.Message} tratando de guardar Appointments.";
                _logger.LogError(insuranceProdiversResponse.Message, ex.ToString());
            }

            return insuranceProdiversResponse;
        }

        public async Task<InsuranceProvidersResponse> UpdateAsync(InsuranceProvidersUpdateDto dto)
        {
            InsuranceProvidersResponse insuranceProdiversResponse = new InsuranceProvidersResponse();

            try
            {
                var resultGetById = await _insuranceProvidersRepository.GetEntityBy(dto.InsuranceProviderID);

                if (!resultGetById.Success)
                {
                    insuranceProdiversResponse.IsSuccess = resultGetById.Success;
                    insuranceProdiversResponse.Message = resultGetById.Message;

                    return insuranceProdiversResponse;
                }

                InsuranceProviders insuranceProviders = new InsuranceProviders();

                insuranceProviders.InsuranceProviderID = dto.InsuranceProviderID;
                insuranceProviders.Name = dto.Name;
                insuranceProviders.ContactNumber = dto.ContactNumber;
                insuranceProviders.Email = dto.Email;
                insuranceProviders.Website = dto.Website;
                insuranceProviders.Address = dto.Address;
                insuranceProviders.City = dto.City;
                insuranceProviders.State = dto.State;
                insuranceProviders.Country = dto.Country;
                insuranceProviders.ZipCode = dto.ZipCode;
                insuranceProviders.CoverageDetails = dto.CoverageDetails;
                insuranceProviders.LogoUrl = dto.LogoUrl;
                insuranceProviders.IsPreferred = dto.IsPreferred;
                insuranceProviders.NetworkTypeId = dto.NetworkTypeId;
                insuranceProviders.CustomerSupportContact = dto.CustomerSupportContact;
                insuranceProviders.AcceptedRegions = dto.AcceptedRegions;
                insuranceProviders.MaxCoverageAmount = dto.MaxCoverageAmount;

                

                var result = await _insuranceProvidersRepository.Update(insuranceProviders);

            }
            catch (Exception ex)
            {
                insuranceProdiversResponse.IsSuccess = false;
                insuranceProdiversResponse.Message = "Error al actualizar el InsuranceProviders";
                _logger.LogError(insuranceProdiversResponse.Message, ex.ToString());

            }
            return insuranceProdiversResponse;
        }
    }
}
