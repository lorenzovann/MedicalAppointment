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

            // Validaciones
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
                
                Appointments? appointmentsToUpdate = await _medicalAppoitmentContext.Appointments.FindAsync(entity.AppointmentID);

                if (appointmentsToUpdate == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "No se puede actualizar el registro.";
                    return operationResult;
                }

                appointmentsToUpdate.AppointmentID = entity.AppointmentID;
                appointmentsToUpdate.AppointmentDate = entity.AppointmentDate;
                appointmentsToUpdate.StatusID = entity.StatusID;
                appointmentsToUpdate.DoctorID = entity.DoctorID;
                appointmentsToUpdate.PatientID = entity.PatientID;

                
                OperationResult updateResult = await base.Update(appointmentsToUpdate);

                if (updateResult.Success)
                {
                    operationResult.Success = true;
                    operationResult.Data = appointmentsToUpdate;
                    operationResult.Message = "Appointments actualizado exitosamente.";
                }
                else
                {
                    operationResult.Success = false;
                    operationResult.Message = updateResult.Message ?? "Error en la actualización del Appointments.";
                }
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error actualizando Appointments.";
                _logger.LogError(operationResult.Message, ex.ToString());
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
            OperationResult operationResult = new OperationResult();

            if (id <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "ID no puede ser negativo ni 0!";
                return operationResult;
            }

            try
            {
                var appointments = await (from a in _medicalAppoitmentContext.Appointments
                                          where a.AppointmentID == id && a.IsActive == true
                                          select new
                                          {
                                              a.AppointmentID,
                                              a.PatientID,
                                              a.DoctorID,
                                              a.AppointmentDate,
                                              a.StatusID,
                                              a.CreatedAt,
                                              a.UpdatedAt,
                                              a.IsActive,

                                          }).FirstOrDefaultAsync();

                if (appointments == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Appointments no encontrado.";
                    return operationResult;
                }

                operationResult.Success = true;
                operationResult.Data = appointments;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = $"Ocurrió un error: {ex.Message}.";
                _logger.LogError(operationResult.Message, ex.ToString());

            }

            return operationResult;
        }




    }


}
