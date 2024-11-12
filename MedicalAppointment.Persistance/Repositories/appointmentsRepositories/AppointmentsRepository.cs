using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Domain.Repositories;
using MedicalAppointment.Domain.Result;
using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;



namespace MedicalAppointment.Persistance.Repositories.appointmentsRepositories
{
    public class AppointmentsRepository : BaseRepository<Appointments>, IAppointmentsRepository
    {


        private readonly MedicalAppointmentContext _medicalAppoitmentContext;
        private readonly ILogger<AppointmentsRepository> _logger;

        public AppointmentsRepository(MedicalAppointmentContext context,
               ILogger<AppointmentsRepository> logger) : base(context)
        {
            _medicalAppoitmentContext = context;
            _logger = logger;

        }



        public override async Task<OperationResult> Save(Appointments entity)
        {
            OperationResult operationResult = new OperationResult();


            if (entity.AppointmentID == 0 || entity.AppointmentDate == DateTime.MinValue)
            {
                operationResult.Success = false;
                operationResult.Message = "AppointmentID y AppointmentDate son obligatorios.";
                return operationResult;
            }


            if (entity.DoctorID <= 0 || entity.PatientID <= 0 || entity.StatusID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "DoctorID, PatientID y StatusID deben ser positivos.";
                return operationResult;
            }

            if (entity.AppointmentDate < DateTime.Now)
            {
                operationResult.Success = false;
                operationResult.Message = "La fecha de la cita no puede ser en el pasado.";
                return operationResult;
            }


            try
            {

                await base.Save(entity);

                operationResult.Data = entity;
                operationResult.Success = true;
                operationResult.Message = "Appointment agregado exitosamente!";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = $"Error: {ex.Message} tratando de guardar Appointment!";
                _logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

        public async override Task<OperationResult> Update(Appointments entity)
        {
            OperationResult operationResult = new OperationResult();


            if (entity.AppointmentID == 0 || entity.AppointmentDate == DateTime.MinValue)
            {
                operationResult.Success = false;
                operationResult.Message = "AppointmentID y AppointmentDate son obligatorios.";
                return operationResult;
            }

            if (entity.DoctorID <= 0 || entity.PatientID <= 0 || entity.StatusID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "DoctorID, PatientID y StatusID deben ser positivos.";
                return operationResult;
            }

            if (entity.AppointmentDate < DateTime.Now)
            {
                operationResult.Success = false;
                operationResult.Message = "La fecha de la cita no puede ser en el pasado.";
                return operationResult;
            }

            try
            {
                Appointments? appointmentsToUpdate = await _medicalAppoitmentContext.Appointments.FindAsync(entity.AppointmentID);

                if (appointmentsToUpdate == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "No se puede actualizar el registro.";
                    return operationResult;
                }
                appointmentsToUpdate.AppointmentDate = entity.AppointmentDate;
                appointmentsToUpdate.StatusID = entity.StatusID;
                appointmentsToUpdate.DoctorID = entity.DoctorID;
                appointmentsToUpdate.PatientID = entity.PatientID;

                operationResult = await base.Update(appointmentsToUpdate);
                operationResult.Data = appointmentsToUpdate;
                operationResult.Message = "Appointments actualizado exitosamente.";
            }

            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error actualizando Appointments.";
                _logger.LogError(operationResult.Message, ex);
            }

            return operationResult;

        }


        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var appointmentsWithDoctorAvailability = await (from appointments in _medicalAppoitmentContext.Appointments
                                                                join doctorAvailability in _medicalAppoitmentContext.DoctorAvailability
                                                                on appointments.DoctorID equals doctorAvailability.DoctorID
                                                                select new
                                                                {
                                                                    appointments.AppointmentID,
                                                                    appointments.PatientID,
                                                                    appointments.DoctorID,
                                                                    appointments.StatusID,
                                                                    appointments.AppointmentDate,
                                                                    appointments.CreatedAt,
                                                                    appointments.UpdatedAt,
                                                                    doctorAvailability.AvailableDate,
                                                                    doctorAvailability.StartTime,
                                                                    doctorAvailability.EndTime,
                                                                    doctorAvailability.IsActive
                                                                }).ToListAsync();

                operationResult.Data = appointmentsWithDoctorAvailability;
                operationResult.Success = true;
                operationResult.Message = "Appointments recuperados con éxito.";
            }

            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = $"Error {ex.Message} tratando de obtener Appointments.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }





        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new OperationResult();

            if (id <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "ID no puede ser negativo ni 0!";
                return operationResult;
            }

            try
            {
                var appointmentWithAvailability = await (from appointments in _medicalAppoitmentContext.Appointments
                                                         join availableAppointments in _medicalAppoitmentContext.DoctorAvailability
                                                         on appointments.DoctorID equals availableAppointments.DoctorID
                                                         where appointments.AppointmentID == id && appointments.IsActive
                                                         select new
                                                         {
                                                             appointments.AppointmentID,
                                                             appointments.PatientID,
                                                             appointments.DoctorID,
                                                             appointments.StatusID,
                                                             appointments.IsActive,
                                                             appointments.AppointmentDate,
                                                             appointments.CreatedAt,
                                                             appointments.UpdatedAt,
                                                             availableAppointments.AvailabilityID
                                                         }).FirstOrDefaultAsync();


                if (appointmentWithAvailability == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Disponibilidad de doctor no encontrado.";
                    return operationResult;
                }

                operationResult.Data = appointmentWithAvailability;
                operationResult.Success = true;
                operationResult.Message = "Appointments recuperado exitosamente.";

            }

            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = $"Ocurrió un error: {ex.Message}";
                _logger.LogError(operationResult.Message, ex.ToString());
            }


            return operationResult;
        }


    }


}
