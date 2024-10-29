using Medical.Domain.Entities.Confi.Users;
using MedicalAppoiment.Aplication.cs.Contracts.configurations;
using MedicalAppoiment.Aplication.cs.Dtos.Cofiguration.PatientsDtos.cs;
using MedicalAppoiment.Aplication.cs.Response.Users;
using MedicalAppointment.Domain.IBaseRepositorie;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppoiment.Aplication.cs.Services.Configuration.Users
{
    public class PatientsServices : IPatientServices
    {

        private readonly PatientRepositorie _patientrepositorie;
        private readonly ILogger<PatientsServices> _logger; 

        public PatientsServices(PatientRepositorie patientrepositorie, 
                 ILogger<PatientsServices> logger)
        {   
            if(patientrepositorie is null)
            {
                throw new ArgumentNullException(nameof(patientrepositorie));

            }
            _patientrepositorie = patientrepositorie;
             this._logger = logger;
            }

         public async Task<PatientResponse> getall()
        {
             PatientResponse response = new PatientResponse();

            try
            {
                var result = await _patientrepositorie.Getall();

                List<GetPatientsDto> listado = ((List<GetPatientsDto>)result.data)
                                          .Select(patients => new GetPatientsDto()
                {
                       PatientID = patients.PatientID,
                       NamePatient = patients.NamePatient,
                       InsuranceProviderID = patients.InsuranceProviderID,
                       Address = patients.Address,
                       BloodType = patients.BloodType,
                       EmergencyContactName = patients.EmergencyContactName,
                       EmergencyContactPhone = patients.EmergencyContactPhone,
                       Gender = patients.Gender,
                       Allergies = patients.Allergies,
                       IsActive = patients.IsActive,
                       DateofBirth = patients.DateofBirth,
                       CreatedAt = patients.CreatedAt,
                       UpdatedAt = patients.UpdatedAt,

                }).ToList();

                response.Success = result.Sucess;
                response.model = listado;
                response.Menssage = "List ocurr Succefully! ";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Erro occur {ex.Message}";
               _logger.LogError(response.Menssage, ex.ToString()); 

            }

            return response;

        }

        public async Task<PatientResponse> GetById(int id)
        {
            PatientResponse response = new PatientResponse();

            try
            {
                var result = await _patientrepositorie.GetEntitiebyId(id); 
                if(result.data != null)
                {
                    Patient patient = (Patient)result.data;

                    GetPatientsDto datos = new GetPatientsDto()
                    {
                        PatientID = id,
                        NamePatient = patient.NamePatient,
                        InsuranceProviderID = patient.InsuranceProviderID,
                        Address = patient.Address,
                        EmergencyContactPhone = patient.EmergencyContactPhone,
                        EmergencyContactName = patient.EmergencyContactName,
                        Allergies = patient.Allergies,
                        DateofBirth = patient.DateofBirth,
                        BloodType = patient.BloodType,
                        Gender = patient.Gender,
                        CreatedAt = patient.CreatedAt,
                        UpdatedAt = patient.UpdatedAt,
                        IsActive = patient.IsActive,
                    };

                    response.model = datos;
                    response.Menssage = "id found sucefully!";
                } else
                {

                    response.Success = false;
                    response.Menssage = "id not found on the regiter! ";

                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Erro occur {ex.Message}";
               _logger.LogError(response.Menssage, ex.ToString());
            }

            return response;
        }

        public async Task<PatientResponse> SaveAsync(SavePatientsDto dto)
        {
           PatientResponse response = new PatientResponse();


            try
            {
               Patient patient = new Patient();  
               patient.NamePatient = dto.NamePatient;  
               patient.BloodType = dto.BloodType;
               patient.Gender = dto.Gender;
               patient.CreatedAt = dto.CreatedAt;
               patient.UpdatedAt = dto.UpdatedAt;
               patient.Allergies = dto.Allergies;
               patient.Address  = dto.Address;
               patient.DateofBirth = dto.DateofBirth;  
               patient.EmergencyContactName = dto.EmergencyContactName;
               patient.EmergencyContactPhone = dto.EmergencyContactPhone;
               patient.InsuranceProviderID = dto.InsuranceProviderID;  
                
                var result = await _patientrepositorie.Add(patient);
                response.model = result;
                response.Menssage = " User register succefully! ";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Erro occur {ex.Message} tried to save the entitie! ";
               _logger.LogError(response.Menssage, ex.ToString());
            }



            return response;

        }

        public async Task<PatientResponse> UpdateAsync(PatientsUpdpateDto dto)
        {
           PatientResponse response = new PatientResponse();


            try
            {
                var result = await _patientrepositorie.FindPatientById(dto.PatientID);
                if (result != null)
                {
                    Patient patient = (Patient)result.data;

                    patient.PatientID = dto.PatientID;  
                    patient.NamePatient = dto.NamePatient;
                    patient.EmergencyContactName = dto.EmergencyContactName;
                    patient.EmergencyContactPhone = dto.EmergencyContactPhone;
                    patient.IsActive = dto.IsActive;
                    patient.Address = dto.Address;
                    patient.Allergies = dto.Allergies; 
                    patient.BloodType = dto.BloodType;
                    patient.CreatedAt = dto.CreatedAt;
                    patient.UpdatedAt = dto.UpdatedAt;
                    patient.DateofBirth = dto.DateofBirth;
                    patient.InsuranceProviderID = dto.InsuranceProviderID; 
                    patient.Gender = dto.Gender; 

                    var datos = await _patientrepositorie.Update(patient);
                    response.model = datos;
                    response.Menssage = "Patient Updated succefully! ";
                } else
                {
                    response.Success = false;
                    response.Menssage = $" Patient not Found! ";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Menssage = $"Erro occur {ex.Message} tried to save the entitie! ";
               _logger.LogError(response.Menssage, ex.ToString());
            }
            return response;

        }
    }
}
