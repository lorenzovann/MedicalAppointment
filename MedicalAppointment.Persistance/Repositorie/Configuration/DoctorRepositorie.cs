


using Medical.Domain.Entities.Confi.Users;
using Medical.Percistances.cs.Base;
using Medical.Percistances.cs.Context;
using MedicalAppointment.Domain.Result;

using MedicalAppointment.Persistance.Interfaces.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Persistance.Repositorie.Configuration
{
    public sealed class DoctorRepositorie : BaseRepositorie<Doctor>, IDoctorRepository
    {

        private readonly MedicalContext _dbContext;
        private readonly ILogger<DoctorRepositorie> _logger;

        public DoctorRepositorie(MedicalContext context,
                  ILogger<DoctorRepositorie> logger) : base(context)
        {
            _dbContext = context;
            _logger = logger;
        }

        public override async Task<OperationResult> Add(Doctor entities)
        {
            OperationResult result = new OperationResult();

            // Validar si el nombre, número de licencia, educación están vacíos
            if (string.IsNullOrEmpty(entities.NameDoctor) || string.IsNullOrEmpty(entities.LicenseNumber) || string.IsNullOrEmpty(entities.Education))
            {
                result.Sucess = false;
                result.Message = "El nombre, número de licencia y la educación son campos obligatorios.";
                return result;
            }

            // Validar si el SpecialtyID o YearofExperiences son inválidos
            if (entities.SpecialtyID <= 0 || entities.YearsOfExperience <= 0)
            {
                result.Sucess = false;
                result.Message = "El ID de especialidad debe ser positivo y los años de experiencia no pueden ser negativos.";
                return result;
            }

            // Validar la tasa de consulta (si está presente)
            if (entities.ConsultationFee.HasValue && entities.ConsultationFee.Value <= 0)
            {
                result.Sucess = false;
                result.Message = "La tarifa de consulta debe ser mayor que 0.";
                return result;
            }

            // Validar la fecha de expiración de la licencia
            if (entities.LicenseExpirationDate < DateTime.Now)
            {
                result.Sucess = false;
                result.Message = "La licencia ha expirado.";
                return result;
            }

            // Validar la dirección de la clínica si está presente
            if (!string.IsNullOrEmpty(entities.ClinicAddress) && entities.ClinicAddress.Length < 5)
            {
                result.Sucess = false;
                result.Message = "La dirección de la clínica es demasiado corta.";
                return result;
            }

            // Validar el modo de disponibilidad (si está presente)
            if (entities.AvailabilityModeId.HasValue && entities.AvailabilityModeId <= 0)
            {
                result.Sucess = false;
                result.Message = "El ID del modo de disponibilidad no es válido.";
                return result;
            }

            try
            {

                await base.Add(entities);
                result.data = entities;
                result.Message = " Doctor agregado exitosamente!";
        

            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo {ex.Message} tratando de guardar el doctor!";
                _logger.LogError(result.Message, ex.ToString()); // Registrar el error correctamente
            }

            return result;
        }

        public override async Task<OperationResult> Delete(Doctor entities)
        {
            OperationResult result = new OperationResult();

           if (string.IsNullOrEmpty(entities.NameDoctor) || string.IsNullOrEmpty(entities.LicenseNumber) || string.IsNullOrEmpty(entities.Education))
            {
                result.Sucess = false;
                result.Message = "El nombre, número de licencia y la educación son campos obligatorios.";
                return result;
            }

            // Validar si el SpecialtyID o YearofExperiences son inválidos
            if (entities.SpecialtyID <= 0 || entities.YearsOfExperience < 0)
            {
                result.Sucess = false;
                result.Message = "El ID de especialidad debe ser positivo y los años de experiencia no pueden ser negativos.";
                return result;
            }

            // Validar la tasa de consulta (si está presente)
            if (entities.ConsultationFee.HasValue && entities.ConsultationFee.Value <= 0)
            {
                result.Sucess = false;
                result.Message = "La tarifa de consulta debe ser mayor que 0.";
                return result;
            }

            // Validar la fecha de expiración de la licencia
            if (entities.LicenseExpirationDate < DateTime.Now)
            {
                result.Sucess = false;
                result.Message = "La licencia ha expirado.";
                return result;
            }

            // Validar la dirección de la clínica si está presente
            if (!string.IsNullOrEmpty(entities.ClinicAddress) && entities.ClinicAddress.Length < 5)
            {
                result.Sucess = false;
                result.Message = "La dirección de la clínica es demasiado corta.";
                return result;
            }

            // Validar el modo de disponibilidad (si está presente)
            if (entities.AvailabilityModeId.HasValue && entities.AvailabilityModeId <= 0)
            {
                result.Sucess = false;
                result.Message = "El ID del modo de disponibilidad no es válido.";
                return result;
            }


            try
            {
                Doctor? doctorRemove = await _dbContext.Doctors.FindAsync(entities.DoctorID);

                if (doctorRemove != null)
                {
                    await base.Delete(doctorRemove);
                    result.data = doctorRemove;
                    result.Message = $" Doctor {entities.DoctorID} Eliminado Exitosamente! ";
                    return result;
                } else
                {
                    result.Sucess= false;
                    result.Message = "Doctor no a sido enctrado! ";
                }

            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo {ex.Message} tratando de remover Doctor ";
                _logger.LogError(result.Message, ToString());


            }

            return result;


        }

        public override async Task<OperationResult> Update(Doctor entities)
        {
            OperationResult result = new OperationResult();


            if (string.IsNullOrEmpty(entities.NameDoctor) 
                || string.IsNullOrEmpty(entities.LicenseNumber) 
                || string.IsNullOrEmpty(entities.Education))
            {
                result.Sucess = false;
                result.Message = "El nombre, número de licencia y la educación son campos obligatorios.";
                return result;
            }

            // Validar si el SpecialtyID o YearofExperiences son inválidos
            if (entities.SpecialtyID <= 0  || entities.YearsOfExperience < 0)
            {
                result.Sucess = false;
                result.Message = " los años de experiencia no pueden ser negativos.";
                return result;
            }

            // Validar la tasa de consulta (si está presente)
            if (entities.ConsultationFee.HasValue && entities.ConsultationFee.Value <= 0)
            {
                result.Sucess = false;
                result.Message = "La tarifa de consulta debe ser mayor que 0.";
                return result;
            }

            // Validar la fecha de expiración de la licencia
            if (entities.LicenseExpirationDate < DateTime.Now)
            {
                result.Sucess = false;
                result.Message = "La licencia ha expirado.";
                return result;
            }

            // Validar la dirección de la clínica si está presente
            if (!string.IsNullOrEmpty(entities.ClinicAddress)  && entities.ClinicAddress.Length < 5)
            {
                result.Sucess = false;
                result.Message = "La dirección de la clínica es demasiado corta.";
                return result;
            }

            // Validar el modo de disponibilidad (si está presente)
            if (entities.AvailabilityModeId.HasValue && entities.AvailabilityModeId <= 0)
            {
                result.Sucess = false;
                result.Message = "El ID del modo de disponibilidad no es válido.";
                return result;
            }

        

            try
            {
                // consulta con base de datos para actualizar el usuario
                Doctor? doctorUpdate = await _dbContext.Doctors.FindAsync(entities);

                if (doctorUpdate == null)
                {
                    result.Sucess = false;
                    result.Message = "No puedes actualizar registro se nesecitan llenar campos! ";
                    return result;

                }

                doctorUpdate.NameDoctor = entities.NameDoctor;
            
                doctorUpdate.DoctorID = entities.DoctorID;
                doctorUpdate.YearsOfExperience = entities.YearsOfExperience;
                doctorUpdate.SpecialtyID = entities.SpecialtyID;
                doctorUpdate.LicenseNumber = entities.LicenseNumber;
                doctorUpdate.ClinicAddress = entities.ClinicAddress;
                doctorUpdate.ConsultationFee = entities.ConsultationFee;
                doctorUpdate.LicenseExpirationDate = entities.LicenseExpirationDate;
                doctorUpdate.AvailabilityModeId = entities.AvailabilityModeId;
                doctorUpdate.CreatedAt = entities.CreatedAt;
                doctorUpdate.PhoneNumber = entities.PhoneNumber;
                doctorUpdate.UpdatedAt = entities.UpdatedAt;
                doctorUpdate.Bio = entities.Bio;
                doctorUpdate.Education = entities.Education;

                await base.Update(doctorUpdate);
                result.data = doctorUpdate;
                result.Message = " Doctor actulizado exitosamente! ";
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error {ex.Message} al trata de Actualizar los Doctores! ";
                _logger.LogError(result.Message, ToString());

            }

            return result;

        }


        public override async Task<OperationResult> Getall()
        {
            OperationResult result = new OperationResult();

            try
            {
                // Consulta para obtener los doctores con sus roles
                var doctor =  await _dbContext.Doctors.ToListAsync();

                result.data = doctor;

            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo {ex.Message} tratando de obtener doctores ";
                _logger.LogError(result.Message, ToString());
            }

            return result;
        }



        public override async Task<OperationResult> GetEntitiebyId(int id)
        {
            OperationResult result = new OperationResult();

            // Validación del ID
            if (id <= 0)
            {
                result.Sucess = false;
                result.Message = "ID no puede ser negativo ni 0!";
                return result;
            }


            try
            {

                // Consulta para obtener el doctor por ID, junto con su rol
                var doctorWithRole = await (from doctor in _dbContext.Doctors
                                            join SystemRole in _dbContext.Roles on doctor.DoctorID equals SystemRole.RoleID
                                            where doctor.DoctorID == id
                                            && doctor.IsActive == true
                                            orderby doctor.CreatedAt descending
                                            select new
                                            {
                                                doctor.DoctorID,
                                                doctor.NameDoctor,
                                                doctor.SpecialtyID,
                                                doctor.LicenseNumber,
                                                doctor.YearsOfExperience,
                                                doctor.Education,
                                                doctor.Bio,
                                                doctor.ConsultationFee,
                                                doctor.ClinicAddress,
                                                doctor.AvailabilityModeId,
                                                doctor.IsActive,
                                                doctor.UpdatedAt,
                                                doctor.CreatedAt,
                                                doctor.PhoneNumber,
                                                doctor.LicenseExpirationDate,
                                                DoctorRole = SystemRole.RoleID,   // Nombre del rol del doctor
                                            }).FirstOrDefaultAsync();

                if (doctorWithRole == null)
                {
                    result.Sucess = false;
                    result.Message = "Doctor no encontrado!";
                    return result;
                }


                result.data = doctorWithRole;
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo {ex.Message} tratando de obtener el doctor con ID {id}";
                _logger.LogError(result.Message, ToString());
            }

            return result;
        }


    }
}
