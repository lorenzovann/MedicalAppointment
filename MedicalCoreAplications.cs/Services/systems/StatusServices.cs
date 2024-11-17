
using Medical.Domain.Entities.Confi.Systems;
using MedicalAppointment.Persistance.Interfaces.Configuration.SystemIntefaces;
using MedicalCoreAplications.cs.Base;
using MedicalCoreAplications.cs.Contracts.Systems;
using MedicalCoreAplications.cs.Dtos.Systems.StatusDtos.cs;
using MedicalCoreAplications.cs.Response.Systems;
using Microsoft.Extensions.Logging;

namespace MedicalCoreAplications.cs.Services.systems
{
    public class StatusServices : IStatusServices
    {
        private readonly IStatusRepository _statusrepository;
        private readonly ILogger<StatusServices> _logger;

        public StatusServices(IStatusRepository statusrepository, ILogger<StatusServices> logger)
        {    

             if(statusrepository is null) throw new ArgumentNullException(nameof(statusrepository));


            _statusrepository = statusrepository;
            _logger = logger;
        }

        public async Task<StatusResponse> getall()
        {
            StatusResponse response = new StatusResponse();


            try
            {
                var result = await _statusrepository.Getall();

                if (result.data != null)
                {
                    List<GetStatusDtos> status = ((List<Status>)result.data).Select(status => new GetStatusDtos
                    {
                        StatusName = status.StatusName,
                        CreateAt = status.CreateAt,
                        ID = status.StatusID,
                    }).ToList();

                    response.success = result.Sucess;
                    response.model = status;
                }
                else
                {
                    response.success = false;
                    response.model = null;
                }

            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"status  has not been found it on the register! {ex.Message} ";
               _logger.LogError(response.Menssaje, ex.ToString());
            }

            return response; 

        }

        public async Task<StatusResponse> GetById(int id)
        {
            StatusResponse response = new StatusResponse();

            try
            {
                var result = await _statusrepository.GetEntitiebyId(id);

                if (result.Sucess && result.data != null)
                {
                    Status status = (Status)result.data!;

                    GetStatusDtos statusDtos = new GetStatusDtos
                    {
                        ID = status.StatusID,
                        StatusName = status.StatusName,
                        CreateAt = status.CreateAt,
                    };

                    response.success = result.Sucess;
                    response.model = statusDtos;
                    response.Menssaje = "Status has been found successfully!";
                }
                else
                {
                    response.success = false;
                    response.Menssaje = "Status has not been found in the register!";
                }

            }
            catch (Exception ex)
            {

                response.success = false;
                response.Menssaje = $"Status has not been found it on the register! erro type {ex.Message} ";
               _logger.LogError(response.Menssaje, ex.ToString());

            }

            return response;

        }

        public async Task<StatusResponse> SaveAsync(SaveStatusDtos dto)
        {
           StatusResponse response = new StatusResponse();

            try
            {
                Status status = new Status();

                status.StatusName = dto.StatusName;
                status.CreateAt = dto.CreateAt;


                var result = await _statusrepository.Add(status);
                response.model = result;
                response.Menssaje = "Status has been added succefully! ";
            }
            catch (Exception ex)
            {

                response.success = false;
                response.Menssaje = $"status erro type {ex.Message} saving the status ";
               _logger.LogError(response.Menssaje, ex.ToString());
            }

           return response;
        }

        public async Task<StatusResponse> UpdateAsync(UpdateStatusDtos dto)
        {
            StatusResponse response = new StatusResponse();


            try
            {
                var result = await _statusrepository.GetEntitiebyId(dto.StatusID);

                if(result.Sucess)
                {
                    Status status = (Status)result.data!;

                    status.StatusName = dto.StatusName;
                    status.StatusID = dto.StatusID;
                    status.CreateAt = dto.CreateAt;
                  

                    var datos = await _statusrepository.Update(status);
                    response.model = datos;
                    response.Menssaje = "Status has been updated succefully! ";

                } else
                {
                    response.success = false;
                    response.Menssaje = "Status has not been found it on the register! ";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"status erro type {ex.Message} updating the status ";
               _logger.LogError(response.Menssaje, ex.ToString());

            }


            return response; 

        }
    }
}
