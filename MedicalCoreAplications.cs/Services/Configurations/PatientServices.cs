using Medical.Domain.Entities.Confi.Users;
using MedicalAppointment.Persistance.Interfaces.Configurations;
using MedicalCoreAplications.cs.Contracts.Configurations;
using MedicalCoreAplications.cs.Dtos.Configurations.PatientDtos;
using MedicalCoreAplications.cs.Response.Configurations;
using Microsoft.Extensions.Logging;

namespace MedicalCoreAplications.cs.Services.Configurations
{
    public class PatientServices : IPatientServices
    { 
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger<PatientServices> _logger;  

        public PatientServices(IPatientRepository patientRepository, ILogger<PatientServices> logger)
        {

            if(patientRepository is null) throw new ArgumentNullException(nameof(patientRepository));


            _patientRepository = patientRepository;
            _logger = logger;
        }

        public async Task<PatientResponse> getall()
        {
            PatientResponse  response = new PatientResponse();

            try
            {
                var result = await _patientRepository.Getall();

               

                response.success = result.Sucess;
                response.model = result.data;
                response.Menssaje = "Patient List. ";


            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} getting all of the patients ";
               _logger.LogError(response.Menssaje, ex.ToString());

            }

            return response;
        }
                
        public async Task<PatientResponse> GetById(int id)
        {
            PatientResponse response = new PatientResponse();


            try
            {
                var result =  await _patientRepository.GetEntitiebyId(id);
                if (result.Sucess)
                {

                 

                    response.success = result.Sucess;
                    response.model = result.data;
                    response.Menssaje = "Patient has been found it succefully! ";
      

                } else
                {
                    response.success = false;
                    response.Menssaje = " Patient has not been found it on the register! ";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} getting all of the patients id ";
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
                patient.CreatedAt = dto.CreatedAt;
                patient.UpdatedAt = dto.UpdatedAt;
                patient.Address = dto.Address; 
                patient.InsuranceProviderID = dto.InsuranceProviderID;  
                patient.BloodType = dto.BloodType;
                patient.EmergencyContactPhone = dto.EmergencyContactPhone;
                patient.Gender = dto.Gender;
                patient.DateofBirth = dto.DateofBirth;
                patient.IsActive = true; 


                var result = await _patientRepository.Add(patient);
                response.success = result.Sucess;
                response.model = result.data;
                response.Menssaje = "Patient has been added to the register! ";


            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} saving the doctor  ";
               _logger.LogError(response.Menssaje, ex.ToString());

            }
            return response;    

        }   

        public async Task<PatientResponse> UpdateAsync(UpdatePatientsDtos dto)
        {
            PatientResponse response = new PatientResponse();

            try
            {
                var result = await _patientRepository.GetEntitiebyId(dto.PatientID);

                Patient patient = (Patient)result.data!; 

                patient.PatientID = dto.PatientID;
                patient.NamePatient = dto.NamePatient;
                patient.InsuranceProviderID = dto.InsuranceProviderID;
                patient.Address = dto.Address;
                patient.IsActive = dto.IsActive;
                patient.CreatedAt = dto.CreatedAt;  
                patient.UpdatedAt = dto.UpdatedAt;  
                patient.BloodType = dto.BloodType;
                patient.Gender = dto.Gender;    
                patient.DateofBirth = dto.DateofBirth;
                patient.EmergencyContactName = dto.EmergencyContactPhone; 

                var datos = await _patientRepository.Update(patient);
                response.success = datos.Sucess;
                response.model = datos.data;
                response.Menssaje = "Patient has been updated sucefully! ";

               
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Menssaje = $"Erro type {ex.Message} Updating the doctor  ";
               _logger.LogError(response.Menssaje, ex.ToString());

            }


            return response; 

        }
    }
}
