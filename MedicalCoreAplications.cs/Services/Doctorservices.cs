using Medical.Domain.Entities.Confi.Users;
using MedicalAppointment.Persistance.Interfaces.Configuration.UsersInterfaces;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using MedicalCoreAplications.cs.Contracts.Configurations;
using MedicalCoreAplications.cs.Dtos.Configuration.DoctorDtos.cs;
using MedicalCoreAplications.cs.Response.Configurations;
using Microsoft.Extensions.Logging;

namespace MedicalCoreAplications.cs.Services
{
    public class Doctorservices : IDoctorServices
    {

        private readonly IDoctorInterfaces doctorRepositorie;
        private readonly ILogger<Doctorservices> _logger;

        public Doctorservices(IDoctorInterfaces doctorRepositorie, ILogger<Doctorservices> logger)
        { 

            if(doctorRepositorie is null) throw new ArgumentNullException(nameof(doctorRepositorie));

            this.doctorRepositorie = doctorRepositorie;
            this._logger = logger;
        }

        public async Task<DoctorResponse> getall()
        {
            DoctorResponse response = new DoctorResponse();

            try
            {
                var result = await doctorRepositorie.Getall();

                List<GetDoctorDtos> doctor = ((List<Doctor>)result.data!) // lista de doctores
                    .Select(doctor => new GetDoctorDtos
                {
                         DoctorID  = doctor.DoctorID,
                         NameDoctor = doctor.NameDoctor,
                         ClinicAddress = doctor.ClinicAddress,
                         SpecialtyID = doctor.SpecialtyID,
                         CreatedAt = doctor.CreatedAt,
                         IsActive = doctor.IsActive,
                         LicenseNumber = doctor.LicenseNumber,
                         PhoneNumber = doctor.PhoneNumber,
                         UpdateAt = doctor.UpdatedAt,
                         
                }).ToList();

                response.model = doctor; // le paso el resultado a mi modelo 

            }
            catch (Exception ex)
            {

                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} try to list all of the doctors! ";
                _logger.LogError(response.Menssaje, ex.ToString());
              
            }

            return response; 

        }

        public async Task<DoctorResponse> GetById(int id)
        {
            DoctorResponse response = new DoctorResponse();


            try
            {
                var result = await doctorRepositorie.GetEntitiebyId(id);
                
                if(result.Sucess)
                {
                    Doctor doctor = (Doctor)result.data!;

                    GetDoctorDtos dto = new GetDoctorDtos()
                    {
                       DoctorID = id,
                       NameDoctor = doctor.NameDoctor,
                       ClinicAddress = doctor.ClinicAddress,
                       SpecialtyID = doctor.SpecialtyID,
                       CreatedAt = doctor.CreatedAt,
                       IsActive = doctor.IsActive,
                       LicenseNumber = doctor.LicenseNumber,
                       PhoneNumber = doctor.PhoneNumber,
                       UpdateAt = doctor.UpdatedAt,
                       
                    };

                    response.model = dto;
                }
            }
            catch (Exception ex) {  

                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} getting the Doctor id! ";
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
                doctor.SpecialtyID = dto.SpecialtyID;
                doctor.ClinicAddress = dto.ClinicAddress;
                doctor.LicenseNumber = dto.LicenseNumber;
                doctor.PhoneNumber = dto.PhoneNumber;


                response.model = await doctorRepositorie.Add(doctor);
                response.Menssaje = "Doctor saved succefully! ";
            }
            catch (Exception ex) { 
                
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} saving the Doctor id! ";
               _logger.LogError(response.Menssaje, ex.ToString());

            } 

            return response; 
        }

        public async  Task<DoctorResponse> UpdateAsync(UpdateDoctorDtos dto)
        {
            DoctorResponse response = new DoctorResponse();

            try
            {

                var result = await doctorRepositorie.GetEntitiebyId(dto.DoctorID); 

                if(result.Sucess)
                {
                    Doctor doctor = (Doctor)result.data!; 
                    doctor.DoctorID = dto.DoctorID;
                    doctor.NameDoctor = dto.NameDoctor;
                    doctor.SpecialtyID = dto.SpecialtyID;
                    doctor.LicenseNumber = dto.LicenseNumber;
                    doctor.PhoneNumber = dto.PhoneNumber;
                    doctor.ClinicAddress = dto.ClinicAddress;
                    doctor.IsActive = dto.IsActive;


                    response.model = await doctorRepositorie.Update(doctor);
                    response.Menssaje = "Doctor has been updated succefully "; 
         
                    
                } else
                {
                    response.success = false;
                    response.Menssaje = "Doctor has not been succefully found it! ";
                }

            }
            catch (Exception ex)
            {

                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} updating the doctor!  ";
               _logger.LogError(response.Menssaje, ex.ToString());
            }

            return response;

        }
    }
}
