

using Medical.Domain.Entities.Confi.Systems;
using Medical.Domain.Entities.Confi.Users;
using Medical.Percistances.cs.Base;
using Medical.Percistances.cs.Context;
using MedicalAppointment.Domain.IBaseRepositorie;
using MedicalAppointment.Domain.Result;
using MedicalAppointment.Persistance.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Xml.XPath;

namespace MedicalAppointment.Persistance.Repositorie.Configuration
{
    public class UserRepositorie : BaseRepositorie<User>, UserInterfaces
    {

        private readonly MedicalContext _context;
        private readonly ILogger<UserRepositorie> _logger;

        public UserRepositorie(MedicalContext dbContext, ILogger<UserRepositorie> logger)
            : base(dbContext)
        {

            _context = dbContext;
            _logger = logger;
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
                result.data = await base.Add(entities);
                result.Message = " Usuario agragado Exitosamente! ";
               }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $" ERROR tipo {ex.Message} al tratar de agregar usuario! ";
                _logger.LogError(result.Message, ToString());

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
                _logger.LogError(result.Message, ToString());
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
         
                    result.data = await base.Delete(UserRemove);
                    result.Message = $"Usuario {entities.IDUser} eliminado exitosamente! ";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"ERROR tipo {ex.Message} tratando de eliminar este usuario! ";
            }

            return result;

        }

        public override async Task<OperationResult> Getall()
        {
            OperationResult result = new OperationResult();

            try
            {
                // consulta con base de datos 
                var usersWithRoles = await (from user in _context.Users
                                            join SystemRole in _context.Roles on user.RoleId equals SystemRole.RoleId
                                            select new
                                            {
                                                user.IDUser,
                                                user.FirstName,
                                                user.LastName,
                                                user.Email,
                                                user.Pasword,
                                                SystemRole.RoleName
                                            }).ToListAsync();

                result.data = usersWithRoles;
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo {ex.Message} tratando de obtener usuarios ";
                _logger.LogError(result.Message, ToString());

            }




            return result;



        }





        public override async Task<OperationResult> GetEntitiebyId(int id)
        {
            OperationResult result = new OperationResult();

            // Validación del ID
            if (id <= 0)
            {
                result.Sucess = false;
                result.Message = "ID no puede ser negativo ni igual a 0!";
                return result;
            }

            try
            {
                // Consulta para obtener el usuario y su rol
                var userWithRole = await (from user in _context.Users
                                          join role in _context.Roles
                                          on user.RoleId equals role.RoleId
                                          where user.IDUser == id
                                          select new
                                          {
                                              user.IDUser,
                                              user.FirstName,
                                              user.LastName,
                                              user.Email,
                                              user.Pasword,
                                              RoleName = role.RoleName  // Nombre del rol del usuario
                                          }).FirstOrDefaultAsync();

                // Verificación si el usuario fue encontrado
                if (userWithRole == null)
                {
                    result.Sucess = false;
                    result.Message = "Usuario no encontrado con el ID proporcionado.";
                    return result;
                }

                // Usuario encontrado
                result.data = userWithRole;
                result.Message = "Usuario encontrado exitosamente.";
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error al tratar de encontrar el ID: {ex.Message}";
                _logger.LogError(result.Message, ex);
            }

            return result;
        }


    }
}
