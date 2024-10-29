using Medical.Domain.Entities.Confi.Users;
using MedicalAppoiment.Aplication.cs.Base;
using MedicalAppoiment.Aplication.cs.Contracts.configurations;
using MedicalAppoiment.Aplication.cs.Dtos.Cofiguration.Users;
using MedicalAppoiment.Aplication.cs.Response.Users;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppoiment.Aplication.cs.Services.Configuration.Users
{
    public class UsersServices : IUserService
    {   

        private readonly UserRepositorie _userreitorie;
        private readonly ILogger<UserRepositorie> _logger;

        public UsersServices(UserRepositorie userrepositorie,
                      ILogger<UserRepositorie> logger)
        { 

          if(userrepositorie is null)
            {
                throw new ArgumentNullException(nameof(userrepositorie));
            } 

         _userreitorie = userrepositorie;
          this._logger = logger;
        }

        public async Task<UserResponse> getall()
        {
            UserResponse response = new UserResponse();

            try
            {
                var result = await _userreitorie.Getall();

                List<GetUserDto> users = ((List<User>)result.data)
                                                         .Select(users => new GetUserDto()
                                                         {
                                                             UserID = users.UserId,
                                                             FirstName = users.FirstName,
                                                             LastName = users.LastName,
                                                             Password = users.Password,
                                                             Email = users.Email,
                                                             RoleId = users.RoleId,

                                                         }).ToList();

                response.Success = result.Sucess;
                response.Model = users;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Error tipo {ex.Message} listando usuarios!";
                _logger.LogError(response.Menssage, ex);
            }

            return response;
        }


        public async Task<UserResponse> GetById(int id)
        {
             UserResponse reponse = new UserResponse();

            try
            {
                var result = await _userreitorie.GetEntitiebyId(id);
                 
                User user = (User)result.data; 

                GetUserDto dto = new GetUserDto()
                {

                    UserID = user.UserId,
                    FirstName = user.FirstName,
                    LastName =user.LastName,
                    RoleId = user.RoleId,
                    Password = user.Password,
                    Email = user.Email,
                        
                };

                reponse.Success = result.Sucess;
                reponse.Model = dto;



            }
            catch (Exception ex)
            {
                reponse.Success = false;
                reponse.Menssage = $"Erro tipo de {ex.Message}";
               _logger.LogError(reponse.Menssage, ex);

            }

            return reponse;
        }

        public async Task<UserResponse> SaveAsync(UserSaveDto dto)
        {
           UserResponse response = new UserResponse();

            try
            {
                User user = new User();

                user.UserId = dto.UserID;
                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.RoleId = dto.RoleId;
                user.Password = dto.Password;
                user.Email = dto.Email;


                response.Model = await _userreitorie.Add(user);
                response.Menssage = " User register Succefully! ";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"error {ex.Message} al agendar user ";
               _logger.LogError(response.Menssage, ex.ToString());
            }

            return response;

        }

        public async Task<UserResponse> UpdateAsync(UserUpdateDto dto)
        {
            UserResponse response = new UserResponse();


            try
            {
                var result = await _userreitorie.GetEntitiebyId(dto.UserID);


                User userupdate = (User)result.data;
                userupdate.UserId = dto.UserID;
                userupdate.FirstName = dto.FirstName;
                userupdate.LastName = dto.LastName;
                userupdate.Password = dto.Password;
                userupdate.Email = dto.Email;
                userupdate.RoleId = dto.RoleId;

  
                response.Model = await _userreitorie.Update(userupdate);
                response.Menssage = " User updating Succefully! ";
            }
            catch (Exception ex)
            {
               response.Success= false;
               response.Menssage = $"erro {ex.Message} tratando de encontrar registro!";
               _logger.LogError(response.Menssage, ex.ToString());
            }

            return response;
        }
    }
}
