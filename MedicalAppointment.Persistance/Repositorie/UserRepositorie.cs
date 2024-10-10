

using Medical.Domain.Entities.Confi.Users;
using Medical.Percistances.cs.Base;
using Medical.Percistances.cs.Context;
using MedicalAppointment.Domain.IBaseRepositorie;
using MedicalAppointment.Domain.Result;
using MedicalAppointment.Persistance.Interfaces.Configuration.UsersInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Xml.XPath;

namespace MedicalAppointment.Persistance.Repositorie
{
    public class UserRepositorie : BaseRepositorie<User>, UserInterfaces
    {

        private readonly Context _context;
        private readonly ILogger<UserRepositorie> _logger;

        public UserRepositorie(Context dbContext, ILogger<UserRepositorie> logger)
            : base(dbContext) {

            _context = dbContext;
            this._logger = logger;
        }

        public async override Task<OperationResult> Add(User entities)
        {
            OperationResult result = new OperationResult();

            // manejo de exepciones 

            if (entities.FirstName == null || entities.LastName == null || entities.Pasword == null || entities.Email == null)
            {
                result.Sucess = false;
                result.Message = "No puede dejar valores vacios! ";
                return result;
            }

            if (entities.IDUser <= 0 || entities.RoleId <= 0)
            {
                result.Sucess = false;
                result.Message = " id no puede ser negativo ni 0! ";
                return result;
            }

            if (await base.Exist(user => user.IDUser == entities.IDUser && user.RoleId == entities.RoleId))

            {
                result.Sucess = false;
                result.Message = "El usuario ya se encuentra registrado!";
                return result;
            }


            try
            {
                result = await base.Add(entities);
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $" ERROR tipo {ex.Message} ";  
              
            }
        
            return result; 
            
        }

        public async override Task<OperationResult> Update(User entities)
        {
            OperationResult result = new OperationResult();

            // Validación de los campos obligatorios
            if (entities.FirstName == null || entities.LastName == null || entities.Pasword == null || entities.Email == null)
            {
                result.Sucess = false;
                result.Message = "No puede dejar valores vacíos!";
                return result;
            }

            // Validación de los IDs
            if (entities.IDUser <= 0 || entities.RoleId <= 0)
            {
                result.Sucess = false;
                result.Message = "ID no puede ser negativo ni 0!";
                return result;
            }

            // Verificación si el usuario ya existe
            if (await base.Exist(user => user.IDUser == entities.IDUser && user.RoleId == entities.RoleId))
            {
                result.Sucess = false;
                result.Message = "El usuario ya está registrado!";
                return result;
            }

            try
            {
                // Usa "Users" en lugar de "User"
                User? userUpdate = await _context.Users.FindAsync(entities.IDUser);
                

                userUpdate.IDUser = entities.IDUser;  
                userUpdate.RoleId = entities.RoleId;
                userUpdate.FirstName = entities.FirstName; 
                userUpdate.LastName = entities.LastName;
                userUpdate.Pasword = entities.Pasword;
                userUpdate.Email = entities.Email;



                result.data = await base.Update(userUpdate);
                result.Message = " Usuario modificado Correctamente! ";

            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error al actualizar el usuario: {ex.Message}";
            }

            return result;
        }

        public override async Task<OperationResult> Delete(User entities)
        {    

             OperationResult result = new OperationResult();

            if (entities.FirstName == null || entities.LastName == null || entities.Pasword == null || entities.Email == null)
            {
                result.Sucess = false;
                result.Message = "No puede dejar valores vacíos!";
                return result;
            }

            // Validación de los IDs
            if (entities.IDUser <= 0 || entities.RoleId <= 0)
            {
                result.Sucess = false;
                result.Message = "ID a eliminar no puede ser negativo ni 0!";
                return result;
            }

            // Verificación si el usuario ya existe
            if (await base.Exist(user => user.IDUser == entities.IDUser && user.RoleId == entities.RoleId))
            {
                result.Sucess = false;
                result.Message = "El usuario ya está registrado!";
                return result;

            }
            try
            {
                User? UserRemove = await _context.Users.FindAsync(entities.IDUser);
                if (UserRemove != null)
                {
                  result.data =  await base.Delete(UserRemove);
                  result.Message = $"Usuario {entities.IDUser} eliminado exitosamente! "; 
                }
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"ERROR tipo {ex.Message} tratando de eliminar este usuario! ";
            }

            return result; 
         
        }

        public override Task<OperationResult> Getall()
        {
            return base.Getall();
        }

        public override Task<bool> Exist(Expression<Func<User, bool>> filter)
        {
            return base.Exist(filter);
        }

    

        public override Task<OperationResult> GetEntitiebyId(int id)
        {
            return base.GetEntitiebyId(id);
        }
    }
}
