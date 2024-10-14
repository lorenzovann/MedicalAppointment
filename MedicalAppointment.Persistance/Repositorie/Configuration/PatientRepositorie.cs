using Medical.Domain.Entities.Confi.Users;
using Medical.Percistances.cs.Base;
using Medical.Percistances.cs.Context;
using MedicalAppointment.Domain.Result;
using MedicalAppointment.Persistance.Interfaces.Configuration.UsersInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace MedicalAppointment.Persistance.Repositorie.Configuration
{
    internal class PatientRepositorie : BaseRepositorie<Patient>, IPatientRepositorie
    {

        private readonly MedicalContext _dbcontext;
        private readonly Logger<PatientRepositorie> _logger;



        public PatientRepositorie(MedicalContext context,
                                 Logger<PatientRepositorie> logger) : base(context)
        {
            _dbcontext = context;
            this._logger = logger;
        }

        public override async Task<OperationResult> Add(Patient entities)
        {
            OperationResult result = new OperationResult();

            // manejo de exepciones
            if (string.IsNullOrEmpty(entities.Name) && string.IsNullOrEmpty(entities.Address) && string.IsNullOrEmpty(entities.EmergencyContactName)
                && string.IsNullOrEmpty(entities.EmergencyContactPhone) && string.IsNullOrEmpty(entities.Allergies))
            {
                result.Sucess = false;
                result.Message = "No puedes dejar valores vasios! ";
                return result;
            }


            if (entities.PatiendID <= 0 && entities.InsuranceProviderID <= 0)
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


            if (await base.Exist(patient => patient.PatiendID == entities.PatiendID && patient.InsuranceProviderID == entities.InsuranceProviderID))
            {
                result.Sucess = false;
                result.Message = "Este usuario ya existe en el registro! ";
                return result;
            }


            try
            {
                result.data = await base.Add(entities);
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


            if (string.IsNullOrEmpty(entities.Name) || string.IsNullOrEmpty(entities.Allergies) ||
               string.IsNullOrEmpty(entities.Address) || string.IsNullOrEmpty(entities.EmergencyContactName)||
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

            if (entities.PatiendID <= 0 && entities.InsuranceProviderID <= 0)
            {
                result.Sucess = false;
                result.Message = "No puedes ingresar valores menores e iguales a 0 ";
                return result;

            }

            try
            {
                Patient? patient = await _dbcontext.Patients.FindAsync(entities.PatiendID);
                if (patient == null)
                {
                    result.Message = " No puedes dejar valor vasio ";
                    result.Sucess = false;
                    return result; 

                }

                result.data = await base.Delete(patient);
                result.Message = $"Paciente con id:{patient.PatiendID} eliminado correctamente!  ";

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

            if (string.IsNullOrEmpty(entities.Name) || string.IsNullOrEmpty(entities.Allergies) ||
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

            if (entities.PatiendID <= 0 && entities.InsuranceProviderID <= 0)
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
         
                    patientUpdate.PatiendID = patientUpdate.PatiendID;
                    patientUpdate.Name = entities.Name;
                    patientUpdate.Allergies = entities.Allergies;
                    patientUpdate.BloodType = entities.BloodType;
                    patientUpdate.DateofBirth = entities.DateofBirth;
                    patientUpdate.InsuranceProviderID = entities.InsuranceProviderID;
                    patientUpdate.EmergencyContactName = entities.EmergencyContactName;
                    patientUpdate.EmergencyContactPhone = entities.EmergencyContactPhone;
                    patientUpdate.Gender = entities.Gender;
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
                                                   select new
                                                   {
                                                       patient.PatiendID,
                                                       patient.Name,
                                                       patient.DateofBirth,
                                                       patient.Gender,
                                                       patient.Address,
                                                       patient.EmergencyContactName,
                                                       patient.EmergencyContactPhone,
                                                       patient.BloodType,
                                                       patient.Allergies,
                                                       patient.InsuranceProviderID,
                                                       InsuranceProviderName = insuranceProvider.Name  // Nombre del proveedor de seguro
                                                   }).ToListAsync();

              
                result.data = patientsWithInsurance;

            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo {ex.Message} tratando de listar pacientes ";
                _logger.LogError(result.Message , ToString());
            }

            return result; 


        }
        public async Task<OperationResult> FindPatientById(int id)
        {
            OperationResult result = new OperationResult();

            // Validar que el ID sea mayor que 0
            if (id <= 0)
            {
                result.Sucess = false;
                result.Message = "No puedes ingresar números negativos o iguales a 0!";
                return result;
            }

            try
            {
                // Buscar paciente por ID y unir con proveedor de seguro
                var FindPatientById = await (from patient in _dbcontext.Patients
                                             join insuranceProvider in _dbcontext.Patients
                                             on patient.InsuranceProviderID equals insuranceProvider.InsuranceProviderID
                                             where patient.PatiendID == id &&
                                             patient.IsActive == true
                                             select new
                                             {
                                                 patient.PatiendID,
                                                 patient.Name,
                                                 patient.DateofBirth,
                                                 patient.Gender,
                                                 patient.Address,
                                                 patient.EmergencyContactName,
                                                 patient.EmergencyContactPhone,
                                                 patient.BloodType,
                                                 patient.Allergies,
                                                 InsuranceProviderName = insuranceProvider.Name  
                                             }).FirstOrDefaultAsync();

                // Verificar si el paciente fue encontrado
                if (FindPatientById != null)
                {
                    result.data = FindPatientById;  // Almacenar la información del paciente en el resultado
                }
                else
                {
                    result.Sucess = false;
                    result.Message = "Paciente no encontrado con el ID proporcionado.";
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepción
                result.Sucess = false;
                result.Message = $"Error tipo: {ex.Message} encontrando el paciente con ID {id}";
                _logger.LogError(result.Message, ex);
            }

            return result;
        }




    }
}
        