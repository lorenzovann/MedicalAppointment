

using Azure;
using Medical.Domain.Entities.Confi.Systems;
using MedicalAppoiment.Aplication.cs.Contracts.systems;
using MedicalAppoiment.Aplication.cs.Dtos.Systems.NotificationsDtos.cs;
using MedicalAppoiment.Aplication.cs.Response.systems;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Net.WebSockets;

namespace MedicalAppoiment.Aplication.cs.Services.systems
{


    public class NotificationsServices : INotificationsServices
    {

        private readonly NotificationsRepositories _notificationsRepositories;
        private readonly ILogger<NotificationsServices> _logger;

        public NotificationsServices(NotificationsRepositories notificationsRepositories, ILogger<NotificationsServices> logger)
        {
            if (notificationsRepositories is null)
            {
                throw new ArgumentNullException(nameof(notificationsRepositories));

            }


            _notificationsRepositories = notificationsRepositories;
            _logger = logger;
        }

        public async Task<NotificationsResponses> getall()
        {
            NotificationsResponses response = new NotificationsResponses();


            try
            {


                var result = await _notificationsRepositories.Getall();

                List<GetNotificationsDto> notificationsDtos = ((List<Notifications>)result.data)
                    .Select(notification => new GetNotificationsDto
                    {
                        NotificationId = notification.NotificationId,
                        Message = notification.Message,
                        SentAt = notification.SentAt,
                        UserID = notification.UserID,
                    })
                    .ToList();

                response.model = notificationsDtos;
                response.Menssage = "List completly succefully! ";
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Menssage = $"Erro occurr {ex.Message}";
                _logger.LogError(response.Menssage, ex.ToString());
            }

            return response;
        }

        public async Task<NotificationsResponses> GetById(int id)
        {
            NotificationsResponses response = new NotificationsResponses();

            try
            {
                var result = await _notificationsRepositories.GetEntitiebyId(id);
                if (result != null)
                {


                    Notifications notifications = (Notifications)result.data;

                    GetNotificationsDto noti = new GetNotificationsDto()
                    {
                        NotificationId = notifications.NotificationId,
                        Message = notifications.Message,
                        SentAt = notifications.SentAt,
                        UserID = notifications.UserID,
                    };

                    response.model = noti;
                    response.Menssage = " List Ejecuted succefully! ";
                }
                else
                {
                    response.Success = false;
                    response.Menssage = "Notification not found on the register! ";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Erro occurr {ex.Message}";
                _logger.LogError(response.Menssage, ex.ToString());
            }
            return response;

        }

        public async Task<NotificationsResponses> SaveAsync(NotificationsSaveDto dto)
        {
            NotificationsResponses response = new NotificationsResponses();

            try
            {
                Notifications noti = new Notifications();
                noti.UserID = dto.UserID; 
                noti.SentAt = dto.SentAt;
                noti.Message = dto.Message;
                

                var result = await _notificationsRepositories.Add(noti);
                response.model = result;
                response.Menssage = "Notifications Register Succefully!";

            }
            catch (Exception ex)
            {
                 response.Success = false;
                 response.Menssage = $"Erro occurr {ex.Message}";
                _logger.LogError(response.Menssage, ex.ToString());
            }

            return response;


        }

        public async Task<NotificationsResponses> UpdateAsync(NotifiactionsUpdate dto)
        {
            NotificationsResponses response = new NotificationsResponses();


            try
            {
                var result = await _notificationsRepositories.GetEntitiebyId(dto.NotificationId);
                if(result != null)
                {
                    Notifications notifications = (Notifications)result.data;
                    notifications.NotificationId = dto.NotificationId;
                    notifications.UserID = dto.UserID; 
                    notifications.SentAt = dto.SentAt;
                    notifications.Message = dto.Message;


                    var datos = await _notificationsRepositories.Update(notifications);
                    response.model = datos;
                    response.Menssage = "Notifications Updated Succefully!"; 
                    
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Erro occurr {ex.Message} ";
               _logger.LogError(response.Menssage, ex.ToString());
            }

            return response;

        }
    }
}