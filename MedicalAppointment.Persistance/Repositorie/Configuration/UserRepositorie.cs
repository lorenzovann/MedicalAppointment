

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
    public sealed class UserRepositorie : BaseRepositorie<User>, UserInterfaces
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

            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(entities.FirstName) ||
                string.IsNullOrWhiteSpace(entities.LastName) ||
                string.IsNullOrWhiteSpace(entities.Password) ||
                string.IsNullOrWhiteSpace(entities.Email))
            {
                result.Sucess = false;
                result.Message = "No puede dejar valores vacíos!";
                return result;
            }

            // Validar UserId y RoleId si no son generados automáticamente
          

            // Verificar si el usuario ya existe
            if (await base.Exist(user => user.UserId == entities.UserId && user.RoleId == entities.RoleId))
            {
                result.Sucess = false;
                result.Message = "El usuario ya se encuentra registrado!";
                return result;
            }

            try
            {
                await base.Add(entities); 
                result.data = entities;
                result.Message = "Usuario agregado correctamente!";

            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tratando de agregar usuario: {ex.Message}. Detalles internos: {ex.InnerException?.Message}";
                _logger.LogError(result.Message, ToString());
            }

            return result;
        }

        public async override Task<OperationResult> Update(User entities)
        {
            OperationResult result = new OperationResult();

            // Validación de los campos obligatorios
            if (entities.FirstName == null || entities.LastName == null || entities.Password == null || entities.Email == null)
            {
                result.Sucess = false;
                result.Message = "No puede dejar valores vacíos!";
                return result;
            }

            // Validación de los IDs
            if (entities.UserId <= 0 || entities.RoleId <= 0)
            {
                result.Sucess = false;
                result.Message = "ID no puede ser negativo ni 0!";
                return result;
            }

            // Verificación si el usuario ya existe
            if (await base.Exist(user => user.UserId == entities.UserId && user.RoleId == entities.RoleId))
            {
                result.Sucess = false;
                result.Message = "El usuario ya está registrado!";
                return result;
            }

            try
            {
                // Usa "Users" en lugar de "User"
                User? userUpdate = await _context.Users.FindAsync(entities.UserId);


                if(userUpdate == null)
                { 
                    result.Sucess = false;
                    result.Message = "Usuario modificar no encontrado! ";
                    return result;

                }

                userUpdate.UserId = entities.UserId;
                userUpdate.RoleId = entities.RoleId;
                userUpdate.FirstName = entities.FirstName;
                userUpdate.LastName = entities.LastName;
                userUpdate.Password = entities.Password;
                userUpdate.Email = entities.Email;
                userUpdate.UpdatedAt = entities.UpdatedAt;
                userUpdate.CreatedAt = entities.CreatedAt;
                userUpdate.IsActive = entities.IsActive; 


                await base.Update(userUpdate);
                result.data = userUpdate;
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

        public override async Task<OperationResult> Getall()
        {
            OperationResult result = new OperationResult();

            try
            {
                // consulta con base de datos 
                var usersWithRoles = await (from user in _context.Users
                                            join role in _context.Roles on user.RoleId equals role.RoleID
                                            select new
                                            {
                                                Usuarioid = user.UserId,
                                                NombreUsuario = user.FirstName,
                                                ApellidoUsuario = user.LastName,
                                                CorreoUsuario = user.Email,
                                                FechaCreacion = user.CreatedAt,
                                                Update = user.UpdatedAt,
                                                user.IsActive,
                                                PasswordUser = user.Password,
                                                RoleUser = role.RoleName,
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
                                          on user.RoleId equals role.RoleID
                                          where user.UserId == id
                                          && user.IsActive == true
                                          orderby user descending
                                          select new
                                          {
                                              user.UserId,
                                              user.FirstName,
                                              user.LastName,
                                              user.Email,
                                              user.Password,
                                              user.CreatedAt,
                                              user.UpdatedAt,
                                              RoleName = role.RoleName  // Nombre del rol del usuario
                                          }).FirstOrDefaultAsync();

                // Verificación si el usuario no fue encontrado
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
