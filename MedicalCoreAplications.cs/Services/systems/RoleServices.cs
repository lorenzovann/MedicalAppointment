using Medical.Domain.Entities.Confi.Systems;
using MedicalAppointment.Persistance.Interfaces.Configuration.SystemIntefaces;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using MedicalCoreAplications.cs.Contracts.Systems;
using MedicalCoreAplications.cs.Dtos.Systems.RolesDtos.cs;
using MedicalCoreAplications.cs.Response.Systems;
using Microsoft.Extensions.Logging;


namespace MedicalCoreAplications.cs.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly IRoleRepository _rolerepositorie;
        private readonly ILogger<RoleServices> _logger;
    

        public RoleServices(IRoleRepository  rolerepositorie, ILogger<RoleServices> logger)
        {
            if (rolerepositorie is null) throw new ArgumentNullException(nameof(rolerepositorie));

            _rolerepositorie = rolerepositorie;
            this._logger = logger;
        }

        public async Task<RoleResponse> getall()
        {
            RoleResponse response = new RoleResponse();

            try
            {

                var result = await _rolerepositorie.Getall(); 

                List<GetRolesDtos> roles = ((List<Role>)
                      result.data!).Select(role => new GetRolesDtos
                {
                          RoleID = role.RoleID, 
                          RoleName = role.RoleName,
                          CreatedAt = role.CreatedAt,
                          IsActive = role.IsActive,
                          UpdatedAt = role.UpdatedAt,

                }).ToList();    


                response.model = roles; 
              

            }
            catch (Exception ex)
            {

                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} to list all of the roles!";
                _logger.LogError(response.Menssaje, ex.ToString());
            }

            return response;
        }

        public async Task<RoleResponse> GetById(int id)
        {
            RoleResponse response = new RoleResponse();

            try
            {
                var result = await _rolerepositorie.GetEntitiebyId(id);

                if (result.Sucess)
                {

                    Role role = (Role)result.data!;


                    GetRolesDtos getRolesDtos = new GetRolesDtos()
                    {
                        RoleID = role.RoleID,
                        RoleName = role.RoleName,
                        CreatedAt = role.CreatedAt,
                        IsActive = role.IsActive,
                        UpdatedAt = role.UpdatedAt,

                    };

                    response.success = result.Sucess; 
                    response.model = getRolesDtos;
                    response.Menssaje = " Role found it succefully! ";
                }
                else
                {
                    response.success = false;
                    response.Menssaje = "Role has not been found it on the register! ";
                }

            }
            catch (Exception ex)
            {

                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} getting the role's id ";
                _logger.LogError(response.Menssaje, ex.ToString());
            }
            return response;
        }

        public async Task<RoleResponse> SaveAsync(SaveRolesDtos dto)
        {
            RoleResponse response = new RoleResponse();

            try
            {
                Role role = new Role();
                role.RoleName = dto.RoleName;
                role.CreatedAt = dto.CreatedAt;
                role.IsActive = true; 
                role.UpdatedAt = dto.UpdatedAt;



                var result = await _rolerepositorie.Add(role);
                response.model = result;
                response.Menssaje = "Role has been register succefully! ";

            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} saving the role! ";
                _logger.LogError(response.Menssaje, ex.ToString());
            }

            return response;
        }

        public async Task<RoleResponse> UpdateAsync(UpdateRolesDtos dto)
        {
            RoleResponse response = new RoleResponse();


            try
            {
                var result = await _rolerepositorie.GetEntitiebyId(dto.RoleID);

                if (result.Sucess)
                {
                    Role role = (Role)result.data!;

                    role.RoleID = dto.RoleID;
                    role.RoleName = dto.RoleName;
                    role.CreatedAt = dto.CreatedAt;
                    role.IsActive = dto.IsActive;


                    response.model = await _rolerepositorie.Update(role);
                    response.Menssaje = "Role updated succefully! ";

                }
                else
                {
                    response.success = false;
                    response.Menssaje = "Role has not been found it on the register! ";

                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} updating the role ";
                _logger.LogError(response.Menssaje, ex.ToString());

            }

            return response;

        }
    }
}