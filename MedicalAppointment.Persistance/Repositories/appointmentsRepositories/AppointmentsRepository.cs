using MedicalAppointment.Domain.Entities.appointments;
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


            if (entity.AppointmentDate == DateTime.MinValue)
            {
                operationResult.Success = false;
                operationResult.Message = "AppointmentDate es obligatorios.";
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
                _medicalAppoitmentContext.Appointments.Add(entity);
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

            if (entity.AppointmentID <= 0)
                return new OperationResult { Success = false, Message = "AppointmentID es obligatorio y debe ser mayor a 0." };

            if (entity.AppointmentDate == DateTime.MinValue)
                return new OperationResult { Success = false, Message = "AppointmentDate es obligatorio." };

            if (entity.DoctorID <= 0 || entity.PatientID <= 0 || entity.StatusID <= 0)
                return new OperationResult { Success = false, Message = "DoctorID, PatientID y StatusID deben ser positivos." };

            if (entity.AppointmentDate < DateTime.Now)
                return new OperationResult { Success = false, Message = "La fecha de la cita no puede ser en el pasado." };

            try
            {
                    
                Appointments? appointmentToUpdate = await _medicalAppoitmentContext.Appointments.FindAsync(entity.AppointmentID);
                    

                
                if (appointmentToUpdate == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Appointments no se actualizo";
                    return operationResult;
                }

                // Actualizar campos relevantes
                appointmentToUpdate.AppointmentID = entity.AppointmentID;
                appointmentToUpdate.PatientID = entity.PatientID;
                appointmentToUpdate.DoctorID = entity.DoctorID;
                appointmentToUpdate.AppointmentDate = entity.AppointmentDate;
                appointmentToUpdate.StatusID = entity.StatusID;

                // Guardar cambios
                _medicalAppoitmentContext.Appointments.Update(appointmentToUpdate);
                await _medicalAppoitmentContext.SaveChangesAsync();

                return new OperationResult
                {
                    Success = true,
                    Message = "Cita actualizada exitosamente.",
                    Data = appointmentToUpdate
                };
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = $"Error {ex.Message} actualizando Appointments";
                _logger.LogError(operationResult.Message , ex.ToString());
            }

            return operationResult;
        }






        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var appointments = await _medicalAppoitmentContext.Appointments.ToListAsync();

                operationResult.Data = appointments;
                operationResult.Success = true;
                operationResult.Message = "Appointments recuperadas con exito.";

            }

            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = $"Error {ex.Message} tratando de recuperar Appointments.";
                _logger.LogError(operationResult.Message, ex.ToString());

            }
            return operationResult;

        }





        public async override Task<OperationResult> GetEntityBy(int id)
        {
            var operationResult = new OperationResult();

            try
            {
                var appointment = await _medicalAppoitmentContext.Appointments
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.AppointmentID == id);

                if (appointment == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Appointment no encontrado.";
                    return operationResult;
                }

                operationResult.Success = true;
                operationResult.Data = appointment;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error al obtener el Appointment.";
                _logger.LogError(ex, operationResult.Message);
            }

            return operationResult;
        }



    }


}
