

using Medical.Domain.Entities.Confi.Users;
using MedicalAppointment.Persistance.Interfaces.Configuration.UsersInterfaces;
using MedicalAppointment.Persistance.Repositorie.Configuration;
using MedicalCoreAplications.cs.Contracts.Configurations;
using MedicalCoreAplications.cs.Dtos.Configuration.PatientDtos.cs;
using MedicalCoreAplications.cs.Response.Configurations;
using Microsoft.Extensions.Logging;

namespace MedicalCoreAplications.cs.Services
{
    public class PatientServices : IPatientsServices
    {
        private readonly IPatientInterfaces _patientrepositorie; 
        private readonly ILogger<PatientServices> _logger; 

        public PatientServices(IPatientInterfaces patientrepositorie, ILogger<PatientServices> logger)
        { 

            if(patientrepositorie is null) throw new ArgumentNullException(nameof(patientrepositorie));


            _patientrepositorie = patientrepositorie;
            this._logger = logger;
        }

        public async Task<PatientResponse> getall()
        {
            PatientResponse response = new PatientResponse();


            try
            {
                var result = await _patientrepositorie.Getall();

                List<GetPatientsDtos> patients = ((List<Patient>)result.data!)
                    .Select(patients => new GetPatientsDtos()
                {
                        PatientID = patients.PatientID,
                        NamePatient = patients.NamePatient,
                        Address = patients.Address,
                        CreatedAt = patients.CreatedAt,
                        DateofBirth = patients.DateofBirth,
                        EmergencyContactPhone = patients.EmergencyContactPhone,
                        BloodType = patients.BloodType,
                        EmergencyContactName = patients.EmergencyContactName,
                        IsActive = true,    
                        UpdatedAt = patients.UpdatedAt,
                       
                }).ToList();


                response.model = patients;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} try to list all of the patients ";
               _logger.LogError(response.Menssaje, ex.ToString());
            }

            return response;
        }

        public async Task<PatientResponse> GetById(int id)
        {
            PatientResponse response = new PatientResponse();

            try
            {
                var result = await _patientrepositorie.GetEntitiebyId(id);

                if (result.Sucess)
                {
                    Patient patient = (Patient)result.data!;

                    GetPatientsDtos getPatientsDtos = new GetPatientsDtos()
                    {
                        NamePatient = patient.NamePatient,
                        PatientID = id,
                        EmergencyContactPhone = patient.EmergencyContactPhone,
                        EmergencyContactName = patient.EmergencyContactName,
                        Address = patient.Address,
                        CreatedAt = patient.CreatedAt,
                        UpdatedAt = patient.UpdatedAt,
                        BloodType = patient.BloodType,
                        DateofBirth = patient.DateofBirth,
                        IsActive = patient.IsActive,

                    };

                    response.model = patient;
                    response.Menssaje = " Patient has been found it succefully! ";
                }
                else
                {
                    response.success = false;
                    response.Menssaje = "patient's id has not been found it on the register ! ";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} looking the patients's id!  ";
               _logger.LogError(response.Menssaje, ex.ToString());

            }

            return response; 

        }

        public async Task<PatientResponse> SaveAsync(SavePatientsDtos dto)
        { 

            PatientResponse response = new PatientResponse();

            try
            {
                Patient patient = new Patient();
                patient.NamePatient = dto.NamePatient; 
                patient.Address = dto.Address; 
                patient.EmergencyContactName = dto.EmergencyContactName;
                patient.DateofBirth = dto.DateofBirth;
                patient.BloodType = dto.BloodType;
                patient.EmergencyContactPhone = dto.EmergencyContactPhone;  


                var result = await _patientrepositorie.Add(patient);

                response.model = result;
                response.Menssaje = " Patient has been register succefully! ";
                
               
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} saving the patient! ";
               _logger.LogError(response.Menssaje, ex.ToString());

            }

            return response; 
        } 

        public async Task<PatientResponse> UpdateAsync(UpdatePatientsDtos dto)
        {
            PatientResponse response = new PatientResponse();

            try
            {

                var result = await _patientrepositorie.GetEntitiebyId(dto.PatientID);

                if (result.Sucess)
                {
                    Patient patient = (Patient)result.data!;

                    patient.PatientID = dto.PatientID;
                    patient.NamePatient = dto.NamePatient;
                    patient.DateofBirth = dto.DateofBirth;
                    patient.Address = dto.Address;
                    patient.EmergencyContactName = dto.EmergencyContactName;
                    patient.EmergencyContactPhone = dto.EmergencyContactPhone;
                    patient.BloodType = dto.BloodType;
                    patient.IsActive = dto.IsActive;


                    var datos = await _patientrepositorie.Update(patient);
                    response.model = datos;
                    response.Menssaje = "patint has been updated succefully! ";

                }
                else
                {
                    response.success = false;
                    response.Menssaje = "patient to updated has not been found on the register! "; 
                }

            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} updating the patient! ";
                _logger.LogError(response.Menssaje, ex.ToString());

            }

            return response; 

        }
    }
}
