using Medical.Domain.Entities.Confi.Users;
using Medical.Percistances.cs.Base;
using Medical.Percistances.cs.Context;
using MedicalAppointment.Domain.Result;
using MedicalAppointment.Persistance.Interfaces.Configurations;
using MedicalAppointment.Persistance.Model.Systems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Logging;


namespace MedicalAppointment.Persistance.Repositorie.Configuration
{
    public class PatientRepositorie : BaseRepositorie<Patient>, IPatientRepository
    {

        private readonly MedicalContext _dbcontext;
        private readonly ILogger<PatientRepositorie> _logger;



        public PatientRepositorie(MedicalContext context,
                                 ILogger<PatientRepositorie> logger) : base(context)
        {
            _dbcontext = context;
            _logger = logger;
        }

        public override async Task<OperationResult> Add(Patient entities)
        {
            OperationResult result = new OperationResult();

            // manejo de exepciones
            if (string.IsNullOrEmpty(entities.NamePatient) && string.IsNullOrEmpty(entities.Address) && string.IsNullOrEmpty(entities.EmergencyContactName)
                && string.IsNullOrEmpty(entities.EmergencyContactPhone) && string.IsNullOrEmpty(entities.Allergies))
            {
                result.Sucess = false;
                result.Message = "No puedes dejar valores vasios! ";
                return result;
            }


            if (entities.PatientID <= 0 && entities.InsuranceProviderID <= 0)
            {
                result.Sucess = false;
                result.Message = "No puedes ingresar valores menores e iguales a 0 ";
                return result;

            }

            if (entities.BloodType == '\0' && entities.Gender == '\0')
            {
                result.Sucess = false;
                result.Message = " No puedes dejar campos vacios! ";
                return result;

            }

            if (entities.Address.Length < 5)
            {
                result.Sucess = false;
                result.Message = " La direccion no tiene suficientes digitos! ";
                return result;

            }


            if (await base.Exist(patient => patient.PatientID == entities.PatientID && patient.InsuranceProviderID == entities.InsuranceProviderID))
            {
                result.Sucess = false;
                result.Message = "Este usuario ya existe en el registro! ";
                return result;
            }


            try
            {
                await base.Add(entities);
                result.data = entities;

                result.Message = "Paciente agregado exitosamente! ";
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Erro tipo {ex.Message} tratando de guardar paciente! ";
                _logger.LogError(result.Message, ToString());
            }

            return result;

        }

        public override async Task<OperationResult> Delete(Patient entities)
        {
            OperationResult result = new OperationResult();


            if (string.IsNullOrEmpty(entities.NamePatient) || string.IsNullOrEmpty(entities.Allergies) ||
               string.IsNullOrEmpty(entities.Address) || string.IsNullOrEmpty(entities.EmergencyContactName) ||
               string.IsNullOrEmpty(entities.EmergencyContactPhone))
            {
                result.Sucess = false;
                result.Message = "No puedes dejar campos vacios! ";
                return result;

            }

            if (entities.BloodType == '\0' && entities.Gender == '\0')
            {
                result.Sucess = false;
                result.Message = " No puedes dejar campos vacios! ";
                return result;

            }

            if (entities.Address.Length < 5)
            {
                result.Sucess = false;
                result.Message = " La direccion no tiene suficientes digitos! ";
                return result;

            }

            if (entities.PatientID <= 0 && entities.InsuranceProviderID <= 0)
            {
                result.Sucess = false;
                result.Message = "No puedes ingresar valores menores e iguales a 0 ";
                return result;

            }

            try
            {
                Patient? patient = await _dbcontext.Patients.FindAsync(entities.PatientID);
                if (patient == null)
                {
                    result.Message = " No puedes dejar valor vasio ";
                    result.Sucess = false;
                    return result;

                }

                result.data = await base.Delete(patient);
                result.Message = $"Paciente con id:{patient.PatientID} eliminado correctamente!  ";

            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo {ex.Message} al remover paciente del registro! ";
                _logger.LogError(result.Message, ToString());

            }

            return result;

        }

