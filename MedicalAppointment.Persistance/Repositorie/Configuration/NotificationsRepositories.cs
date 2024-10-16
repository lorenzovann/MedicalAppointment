

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
        private readonly Logger<NotificationsRepositories> _logger;
        public NotificationsRepositories(MedicalContext context,
                           Logger<NotificationsRepositories> _logger) : base(context)
        { 

            _context = context; 
            this._logger = _logger;
        }


        // implementaciones 
        public override async Task<OperationResult> Add(Notifications entities)
        {
            OperationResult result = new OperationResult();


            if (entities.NotificationId <= 0 && entities.UserID <= 0)
            {
                result.Sucess = false;
                result.Message = " No puedes generar id menor o igual a 0! ";
                return result; 

            }


            if (string.IsNullOrEmpty(entities.Message))
            {
                result.Sucess = false; 
                result.Message = " No puedes dejar  el campos vasios! ";
                return result;

            }


            if (entities.SentAt < DateTime.UtcNow)
            {
                result.Sucess = false;
                result.Message = " No puedes ingresar valores antes de fecha! ";
                return result;
            }


            if (await base.Exist(N => N.NotificationId == entities.NotificationId && N.UserID == entities.UserID))
            {
                result.Sucess = false;
                result.Message = " La notificacion ya existe! ";
                return result;
            }
           

            try
            {
                 
                 result.data = await base.Add(entities);
                 result.Message = "Notificacion agendada correctamente! ";

            }
            catch (Exception ex)
            {
                result.Sucess = false; 
                result.Message = $"Error tipo: {ex.Message} agregando notificacion! ";
               _logger.LogError(result.Message, ToString()); 

            }

            return result; 

        }

        public override async Task<OperationResult> Delete(Notifications entities)
        {
           
            OperationResult result = new OperationResult();


            if (entities.NotificationId <= 0 && entities.UserID <= 0)
            {
                result.Sucess = false;
                result.Message = " No puedes generar id menor o igual a 0! ";
                return result;

            }


            if (string.IsNullOrEmpty(entities.Message))
            {
                result.Sucess = false;
                result.Message = " No puedes dejar  el campos vasios! ";
                return result;

            }


            if (entities.SentAt < DateTime.UtcNow)
            {
                result.Sucess = false;
                result.Message = " No puedes ingresar valores antes de fecha! ";
                return result;
            }

            try
            {
                Notifications? notifications =  await _context.Notifications.FindAsync(entities.NotificationId);

                if (notifications == null)
                {
                    result.Sucess = false;
                    result.Message = "No puedes dejar valores vacios! ";
                    return result;
                }

                result.data = await base.Delete(entities);
                result.Message = $"Notificacion: {entities.NotificationId} eliminada con exito"; 


            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo: {ex.Message} eliminando notificacion! ";
                _logger.LogError(result.Message, ToString());
            } 


            return result; 


        }

        public override async Task<OperationResult> Update(Notifications entities)
        {

            OperationResult result = new OperationResult();


            if (entities.NotificationId <= 0 || entities.UserID <= 0)
            {
                result.Sucess = false;
                result.Message = "Error no puedes genera valores menores o iguales a 0";
                return result; 

            }

            if (entities.SentAt < DateTime.UtcNow)
            {
                result.Sucess = false;
                result.Message = " No puedes ingresar valores antes de fecha! ";
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

                result.data = await base.Update(notificationsUpdate);
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
                                    join Users in _context.Users on Notifications.UserID equals Users.IDUser
                                    select new
                                    {
                                        NotificationID = Notifications.NotificationId,
                                        NotificationMessage = Notifications.Message,
                                        NotificationSentAt = Notifications.SentAt,
                                        UserID = Users.IDUser,
                                        UserName = Users.FirstName,
                                        UserEmail = Users.Email,
                                        UserPassword = Users.Pasword
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
                                       join Users in _context.Users on Notifications.UserID equals Users.IDUser
                                       where Notifications.NotificationId == id
                                       && Users.IsActive == true
                                       orderby Notifications descending 
                                       select new
                                       {
                                           NotificationID = Notifications.NotificationId,
                                           NotificationMessage = Notifications.Message,
                                           NotificationSentat = Notifications.SentAt, 
                                           UsersIDUser = Users.IDUser,  
                                           UserName = Users.FirstName,
                                           UserslastName = Users.LastName,
                                           UsersPassword = Users.Pasword,
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

