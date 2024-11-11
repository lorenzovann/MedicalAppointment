

using Azure;
using Medical.Domain.Entities.Confi.Systems;
using MedicalAppointment.Persistance.Interfaces.Configuration.SystemIntefaces;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using MedicalCoreAplications.cs.Contracts.systems;
using MedicalCoreAplications.cs.Dtos.Systems.RolesDtos.cs;
using MedicalCoreAplications.cs.Dtos.Systems.StatusDtos.cs;
using MedicalCoreAplications.cs.Response.Systems;
using Microsoft.Extensions.Logging;

namespace MedicalCoreAplications.cs.Services
{
    public class StatusServices : IStatusServices
    {

        private readonly IStatusInterfaces _statusrepositorie;
        private readonly ILogger<StatusServices> _logger; 

        public StatusServices(IStatusInterfaces statusrepositorie, ILogger<StatusServices> logger)
        {  
             if(statusrepositorie is null) throw new ArgumentNullException(nameof(statusrepositorie));



            _statusrepositorie = statusrepositorie;
            this._logger = logger;
        }

         public  async Task<StatusResponse> getall()
        {
            StatusResponse response = new StatusResponse();


            
            try
            {
                var result = await _statusrepositorie.Getall();

                List<GetStatusDtos> getStatusDtos = ((List<Status>)result.data!)
                  .Select(status => new GetStatusDtos
                  {
                      StatusID = status.StatusID,
                      StatusName = status.StatusName,

                  }).ToList();

                response.model = getStatusDtos;
                response.Menssaje = "notifications List Correctly! ";

            }
            catch (Exception ex)
            {

                response.success = false;
                response.Menssaje = $"Erro {ex.Message} triying to List all of the regiter! ";
               _logger.LogError(response.Menssaje, ex.ToString());
            }

            return response;
        }

        public  async Task<StatusResponse> GetById(int id)
        {
            StatusResponse response = new StatusResponse(); 

            try
            {
                var result = await _statusrepositorie.GetEntitiebyId(id); 
                if (result != null)
                {
                    Status status = (Status)result.data!;

                    GetStatusDtos datos = new GetStatusDtos()
                    {
                        StatusID = id,
                        StatusName = status.StatusName,
                        CreateAt = status.CreateAt,


                    };

                    response.model = datos;
                    response.Menssaje = "Id Found it Succefully! ";

                }
                else
                {
                    response.success = false;
                    response.Menssaje = "Erro not found id!";

                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Error ocur {ex.Message}";
                _logger.LogError(response.Menssaje, ex.ToString());
            }

            return response;
        }

        public async  Task<StatusResponse> SaveAsync(SaveStatusDtos dto)
        {
            StatusResponse response = new StatusResponse();

            try
            {
                Status status = new Status();
                status.StatusName = dto.StatusName;


                var datos = await _statusrepositorie.Add(status);
                response.model = datos;
                response.Menssaje = "The Status has been registered successfully!";


            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Error ocur {ex.Message}";
                _logger.LogError(response.Menssaje, ex.ToString());
            }

            return response;
        }

        public async Task<StatusResponse> UpdateAsync(UpdateStatusDtos dto)
        {
            StatusResponse response = new StatusResponse();

            try
            {
                var result = await _statusrepositorie.GetEntitiebyId(dto.StatusID);
                if (result != null)
                {
                    Status status = (Status)result.data!;

                    status.StatusName = dto.StatusName;
                    status.StatusID = dto.StatusID;


                    var datos = await _statusrepositorie.Update(status);
                    response.model = datos;
                    response.Menssaje = "Status updated sucefully!";
                }
                else
                {
                    response.success = false;
                    response.Menssaje = "ERR Entitie not found on the regiter! ";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Error ocur {ex.Message} update Status! ";
                _logger.LogError(response.Menssaje, ex.ToString());
            }
            return response;

        }
    }
}
