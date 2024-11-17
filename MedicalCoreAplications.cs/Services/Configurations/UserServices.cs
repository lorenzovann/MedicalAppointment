using Medical.Domain.Entities.Confi.Users;
using MedicalAppointment.Persistance.Interfaces;
using MedicalAppointment.Persistance.Interfaces.Configurations;
using MedicalAppointment.Persistance.Model;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using MedicalCoreAplications.cs.Contracts.Configurations;
using MedicalCoreAplications.cs.Dtos.Configurations.UserDtos.cs;
using MedicalCoreAplications.cs.Dtos.Systems.StatusDtos.cs;
using MedicalCoreAplications.cs.Response.Configurations;
using Microsoft.Extensions.Logging;


namespace MedicalCoreAplications.cs.Services.Configurations
{
    public class UserServices : IUserServices
    {

        private readonly IUserRepository _userrepository;
        private readonly ILogger<UserServices> _logger; 

        public UserServices(IUserRepository userservices, ILogger<UserServices> logger)
        {   
            if(userservices is null) throw new ArgumentNullException(nameof(userservices));



            _userrepository = userservices;
            _logger = logger;
        }

        public async  Task<UserResponse> getall()
        {
           UserResponse response = new UserResponse();


            try
            {
                var result = await _userrepository.Getall(); 


                    
               
                if(result.Sucess)
                {
                   response.success = result.Sucess;
                   response.model = result.data; 

                } else
                {
                    response.success = false;
                    response.Menssaje = "List not found it on the register! ";

                }

            }
            catch (Exception ex)
            {

                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} list all of the users  ";
               _logger.LogError(response.Menssaje, ex.ToString());

            }




            return response; 

        }

        public async Task<UserResponse> GetById(int id)
        {
            UserResponse response = new UserResponse();

            try
            {
                var result = await _userrepository.GetEntitiebyId(id);    

                if(result.Sucess)
                {
                  
                    response.success = result.Sucess; 
                    response.model = result.data;

                    response.Menssaje = "User has been found it succefully! ";
                

                } else
                {
                    response.success = false;
                    response.Menssaje = "User has not been found it on the register! ";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} getting the user's id ";
               _logger.LogError(response.Menssaje, ex.ToString());

            }
             
            return response; 

        }

        public async Task<UserResponse> SaveAsync(SaveUserDtos dto)
        {
            UserResponse response = new UserResponse();

            try
            {
                User user = new User();
                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Email = dto.Email;
                user.Password = dto.Password;
                user.CreatedAt = dto.CreatedAt;
                user.UpdatedAt = dto.UpdatedAt;
                user.RoleId = dto.RoleId;
                user.IsActive = true;


                var result = await _userrepository.Add(user);
                response.success = result.Sucess; 
                response.model = result.data;
                response.Menssaje = "User has been register succefuly! ";

            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} saving the user's id ";
               _logger.LogError(response.Menssaje, ex.ToString());

            }

            return response; 

        }

        public async Task<UserResponse> UpdateAsync(UpdateUserDtos dto)
        {
            UserResponse response = new UserResponse();

            try
            {
                // Obténgo el usuario desde el repositorio
                var result = await _userrepository.GetEntitiebyId(dto.UserId);

                    User user = (User)result.data!; 
                    user.UserId = dto.UserId;
                    user.FirstName = dto.FirstName;
                    user.LastName = dto.LastName;
                    user.Email = dto.Email;
                    user.Password = dto.Password;
                    user.CreatedAt = dto.CreatedAt;
                    user.UpdatedAt = dto.UpdatedAt;
                    user.IsActive = dto.IsActive;
                    user.RoleId = dto.RoleId;


                    response.model = await _userrepository.Update(user);
                    response.Menssaje = "User has been updated succefully! ";
                   
               
               
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Error type: {ex.Message} while saving the user's ID.";
                _logger.LogError(response.Menssaje, ex);
            }

            return response;
        }

    }
}
