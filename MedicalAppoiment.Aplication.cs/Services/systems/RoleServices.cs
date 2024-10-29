

using Azure;
using Medical.Domain.Entities.Confi.Systems;
using MedicalAppoiment.Aplication.cs.Contracts.configurations;
using MedicalAppoiment.Aplication.cs.Contracts.systems;
using MedicalAppoiment.Aplication.cs.Dtos.Cofiguration.Users;
using MedicalAppoiment.Aplication.cs.Dtos.Systems.RoleDtos.cs;
using MedicalAppoiment.Aplication.cs.Response.systems;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using Microsoft.Extensions.Logging;

namespace MedicalAppoiment.Aplication.cs.Services.systems
{
    public class RoleServices : IRoleServices
    {

        private readonly RoleRepositorie _rolerepositorie;
        private readonly ILogger<RoleServices> _logger;

        public RoleServices(RoleRepositorie rolerepositorie, ILogger<RoleServices> logger)
        {
            if (rolerepositorie == null) throw new ArgumentNullException(nameof(rolerepositorie));

            _rolerepositorie = rolerepositorie;
            this._logger = logger;
        }

        public async Task<RoleResponses> getall()
        {
            RoleResponses response = new RoleResponses();


            try
            {
                var result = await _rolerepositorie.Getall();
                
                List<getRolesDto> roles = ((List<Role>)result.data).Select(role => new getRolesDto
                {
                    RoleID = role.RoleID,
                    RoleName = role.RoleName,
                    CreatedAt = role.CreatedAt,
                    UpdatedAt = role.UpdatedAt,
                    IsActive = role.IsActive,
                    
                }).ToList();


                response.model = roles;
                response.Menssage = "Roles has been List Succefully!";
             
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Menssage=$"Erro {ex.Message} triying to List all of the regiter! ";
               _logger.LogError(response.Menssage, ex.ToString());
            }
            return response;

        }

        public async Task<RoleResponses> GetById(int id)
        {
            RoleResponses response = new RoleResponses();

            try
            {
                var result = await _rolerepositorie.GetEntitiebyId(id); 
                if (result.data != null)
                {
                    Role role = (Role)result.data;

                   
                    getRolesDto role1 = new getRolesDto()
                    {
                        RoleID = role.RoleID,
                        RoleName = role.RoleName,
                        IsActive=role.IsActive,
                        CreatedAt = role.CreatedAt,
                        UpdatedAt = role.UpdatedAt
                    };
                    
                    response.model = role1;
                    response.Menssage = "Role found successfully!";
                }
                else
                {
                    response.Success = false;
                    response.Menssage = "Role not found!";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Error {ex.Message} while trying to get the entity!";
                _logger.LogError(response.Menssage, ex.ToString());
            }

            return response;
        }



        public async Task<RoleResponses> SaveAsync(SaveRolesDtos dto)
        {
            RoleResponses response = new RoleResponses();

            try
            {
                Role role = new Role();
                role.RoleName = dto.RoleName;
                role.CreatedAt = dto.CreatedAt;
                role.UpdatedAt = dto.UpdatedAt;

                var datos = await _rolerepositorie.Add(role); 
                response.model = datos;
                response.Menssage = "Role added succefully!";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Error {ex.Message} while trying to Save the entity! ";
               _logger.LogError(response.Menssage, ex.ToString());
            }

            return response;
        }

        public async Task<RoleResponses> UpdateAsync(RoleUpdateDtos dto)
        {
            RoleResponses response = new RoleResponses();

            try
            {
                var datos = await _rolerepositorie.GetEntitiebyId(dto.RoleID);
                if(datos == null)
                {
                    response.Success = false;
                    response.Menssage = "Entitie not found on the register!";
                    return response;
                }


                Role role = (Role)datos.data;
                role.RoleID = dto.RoleID;
                role.RoleName = dto.RoleName;   
                role.CreatedAt = dto.CreatedAt; 
                role.UpdatedAt = dto.UpdatedAt;

                var result = await _rolerepositorie.Update(role);  
                response.model= result;
                response.Menssage = "Role Updated Succefully! "; 
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Error {ex.Message} while trying to update the entity! ";
               _logger.LogError(response.Menssage, ex.ToString());
            }

            return response;
        }
    }
}