        public override async Task<OperationResult> Update(Patient entities)
        {
            OperationResult result = new OperationResult();

            if (string.IsNullOrEmpty(entities.NamePatient) || string.IsNullOrEmpty(entities.Allergies) ||
           string.IsNullOrEmpty(entities.Address) || string.IsNullOrEmpty(entities.EmergencyContactName) ||
           string.IsNullOrEmpty(entities.EmergencyContactPhone))
            {
                result.Sucess = false;
                result.Message = "No puedes dejar campos vacios! ";
                return result;

            }

            if (entities.BloodType == '\0' && entities.Gender == '\0')
            {
                result.Sucess = false;
                result.Message = " No puedes dejar campos vacios! ";
                return result;

            }

            if (entities.Address.Length < 5)
            {
                result.Sucess = false;
                result.Message = " La direccion no tiene suficientes digitos! ";
                return result;

            }

            if (entities.PatientID <= 0 && entities.InsuranceProviderID <= 0)
            {
                result.Sucess = false;
                result.Message = "No puedes ingresar valores menores e iguales a 0 ";
                return result;

            }

            try
            {
                Patient? patientUpdate = await _dbcontext.Patients.FindAsync(entities);

                if (patientUpdate == null)
                {
                    result.Sucess = false;
                    result.Message = "No puedes dejar el campo vasio! ";
                    return result;

                }

                patientUpdate.PatientID = patientUpdate.PatientID;
                patientUpdate.NamePatient = entities.NamePatient;
                patientUpdate.Allergies = entities.Allergies;
                patientUpdate.BloodType = entities.BloodType;
                patientUpdate.DateofBirth = entities.DateofBirth;
                patientUpdate.InsuranceProviderID = entities.InsuranceProviderID;
                patientUpdate.EmergencyContactName = entities.EmergencyContactName;
                patientUpdate.EmergencyContactPhone = entities.EmergencyContactPhone;
                patientUpdate.Gender = entities.Gender;
                patientUpdate.CreatedAt = entities.CreatedAt;
                patientUpdate.IsActive = entities.IsActive;
                patientUpdate.Address = entities.Address;


                result.data = await base.Update(patientUpdate);
                result.Message = " Paciente Actulizado correctamente! ";


            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo {ex.Message} actualizando Paciente ";
                _logger.LogError(result.Message, ToString());

            }

            return result;

        }



        public override async Task<OperationResult> Getall()
        {

            OperationResult result = new OperationResult();

            try
            {
                var patientsWithInsurance = await (from patient in _dbcontext.Patients
                                                   join insuranceProvider in _dbcontext.Patients
                                                   on patient.InsuranceProviderID equals insuranceProvider.InsuranceProviderID
                                                   select new PatientModel
                                                   {

                                                       PatientID = patient.PatientID,
                                                       NamePatient = patient.NamePatient,
                                                       EmergencyContactName = patient.EmergencyContactName,
                                                       Address = patient.Address,
                                                       Allergies = patient.Allergies,
                                                       BloodType = patient.BloodType,
                                                       DateofBirth = patient.DateofBirth,
                                                       EmergencyContactPhone = patient.EmergencyContactPhone,
                                                       Gender = patient.Gender,
                                                       InsuranceProviderID = patient.InsuranceProviderID,


                                                   }).AsNoTracking()
                                                   .ToListAsync();


                result.data = patientsWithInsurance;

            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo {ex.Message} tratando de listar pacientes ";
                _logger.LogError(result.Message, ToString());
            }

            return result;


        }



        public async override Task<OperationResult> GetEntitiebyId(int id)
        {
            OperationResult result = new OperationResult(); 

            if (id <= 0)
            {
                result.Sucess = false;
                result.Message = " No puedes ingresar id menor o igual a  0! ";
            }

            try
            {
                var patientsWithInsurance = await (from patient in _dbcontext.Patients
                                                   join insuranceProvider in _dbcontext.Patients
                                                   on patient.InsuranceProviderID equals insuranceProvider.InsuranceProviderID
                                                   select new PatientModel
                                                   {

                                                       PatientID = patient.PatientID,
                                                       NamePatient = patient.NamePatient,
                                                       EmergencyContactName = patient.EmergencyContactName,
                                                       Address = patient.Address,
                                                       Allergies = patient.Allergies,
                                                       BloodType = patient.BloodType,
                                                       DateofBirth = patient.DateofBirth,
                                                       EmergencyContactPhone = patient.EmergencyContactPhone,
                                                       Gender = patient.Gender,
                                                       InsuranceProviderID = patient.InsuranceProviderID,


                                                   }).FirstOrDefaultAsync();

                result.data = patientsWithInsurance;


            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo {ex.Message} tratando de encontrar pacientes ";
                _logger.LogError(result.Message, ToString());
            }

            return result;

        }

  
    }
}
        