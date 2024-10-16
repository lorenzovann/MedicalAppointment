


using Medical.Domain.Entities.Confi.Users;
using Medical.Percistances.cs.Base;
using Medical.Percistances.cs.Context;
using MedicalAppointment.Domain.Result;
using MedicalAppointment.Persistance.Interfaces.Configuration.UsersInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Persistance.Repositorie.Configuration
{
    public class DoctorRepositorie : BaseRepositorie<Doctor>, IDoctorInterfaces
    {

        private readonly MedicalContext _dbContext;
        private readonly Logger<DoctorRepositorie> _logger;

        public DoctorRepositorie(MedicalContext context,
                  Logger<DoctorRepositorie> logger) : base(context)
        {
            _dbContext = context;
            this._logger = logger;
        }

        public override async Task<OperationResult> Add(Doctor entities)
        {
            OperationResult result = new OperationResult();

            // Validar si el nombre, número de licencia, educación están vacíos
            if (string.IsNullOrEmpty(entities.Name) || string.IsNullOrEmpty(entities.LicenseNumber) || string.IsNullOrEmpty(entities.Education))
            {
                result.Sucess = false;
                result.Message = "El nombre, número de licencia y la educación son campos obligatorios.";
                return result;
            }

            // Validar si el SpecialtyID o YearofExperiences son inválidos
            if (entities.SpecialtyID <= 0 || entities.YearofExperinces < 0)
            {
                result.Sucess = false;
                result.Message = "El ID de especialidad debe ser positivo y los años de experiencia no pueden ser negativos.";
                return result;
            }

            // Validar la tasa de consulta (si está presente)
            if (entities.ConsultacionFee.HasValue && entities.ConsultacionFee.Value <= 0)
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
            if (!string.IsNullOrEmpty(entities.ClinicAdress) && entities.ClinicAdress.Length < 5)
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


                result.data = await base.Add(entities); 
                result.Message = " Doctor agregado exitosamente!";
        

            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo {ex.Message} tratando de guardar el doctor!";
                _logger.LogError(result.Message, ToString()); // Registrar el error correctamente
            }

            return result;
        }

        public override async Task<OperationResult> Delete(Doctor entities)
        {
            OperationResult result = new OperationResult();

            if (string.IsNullOrEmpty(entities.Name) || string.IsNullOrEmpty(entities.LicenseNumber) || string.IsNullOrEmpty(entities.Education))
            {
                result.Sucess = false;
                result.Message = "El nombre, número de licencia y la educación son campos obligatorios.";
                return result;
            }

            // Validar si el SpecialtyID o YearofExperiences son inválidos
            if (entities.SpecialtyID <= 0 || entities.YearofExperinces < 0)
            {
                result.Sucess = false;
                result.Message = "El ID de especialidad debe ser positivo y los años de experiencia no pueden ser negativos.";
                return result;
            }

            // Validar la tasa de consulta (si está presente)
            if (entities.ConsultacionFee.HasValue && entities.ConsultacionFee.Value <= 0)
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
            if (!string.IsNullOrEmpty(entities.ClinicAdress) && entities.ClinicAdress.Length < 5)
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
                Doctor? doctorRemove = await _dbContext.Doctors.FindAsync(entities.IDDoctor);

                if (doctorRemove != null)
                {

                    result.data = await base.Delete(doctorRemove);
                    result.Message = $" Doctor {entities.IDDoctor} Eliminado Exitosamente! ";
                    return result;
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


            if (string.IsNullOrEmpty(entities.Name) || string.IsNullOrEmpty(entities.LicenseNumber) || string.IsNullOrEmpty(entities.Education))
            {
                result.Sucess = false;
                result.Message = "El nombre, número de licencia y la educación son campos obligatorios.";
                return result;
            }

            // Validar si el SpecialtyID o YearofExperiences son inválidos
            if (entities.SpecialtyID <= 0 || entities.YearofExperinces < 0)
            {
                result.Sucess = false;
                result.Message = "El ID de especialidad debe ser positivo y los años de experiencia no pueden ser negativos.";
                return result;
            }

            // Validar la tasa de consulta (si está presente)
            if (entities.ConsultacionFee.HasValue && entities.ConsultacionFee.Value <= 0)
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
            if (!string.IsNullOrEmpty(entities.ClinicAdress) && entities.ClinicAdress.Length < 5)
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

                doctorUpdate.Name = entities.Name;
                doctorUpdate.IDDoctor = entities.IDDoctor;
                doctorUpdate.YearofExperinces = entities.YearofExperinces;
                doctorUpdate.SpecialtyID = entities.SpecialtyID;
                doctorUpdate.LicenseNumber = entities.LicenseNumber;
                doctorUpdate.ClinicAdress = entities.ClinicAdress;
                doctorUpdate.ConsultacionFee = entities.ConsultacionFee;
                doctorUpdate.LicenseExpirationDate = entities.LicenseExpirationDate;
                doctorUpdate.AvailabilityModeId = entities.AvailabilityModeId;
                doctorUpdate.Bio = entities.Bio;
                doctorUpdate.Education = entities.Education;


                result.data = await base.Update(doctorUpdate);
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
                var doctorsWithRoles = await (from doctor in _dbContext.Doctors
                                              join SystemRole in _dbContext.Roles on doctor.IDDoctor equals SystemRole.RoleId
                                              select new
                                              {
                                                  doctor.IDDoctor,
                                                  doctor.Name,
                                                  doctor.SpecialtyID,
                                                  doctor.LicenseNumber,
                                                  doctor.YearofExperinces,
                                                  doctor.Education,
                                                  doctor.Bio,
                                                  doctor.ConsultacionFee,
                                                  doctor.ClinicAdress,
                                                  doctor.AvailabilityModeId,
                                                  doctor.LicenseExpirationDate,
                                                  SystemRole.RoleName   // Nombre del rol del doctor
                                              }).ToListAsync();

                result.data = doctorsWithRoles; // Almacenamos la lista de doctores con sus roles en result.data

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
                                            join SystemRole in _dbContext.Roles on doctor.IDDoctor equals SystemRole.RoleId
                                            where doctor.IDDoctor == id
                                            && doctor.IsActive == true
                                            orderby doctor descending
                                            select new
                                            {
                                                doctor.IDDoctor,
                                                doctor.Name,
                                                doctor.SpecialtyID,
                                                doctor.LicenseNumber,
                                                doctor.YearofExperinces,
                                                doctor.Education,
                                                doctor.Bio,
                                                doctor.ConsultacionFee,
                                                doctor.ClinicAdress,
                                                doctor.AvailabilityModeId,
                                                doctor.IsActive,
                                                doctor.UpdateAt,
                                                doctor.CreateAt,
                                                doctor.LicenseExpirationDate,
                                                DoctorRole = SystemRole.RoleName   // Nombre del rol del doctor
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
