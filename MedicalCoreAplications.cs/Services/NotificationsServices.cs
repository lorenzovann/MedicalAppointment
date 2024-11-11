

using Medical.Domain.Entities.Confi.Systems;
using MedicalAppointment.Persistance.Interfaces.Configuration.SystemIntefaces;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using MedicalCoreAplications.cs.Contracts.systems;
using MedicalCoreAplications.cs.Dtos.Systems.NotificationsDtos.cs;
using MedicalCoreAplications.cs.Response.Systems;
using Microsoft.Extensions.Logging;

namespace MedicalCoreAplications.cs.Services
{
    public class NotificationsServices : INotificationsServices
    {    

        private readonly INotificationsinterfaces _notificationsRepositories;
        private readonly ILogger<NotificationsServices> _logger;    

        public NotificationsServices(INotificationsinterfaces notificationsRepositories, 
                    ILogger<NotificationsServices> logger)
        {  

            if(notificationsRepositories is null) throw new ArgumentNullException(nameof(notificationsRepositories));   


            _notificationsRepositories = notificationsRepositories;
            this._logger = logger;
        }

        public async Task<NotificationsResponse> getall()
        {
            NotificationsResponse response = new NotificationsResponse();

            try
            {
                var result = await _notificationsRepositories.Getall();

                List<GetNotifications> notifications = ((List<Notifications>)result.data!)
                    .Select(notificatiosn =>
                    new GetNotifications
                    {
                        NotificationId = notificatiosn.NotificationId,  
                        Message = notificatiosn.Message,    
                        SentAt = notificatiosn.SentAt,
                        UserID = notificatiosn.UserID,  

                    }).ToList();

                response.model = notifications;

            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} try to list all of the Notifications ";
               _logger.LogError(response.Menssaje, ex.ToString());

            }


            return response; 

        }

        public  async Task<NotificationsResponse> GetById(int id)
        {
            NotificationsResponse response = new NotificationsResponse();

            try
            {
                var result = await _notificationsRepositories.GetEntitiebyId(id);
                
                if(result.Sucess)
                {
                    Notifications notifications = (Notifications)result.data!;

                    GetNotifications getNotifications = new GetNotifications()
                    {
                        NotificationId=id,
                        Message = notifications.Message, 
                        SentAt = notifications.SentAt,
                        UserID = notifications.UserID,
                    };


                    response.model = getNotifications;
                    response.Menssaje = "Notifications has been found it succefully! ";
                } else
                {
                    response.success = false;
                    response.Menssaje = "Notifications has not been found it on the register! ";
                }
            }
            catch (Exception ex)
            {

                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} getting the notifications id ";
               _logger.LogError(response.Menssaje, ex.ToString());
            }

            return response; 

        }

        public async Task<NotificationsResponse> SaveAsync(SaveNotifications dto)
        {
            NotificationsResponse response = new NotificationsResponse();

            try
            {
                Notifications noti = new Notifications();
                noti.Message = dto.Message; 
                noti.UserID = dto.UserID; 

                var result = await _notificationsRepositories.Add(noti);
                response.model = result;
                response.Menssaje = "Notifications has been registers sucefully! ";
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} saving the nofication!";
               _logger.LogError(response.Menssaje, ex.ToString());

            }

            return response; 

        }

        public async Task<NotificationsResponse> UpdateAsync(UpdateNotifications dto)
        {
            NotificationsResponse response = new NotificationsResponse();

            try
            {
                var rs = await _notificationsRepositories.GetEntitiebyId(dto.NotificationId);

                if(rs.Sucess)
                {
                    Notifications noti = (Notifications)rs.data!; 

                    noti.NotificationId = dto.NotificationId;
                    noti.Message = dto.Message; 
                    noti.UserID = dto.UserID;


                    response.model = await _notificationsRepositories.Update(noti);
                    response.Menssaje = " notifications has been succefully updated! ";
                } else
                {
                    response.success = false;
                    response.Menssaje = "notifications has not been found it on the register! ";
                }
            }
            catch (Exception ex)
            {

                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} updating the nofication!";
               _logger.LogError(response.Menssaje, ex.ToString());

            }
            return response; 
        }
    }
}
