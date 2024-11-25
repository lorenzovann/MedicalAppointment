using Medical.Domain.Entities.Confi.Users;
using MedicalAppointment.Persistance.Interfaces.Configurations;
using MedicalCoreAplications.cs.Contracts.Configurations;
using MedicalCoreAplications.cs.Dtos.Configurations.DoctorDtos;
using MedicalCoreAplications.cs.Response.Configurations;
using Microsoft.Extensions.Logging;


namespace MedicalCoreAplications.cs.Services.Configurations
{
    public class DoctorServices : IDoctorServices
    {
        private readonly IDoctorRepository _doctorrepository; 
        private readonly ILogger<DoctorServices> _logger;

        public DoctorServices(IDoctorRepository doctorrepository,
                  ILogger<DoctorServices> logger)
        {  
             if(doctorrepository is null) throw new ArgumentNullException(nameof(doctorrepository));


            _doctorrepository = doctorrepository;
            _logger = logger;
        }

        public async Task<DoctorResponse> getall()
        {
           DoctorResponse response = new DoctorResponse();


            try
            {
                
                var result = await _doctorrepository.Getall();

                if(!result.Sucess)
                {
                    response.model = result.data;
                    response.success = result.Sucess; 
                    return response;    

                }

                response.model = result.data;
                response.Menssaje = "Doctor List succefully! ";

      
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} getting all of the doctors ";
               _logger.LogError(response.Menssaje, ex.ToString());

            }

            return response;

        }

        public async Task<DoctorResponse> GetById(int id)
        {
           DoctorResponse response = new DoctorResponse();

            try
            {
                var result = await _doctorrepository.GetEntitiebyId(id);

                if(result.Sucess)
                {
                    response.model = result.data; 
                    response.success = result.Sucess;  
                    response.Menssaje = "Doctor has been found it succefully! ";
                }
                else
                {
                    response.success = false;
                    response.Menssaje = "Doctor has not been found it on the register! ";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} getting doctor's id ";
               _logger.LogError(response.Menssaje, ex.ToString());

            }
            return response; 

        }

        public async Task<DoctorResponse> SaveAsync(SaveDoctorDtos dto)
        {
            DoctorResponse response = new DoctorResponse();


            try
            {
                Doctor doctor = new Doctor();
                doctor.NameDoctor = dto.NameDoctor;
                doctor.YearsOfExperience = dto.YearsOfExperience;
                doctor.SpecialtyID = dto.SpecialtyID;
                doctor.ClinicAddress = dto.ClinicAddress;
                doctor.Education = dto.Education;
                doctor.LicenseNumber = dto.LicenseNumber;
                doctor.PhoneNumber = dto.PhoneNumber;
                doctor.CreatedAt = dto.CreatedAt;
                doctor.UpdatedAt = dto.UpdatedAt;


                var result = await _doctorrepository.Add(doctor);
                response.model = result;
                response.Menssaje = "Doctor has been added to the register! ";
     
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} Saving the doctor ";
               _logger.LogError(response.Menssaje, ex.ToString());

            }


            return response;
        }

        public async Task<DoctorResponse> UpdateAsync(UpdateDoctorDtos dto)
        {
            DoctorResponse response = new DoctorResponse();

            try
            {
                var result = await _doctorrepository.GetEntitiebyId(dto.DoctorID);

                Doctor Doctor = (Doctor)result.data!; 
                Doctor.DoctorID = dto.DoctorID;
                Doctor.NameDoctor= dto.NameDoctor;
                Doctor.ClinicAddress = dto.ClinicAddress;
                Doctor.LicenseNumber = dto.LicenseNumber;
                Doctor.PhoneNumber = dto.PhoneNumber;
                Doctor.CreatedAt = dto.CreatedAt;
                Doctor.IsActive = dto.IsActive;
                Doctor.UpdatedAt = dto.UpdatedAt;
                Doctor.SpecialtyID = dto.SpecialtyID;
                Doctor.Education = dto.Education;
                Doctor.YearsOfExperience = dto.YearsOfExperience;
               
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} Updating the doctor ";
               _logger.LogError(response.Menssaje, ex.ToString());

            }
            return response;

        }
    }
}
