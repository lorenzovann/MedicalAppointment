

using Azure;
using Medical.Domain.Entities.Confi.Systems;
using Medical.Domain.Entities.Confi.Users;
using MedicalAppoiment.Aplication.cs.Contracts.systems;
using MedicalAppoiment.Aplication.cs.Dtos.Systems.StatusDtos.cs;
using MedicalAppoiment.Aplication.cs.Response.systems;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Logging;
using System.Xml.XPath;

namespace MedicalAppoiment.Aplication.cs.Services.systems
{
    public class StatusServices : IStatusServices
    {    

        private readonly StatusRepositorie _responses;
        private readonly ILogger<StatusServices> _logger;

        public StatusServices(StatusRepositorie responses, 
                           ILogger<StatusServices> logger)
        {
            if(responses == null) throw new ArgumentNullException(nameof(responses));

            _responses = responses;
            this._logger = logger;
        }
        public async Task<StatusResponses> getall()
        {
           StatusResponses response = new StatusResponses();

            try
            {
                var result = await _responses.Getall();

                List<GetStatusDtos> getStatusDtos = ((List<Status>)result.data)
                    .Select(status => new GetStatusDtos
                {
                    StatusID = status.StatusID,
                    StatusName = status.StatusName,
                
                }).ToList();
                 
              response.model = getStatusDtos;
              response.Menssage = "notifications List Correctly! ";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Error ocur {ex.Message}";
               _logger.LogError(response.Menssage, ex.ToString()); 

            }


            
            return response;

        }

        public async Task<StatusResponses> GetById(int id)
        {
            StatusResponses response = new StatusResponses();

            try
            {
                var result = await _responses.GetEntitiebyId(id);
                if (result != null)
                {
                    Status status = (Status)result.data;

                    GetStatusDtos datos = new GetStatusDtos()
                    {
                        StatusID = status.StatusID,
                        StatusName = status.StatusName,
                 
                    };

                    response.model = datos;
                    response.Menssage = "Id Found it Succefully! ";

                }
                else
                {  
                    response.Success = false;
                    response.Menssage = "Erro not found id!";
                    
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Error ocur {ex.Message}";
               _logger.LogError(response.Menssage, ex.ToString());
            }

            return response;

        }

        public async Task<StatusResponses> SaveAsync(StatusSaveDtos dto)
        {
            StatusResponses response = new StatusResponses();

            try
            {
                Status status  = new Status();
                status.StatusName = dto.StatusName;
                

                var datos = await _responses.Add(status);
                response.model = datos;
                response.Menssage = "The Status has been registered successfully!";


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Error ocur {ex.Message}";
               _logger.LogError(response.Menssage, ex.ToString());
            }

            return response;

        }

        public async Task<StatusResponses> UpdateAsync(StatusUpdateDtos dto)
        {
            StatusResponses response = new StatusResponses();

            try
            {
                var result = await _responses.GetEntitiebyId(dto.StatusID);
                if (result != null)
                {
                    Status status = (Status)result.data; 

                    status.StatusName = dto.StatusName;
                    status.StatusID = dto.StatusID;
                   
                   
                    var datos = await _responses.Update(status);
                    response.model = datos;
                    response.Menssage = "Status updated sucefully!";
                } else
                {
                    response.Success = false;
                    response.Menssage = "ERR Entitie not found on the regiter! ";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Error ocur {ex.Message} update Status! ";
               _logger.LogError(response.Menssage, ex.ToString());
            }
            return response;

        }
    }
}
