

using MedicalAppointment.Application.Contracts.InsuranceContracts;
using MedicalAppointment.Application.Dto.DtosInsurance.NetworkTypeDtos;
using MedicalAppointment.Application.Responses.appointmentsResponses;
using MedicalAppointment.Application.Responses.InsuranceResponses;
using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Domain.Entities.Insurance;
using MedicalAppointment.Persistance.Interfaces.Insurance;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Application.Services.InsuranceService
{
    public class NetworkTypeService : INetworkTypeService
    {
        private readonly INetworkTypeRepository _networkTypeRepository;
        private readonly ILogger<NetworkTypeService> _logger;


        public NetworkTypeService(INetworkTypeRepository networkTypeRepository,
                                   ILogger<NetworkTypeService> logger)
        {
            _networkTypeRepository = networkTypeRepository;
            _logger = logger;

        }

        public async Task<NetworkTypeResponse> GetAll()
        {
            NetworkTypeResponse networkTypeResponse = new NetworkTypeResponse();

            try
            {
                var result = await _networkTypeRepository.GetAll();

                if (result.Data is List<NetworkType> networkTypeList)
                {
                    networkTypeResponse.Data = networkTypeList
                        .Select(networkType => new NetworkType
                        {
                            NetworkTypeId = networkType.NetworkTypeId,
                            Name = networkType.Name,
                            Description = networkType.Description,
                            CreatedAt = networkType.CreatedAt,
                            UpdatedAt = networkType.UpdatedAt,
                            IsActive = networkType.IsActive,


                        }).ToList();

                    networkTypeResponse.IsSuccess = true;
                    networkTypeResponse.Message = "Listado de NetworkType obtenido exitosamente";

                }
                else
                {
                    networkTypeResponse.IsSuccess = false;
                    networkTypeResponse.Message = "No se encontraron NetworkType";
                }

            }

            catch (Exception ex)
            {
                networkTypeResponse.IsSuccess = false;
                networkTypeResponse.Message = $"Error {ex.Message} obteniendo NetworkType";
                _logger.LogError(networkTypeResponse.Message, ex.ToString);

            }
            return networkTypeResponse;
        }

        public async Task<NetworkTypeResponse> GetById(int id)
        {
            NetworkTypeResponse networkTypeResponse = new NetworkTypeResponse();

            try
            {
                var result = await _networkTypeRepository.GetEntityBy(id);

                if(!result.Success)
                {
                    networkTypeResponse.IsSuccess = result.Data;
                    networkTypeResponse.Message =  result.Message;
                    
                }
                
                networkTypeResponse.Data = result.Data;
            }

            catch(Exception ex)  
            { 
                networkTypeResponse.IsSuccess = false;
                networkTypeResponse.Message = $"Error {ex.Message} obteniendo NetworkType";
                _logger.LogError(networkTypeResponse.Message, ex.ToString());
            
            }
            return networkTypeResponse;

        }

        public async Task<NetworkTypeResponse> SaveAsync(NetworkTypeSaveDto dto)
        {
            NetworkTypeResponse networkTypeResponse = new NetworkTypeResponse();

            try
            {

                NetworkType networkType = new NetworkType
                {
                    Name = dto.Name,
                    Description = dto.Description,

                };


                var result = await _networkTypeRepository.Save(networkType);

                if(result.Success)
                {
                    NetworkTypeSaveDto SaveDto = new NetworkTypeSaveDto
                    {

                        Name = networkType.Name,
                        Description = networkType.Description,
                    };

                    networkTypeResponse.Data = SaveDto;
                    networkTypeResponse.IsSuccess=true;
                    networkTypeResponse.Message = "NetworkType guardado exitosamente";


                }
                else
                {
                    networkTypeResponse.IsSuccess = false;
                    networkTypeResponse.Message = result.Message;
                }

            }

            catch (Exception ex)
            {
                networkTypeResponse.IsSuccess = false;
                networkTypeResponse.Message = $"Error {ex.Message} guardando NetworkType";
                _logger.LogError(networkTypeResponse.Message, ex.ToString());
            }
            return networkTypeResponse;
        }

        public async Task<NetworkTypeResponse> UpdateAsync(NetworkTypeUpdateDto dto)
        {
            NetworkTypeResponse networkTypeResponse = new NetworkTypeResponse();

            try
            {
                var resultGetById = await _networkTypeRepository.GetEntityBy(dto.NetworkTypeId);

                if (!resultGetById.Success)
                {
                    networkTypeResponse.IsSuccess = resultGetById.Success;
                    networkTypeResponse.Message = resultGetById.Message;
                    

                    return networkTypeResponse;
                }

                NetworkType? networkType = new NetworkType();

                networkType.NetworkTypeId = dto.NetworkTypeId;
                networkType.Name = dto.Name;
                networkType.Description = dto.Description;
                networkType.UpdatedAt = dto.UpdatedAt;


                var result = await _networkTypeRepository.Update(networkType);

            }
            catch (Exception ex)
            {
                networkTypeResponse.IsSuccess = false;
                networkTypeResponse.Message = "Error al actualizar el appointment";
                _logger.LogError(networkTypeResponse.Message, ex.ToString());

            }
            return networkTypeResponse;
        }
    }
}
