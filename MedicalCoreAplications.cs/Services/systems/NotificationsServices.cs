

using Medical.Domain.Entities.Confi.Systems;
using Medical.Domain.Entities.Confi.Users;
using MedicalAppointment.Persistance.Interfaces.Configuration.SystemIntefaces;
using MedicalAppointment.Persistance.Model.Systems;
using MedicalCoreAplications.cs.Contracts.Systems;
using MedicalCoreAplications.cs.Dtos.Systems.NotificationsDtos.cs;
using MedicalCoreAplications.cs.Response.Systems;
using Microsoft.Extensions.Logging;

namespace MedicalCoreAplications.cs.Services.systems
{
    public class NotificationsServices : INotificationServices
    {

        private readonly INotificationsRepository _notificationsRepository;
        private readonly ILogger<NotificationsServices> _logger;    

        public NotificationsServices(INotificationsRepository notificationsRepository, ILogger<NotificationsServices> logger)
        {  
            if(notificationsRepository is null) throw new ArgumentNullException(nameof(notificationsRepository));


            _notificationsRepository = notificationsRepository;
            this._logger = logger;
        }

        public async Task<NotificationsResponse> getall()
        {
           NotificationsResponse notificationsResponse = new NotificationsResponse();

            try
            {


                var result = await _notificationsRepository.Getall();

                // Verifica si `result.data` es una lista de `Notifications`
                if (result.data is List<Notifications> notifications)
                {
                    notificationsResponse.model = notifications
                        .Select(notification => new GetNotificationsDtos
                        {
                            NotificationId = notification.NotificationId,
                            UserID = notification.UserID,
                            Message = notification.Message,
                            SentAt = notification.SentAt,
                        }).ToList();

                    notificationsResponse.Menssaje = "Notifications have been found!";
                    notificationsResponse.success = true;

                }
                else
                {
                    notificationsResponse.Menssaje = "Notifications have not been found!";
                    notificationsResponse.success = false;
                }

            }
            catch (Exception ex)
            {

                notificationsResponse.success = false;
                notificationsResponse.Menssaje = $"Notifications has not been found it on the register! {ex.Message}  ";
               _logger.LogError(notificationsResponse.Menssaje, ex.ToString());

            }

            return notificationsResponse;
        }

        public async Task<NotificationsResponse> GetById(int id)
        {
            NotificationsResponse response = new NotificationsResponse();

            try
            {
                var result = await _notificationsRepository.GetEntitiebyId(id);

                if (result.Sucess)
                {
                    response.success = result.Sucess;
                    response.model = result.data;
                
                } else
                {
                    response.success = false;
                    response.Menssaje = "Notifications has not been found it! ";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = "Notifications has not been found it on the register! ";
               _logger.LogError(response.Menssaje, ex.ToString());
            }

            return response; 

        }

        public async Task<NotificationsResponse> SaveAsync(SaveNotificationsDto dto)
        {
             NotificationsResponse response = new NotificationsResponse();


            try
            {
                Notifications notifications = new Notifications();
                notifications.UserID = dto.UserID;
                notifications.Message = dto.Message;
                notifications.SentAt = dto.SentAt;


                var result = await _notificationsRepository.Add(notifications);
                response.model = result;
                response.Menssaje = "Notifications has been register in the system! ";

            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = "Notifications has not been found it on the register ";
                _logger.LogError(response.Menssaje, ex.ToString());
            }

            return response; 

        }

        public async Task<NotificationsResponse> UpdateAsync(UpdateNotificationsDtos dto)
        { 
            NotificationsResponse response = new NotificationsResponse();
            try
            {
                var result = await _notificationsRepository.GetEntitiebyId(dto.NotificationId);


                Notifications notifications = (Notifications)result.data!; 

                notifications.NotificationId = dto.NotificationId;
                notifications.Message = dto.Message;
                notifications.SentAt = dto.SentAt;
                notifications.UserID = dto.UserID;


                response.success = result.Sucess;
                response.model = await _notificationsRepository.Update(notifications);



            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Notifications has not been found it on the registe¨{ex.Message} ";
               _logger.LogError(response.Menssaje, ex.ToString());
            }
            return response; 

        }
    }
}
