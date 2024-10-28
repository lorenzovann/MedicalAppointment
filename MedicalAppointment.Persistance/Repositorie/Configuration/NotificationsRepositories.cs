

using Medical.Domain.Entities.Confi.Systems;
using Medical.Percistances.cs.Base;
using Medical.Percistances.cs.Context;
using MedicalAppointment.Domain.Result;
using MedicalAppointment.Persistance.Interfaces.Configuration.SystemIntefaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Xml.XPath;

namespace MedicalAppointment.Persistance.Repositorie.Configuration
{
    public class NotificationsRepositories : BaseRepositorie<Notifications>, INotificationsinterfaces
    {

        private readonly MedicalContext _context; 
        private readonly ILogger<NotificationsRepositories> _logger;
        public NotificationsRepositories(MedicalContext context,
                           ILogger<NotificationsRepositories> _logger) : base(context)
        { 

            _context = context; 
            this._logger = _logger;
        }


        // implementaciones 
        public override async Task<OperationResult> Add(Notifications entities)
        {
            OperationResult result = new OperationResult();

        

            if (string.IsNullOrEmpty(entities.Message))
            {
                result.Sucess = false;
                result.Message = "No puedes dejar el campo vacío!";
                return result;
            }

            if (await base.Exist(N => N.NotificationId == entities.NotificationId && N.UserID == entities.UserID))
            {
                result.Sucess = false;
                result.Message = "La notificación ya existe!";
                return result;
            }

            try
            {
                await base.Add(entities);
                result.data = entities;
                result.Message = "Notificación agendada correctamente!";
            }
            catch (DbUpdateException ex)
            {
                // Captura y muestra el detalle de la inner exception
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                result.Sucess = false;
                result.Message = $"Error tipo: {innerExceptionMessage} agregando notificación!";
                _logger.LogError(innerExceptionMessage, ToString());
            }

            return result;
        }

        
       

        public override async Task<OperationResult> Update(Notifications entities)
        {

            OperationResult result = new OperationResult();


           if(entities.UserID <= 0)
            {
                result.Sucess = false;
                result.Message = "Error no puedes genera valores menores o iguales a 0";
                return result; 

            }

          

            if (string.IsNullOrEmpty(entities.Message))
            {
                result.Sucess = false;
                result.Message = " No puedes ingresar valores vacios! ";
                return result;
            }


            try
            {
                Notifications? notificationsUpdate = await _context.Notifications.FindAsync(entities.NotificationId);
                if (notificationsUpdate == null)
                {
                    result.Sucess = false;
                    result.Message = " Campo no puede estar vacio! ";
                    return result;
                }

                notificationsUpdate.NotificationId = entities.NotificationId;
                notificationsUpdate.UserID = entities.UserID;
                notificationsUpdate.Message = entities.Message;
                notificationsUpdate.SentAt = entities.SentAt;

                await base.Update(notificationsUpdate);
                result.data = notificationsUpdate;
                result.Message = " Notificacion modiificada! "; 

            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo: {ex.Message} Modificando las notificaciones! ";
               _logger.LogError(result.Message, ToString());
            }

            return result; 
            
        }

        public override async Task<OperationResult> Getall()
        {
            OperationResult result = new OperationResult();

            try
            {
                var Listar = await (from Notifications in _context.Notifications
                                    join Users in _context.Users on Notifications.UserID equals Users.UserId
                                    select new
                                    {
                                        NotificationID = Notifications.NotificationId,
                                        NotificationMessage = Notifications.Message,
                                        NotificationSentAt = Notifications.SentAt,
                                        UserID = Users.UserId,
                                        UserName = Users.FirstName,
                                        UserEmail = Users.Email,
                                        UserPassword = Users.Password
                                    }).ToListAsync();
                result.data = Listar;  

            }
            catch (Exception ex)
            {
                 result.Sucess = false;
                 result.Message = $"Error tipo: {ex.Message} listar las notificaciones! ";
                _logger.LogError(result.Message, ToString());
            }


            return result; 
        }

        public override async Task<OperationResult> GetEntitiebyId(int id)
        {
            OperationResult result = new OperationResult();

            // Validar si el ID es válido
            if (id <= 0)
            {
                result.Sucess = false;
                result.Message = "No puedes ingresar un ID menor o igual a 0!";
                return result;
            }

            try
            {
                var ValueFind = await (from Notifications in _context.Notifications
                                       join Users in _context.Users on Notifications.UserID equals Users.UserId
                                       where Notifications.NotificationId == id
                                       && Users.IsActive == true
                                       orderby Notifications descending 
                                       select new
                                       {
                                           NotificationID = Notifications.NotificationId,
                                           NotificationMessage = Notifications.Message,
                                           NotificationSentat = Notifications.SentAt, 
                                           UsersIDUser = Users.UserId,
                                           UserName = Users.FirstName,
                                           UserslastName = Users.LastName,
                                           UsersPassword = Users.Password,
                                           UserEmail = Users.Email
                                       }).FirstOrDefaultAsync(); 

                if(ValueFind == null)
                {
                    result.Sucess = false;
                    result.Message = "Notificacion no encontrada! ";
                    return result;

                }

                result.data = ValueFind;
                result.Message = " Lista de notificaciones ejecutadas exitosamente! ";
             
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error: {ex.Message} al listar las notificaciones!";
                _logger.LogError(result.Message, ToString());
            }
                
            return result;
        }





    }
}

