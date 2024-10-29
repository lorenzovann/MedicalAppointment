

using Medical.Domain.Entities.Confi.Users;
using MedicalAppoiment.Aplication.cs.Contracts.configurations;
using MedicalAppoiment.Aplication.cs.Dtos.Cofiguration.Doctors;
using MedicalAppoiment.Aplication.cs.Dtos.Cofiguration.Users;
using MedicalAppoiment.Aplication.cs.Response.Users;
using MedicalAppointment.Domain.Result;
using MedicalAppointment.Persistance.Interfaces.Configuration.UsersInterfaces;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace MedicalAppoiment.Aplication.cs.Services.Configuration.Users
{
    public class DoctorServices : IDoctorServices
    { 
        private readonly DoctorRepositorie _doctorRepositorie;
        private readonly ILogger<DoctorServices> _logger;

        public DoctorServices(DoctorRepositorie doctorrepositorie,
             ILogger<DoctorServices> _logger)

        {

            if( _doctorRepositorie == null )
            {
                throw new ArgumentNullException(nameof(_doctorRepositorie ));
            }

            _doctorRepositorie = doctorrepositorie;
            this._logger = _logger;

        }

        public async Task<DoctorResponse> getall()
        {
            DoctorResponse response = new DoctorResponse();

            try
            {
              var datos = await _doctorRepositorie.Getall();
                List<GetDoctorDto> doctor = ((List<Doctor>)datos.data)
                                        .Select(doctor => new GetDoctorDto
              {
              DoctorID = doctor.DoctorID,
              NameDoctor = doctor.NameDoctor,
              SpecialtyID = doctor.SpecialtyID,
              LicenseNumber = doctor.LicenseNumber,
              YearsOfExperience = doctor.YearsOfExperience,
              Education = doctor.Education,
              Bio = doctor.Bio,
              ConsultationFee = doctor.ConsultationFee,
              ClinicAddress = doctor.ClinicAddress,
              AvailabilityModeId = doctor.AvailabilityModeId,
              LicenseExpirationDate = doctor.LicenseExpirationDate,
             }).ToList();

                response.Success = datos.Sucess;
                response.model = doctor;


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Error tipo {ex.Message} tratando de listar registros";
            }


            return response;


        }

        public async Task<DoctorResponse> GetById(int id)
        {
            DoctorResponse response = new DoctorResponse();

            try
            {
                var result = await _doctorRepositorie.GetEntitiebyId(id);

                // Verifica si hay datos en el resultado
                if (result.data != null)
                {
                    Doctor doctor = (Doctor)result.data;

                    // Mapea las propiedades de `Doctor` a `GetDoctorDto`
                    GetDoctorDto dto = new GetDoctorDto()
                    {
                        DoctorID = doctor.DoctorID,
                        LicenseExpirationDate = doctor.LicenseExpirationDate,
                        NameDoctor = doctor.NameDoctor,
                        ClinicAddress = doctor.ClinicAddress,
                        SpecialtyID = doctor.SpecialtyID,
                        AvailabilityModeId = doctor.AvailabilityModeId,
                        Bio = doctor.Bio,
                        CreatedAt = doctor.CreatedAt,
                        UpdatedAt = doctor.UpdatedAt,
                        PhoneNumber = doctor.PhoneNumber,
                        Education = doctor.Education,
                        YearsOfExperience = doctor.YearsOfExperience,
                        ConsultationFee = doctor.ConsultationFee,
                        LicenseNumber = doctor.LicenseNumber,
                    };

                        
                    response.model = dto;
                    response.Success = result.Sucess;
                }
                else
                {
                    response.Success = false;
                    response.Menssage = "Doctor no encontrado.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Error: {ex.Message} al obtener el doctor con ID {id}.";
                _logger.LogError(response.Menssage, ex);
            }

            return response;
        }


        public async Task<DoctorResponse> SaveAsync(DoctorSaveDto dto)
        {
            DoctorResponse response = new DoctorResponse();

            try
            {

                Doctor doctor = new Doctor();

                doctor.DoctorID = dto.DoctorID;
                doctor.NameDoctor = dto.NameDoctor;
                doctor.ConsultationFee = dto.ConsultationFee;
                doctor.LicenseNumber = dto.LicenseNumber;  
                doctor.PhoneNumber = dto.PhoneNumber;
                doctor.Bio = dto.Bio;
                doctor.ClinicAddress = dto.ClinicAddress; 
                doctor.AvailabilityModeId=dto.AvailabilityModeId;
                doctor.CreatedAt = dto.CreatedAt;
                doctor.UpdatedAt = dto.UpdatedAt;
                doctor.LicenseExpirationDate = dto.LicenseExpirationDate;
                doctor.Education = dto.Education;
                doctor.SpecialtyID = dto.SpecialtyID;


               response.model = await _doctorRepositorie.Add(doctor);
               response.Menssage = " User register Succefully! ";
        
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Erro {ex.Message} tratando de guardar usuario ";
                _logger.LogError(response.Menssage, ex.ToString());
            }

            return response;
        }

        public async Task<DoctorResponse> UpdateAsync(DoctorUpdateDto dto)
        {
            DoctorResponse response = new DoctorResponse();

            try
            {
                var result = await _doctorRepositorie.GetEntitiebyId(dto.DoctorID);

                if (result.data != null) 
                {
                    Doctor doctor = (Doctor)result.data; 
                    
                    doctor.NameDoctor = dto.NameDoctor;
                    doctor.ConsultationFee = dto.ConsultationFee;
                    doctor.Bio = dto.Bio;
                    doctor.PhoneNumber = dto.PhoneNumber;
                    doctor.ClinicAddress = dto.ClinicAddress;
                    doctor.LicenseNumber = dto.LicenseNumber;
                    doctor.Education = dto.Education;
                    doctor.UpdatedAt = dto.UpdatedAt; 
                    doctor.LicenseExpirationDate = dto.LicenseExpirationDate;
                    doctor.SpecialtyID = dto.SpecialtyID;
                    doctor.YearsOfExperience = dto.YearsOfExperience;
                    doctor.IsActive = dto.IsActive;

                    var updateResult = await _doctorRepositorie.Update(doctor);
                    response.model = updateResult;
                    response.Menssage = "Doctor updated successfully!";
                }
                else
                {
                    response.Menssage = "Doctor not found!";
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Error: {ex.Message} while updating the doctor.";
                _logger.LogError(response.Menssage, ex);
            }

            return response;
        }

    }
}
