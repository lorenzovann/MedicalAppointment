

using Medical.Domain.Entities.Confi.Users;
using MedicalAppointment.Persistance.Interfaces;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using MedicalCoreAplications.cs.Contracts.Configurations;
using MedicalCoreAplications.cs.Dtos.Configuration.UserDtos.cs;
using MedicalCoreAplications.cs.Response.Configurations;
using Microsoft.Extensions.Logging;

namespace MedicalCoreAplications.cs.Services
{
    public class UserServices : IUserServices
    {

        private readonly UserInterfaces _userrepositorie;
        private readonly ILogger<UserServices> _logger; 


        public UserServices(UserInterfaces  userrepositorie, ILogger<UserServices> logger)
        {  
            if(userrepositorie is null) throw new ArgumentNullException(nameof(userrepositorie));


            _userrepositorie = userrepositorie;
            this._logger = logger;
        }

        public async Task<UserResponse> getall()
        {
            UserResponse response = new UserResponse();


            try
            {
                var result = await _userrepositorie.Getall();

                List<GetUsersDto> users = ((List<User>)result.data!)
                             .Select(users => new GetUsersDto()
                {
                    UserID = users.UserId,
                    FirstName = users.FirstName,
                    LastName = users.LastName,
                    Password = users.Password,
                    RoleId = users.RoleId,
                    CreatedAt = users.CreatedAt,
                    Email = users.Email

                }).ToList();

                response.model = users;


            }
            catch (Exception ex) 
            {
                response.success = false;
                response.Menssaje = $" Erro {ex.Message} try to list all of the users ";
               _logger.LogError(response.Menssaje, ex.ToString());
            }

            return response; 

        }

        public async Task<UserResponse> GetById(int id)
        {
            UserResponse response = new UserResponse();

            try
            {
                var result = await _userrepositorie.GetEntitiebyId(id); 

                if(result.Sucess)
                {
                    User user = (User)result.data!;

                    GetUsersDto dto = new GetUsersDto()
                    {
                         UserID = id, 
                         FirstName = user.FirstName,
                         LastName = user.LastName,
                         Password = user.Password,
                         Email = user.Email,
                         RoleId = user.RoleId,
                         CreatedAt = user.CreatedAt,
                         IsActive = user.IsActive,
                        
                    };


                    response.model = dto;
                    response.Menssaje = "Use id has been found it succefully!! ";
                } else
                {


                    response.success = false;
                    response.Menssaje = "User has not been found it on the register! ";
                }
    

            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $" Erro {ex.Message} trying to get the id user ";
               _logger.LogError(response.Menssaje, ex.ToString());

            }
            return response;


        }

        public async Task<UserResponse> SaveAsync(SaveUsersDtos dto)
        {
            UserResponse response = new UserResponse();


            try
            {
                User user = new User();
                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Password = dto.Password;
                user.Email = dto.Email;
                user.RoleId = dto.RoleId;
                

                 response.model = await _userrepositorie.Add(user);
                 response.Menssaje = " User has been saved succefully! ";
            }
            catch (Exception ex)
            {

                  response.success = false;
                  response.Menssaje = $" Erro {ex.Message} Saving the user! ";
                _logger.LogError(response.Menssaje, ex.ToString());
            }

            return response;
        }

        public async Task<UserResponse> UpdateAsync(UpdateUsersDto dto)
        {
            UserResponse response = new UserResponse();

            try
            {

                var result = await _userrepositorie.GetEntitiebyId(dto.UserID);

                if(!result.Sucess)
                {
                    response.success = false;
                    response.Menssaje = "User to update has not been found it on the register! ";
                    return response; 
                }

                User user = (User)result.data!;
                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Password = dto.Password;
                user.Email = dto.Email;
                user.RoleId = dto.RoleId;



                response.model = await _userrepositorie.Update(user);
                response.Menssaje = "User has been updated succefully! ";
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $" Erro {ex.Message} Updating    the user! ";
               _logger.LogError(response.Menssaje, ex.ToString());

            }


            return response;

        }
    }
}
