

using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Domain.Repositories;
using MedicalAppointment.Domain.Result;
using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace MedicalAppointment.Persistance.Repositories.appointmentsRepositories
{
    public class DoctorAvailabilityRepository : BaseRepository<DoctorAvailability>, IDoctorAvailabilityRepository

    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<DoctorAvailabilityRepository> _logger;

        public DoctorAvailabilityRepository(MedicalAppointmentContext context
            , ILogger<DoctorAvailabilityRepository> logger) : base(context)
        {
            _medicalAppointmentContext = context;
            _logger = logger;
        }

        
        public async override Task<OperationResult> Save(DoctorAvailability entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity.AvailabilityID == 0 || entity.AvailableDate == DateTime.MinValue)
            {
                operationResult.Success = false;
                operationResult.Message = "AvailabilityID y AvailableDate son obligatorios.";
                return operationResult;
            }


            if (entity.DoctorID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "DoctorID debe ser positivo.";
                return operationResult;
            }

            if (entity.StartTime < DateTime.Now.TimeOfDay || entity.EndTime < DateTime.Now.TimeOfDay)
            {
                operationResult.Success = false;
                operationResult.Message = "La hora de inicio y finalizacion no puede ser en el pasado.";
                return operationResult;
            }

            try
            {
                await base.Save(entity);
                operationResult.Data = entity;
                operationResult.Success = true;
                operationResult.Message = "DoctorAvailability guardado exitosamente.";

            }

            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = $"Error: {ex.Message} guardando DoctorAvailability.";
                _logger.LogError(operationResult.Message, ex);

            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(DoctorAvailability entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity.AvailabilityID == 0 || entity.AvailableDate == DateTime.MinValue)
            {
                operationResult.Success = false;
                operationResult.Message = "AvailabilityID y AvailableDate son obligatorios.";
                return operationResult;
            }


            if (entity.DoctorID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "DoctorID debe ser positivo.";
                return operationResult;
            }

            if (entity.StartTime < DateTime.Now.TimeOfDay || entity.EndTime < DateTime.Now.TimeOfDay)
            {
                operationResult.Success = false;
                operationResult.Message = "La hora de inicio y finalizacion no puede ser en el pasado.";
                return operationResult;
            }

            try
            {
                DoctorAvailability? doctorAvailabilityToUpdate = await _medicalAppointmentContext.DoctorAvailability.FindAsync(entity);

                if (doctorAvailabilityToUpdate == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "No se puede actualizar el registro.";
                    return operationResult;

                }

                doctorAvailabilityToUpdate.AvailabilityID = entity.AvailabilityID;
                doctorAvailabilityToUpdate.DoctorID = entity.DoctorID;
                doctorAvailabilityToUpdate.AvailableDate = entity.AvailableDate;
                doctorAvailabilityToUpdate.StartTime = entity.StartTime;
                doctorAvailabilityToUpdate.EndTime = entity.EndTime;

                operationResult = await base.Update(doctorAvailabilityToUpdate);
                operationResult.Data = doctorAvailabilityToUpdate;
                operationResult.Message = "DoctorAAvaailability actualizado exitosamente.";


            }

            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error actualizando DoctorAvailability.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var doctorAvailability = await _medicalAppointmentContext.DoctorAvailability.ToListAsync();

                operationResult.Data = doctorAvailability;
                operationResult.Success = true;
                operationResult.Message = "DoctorAvailability recuperadas con exito.";

            }

            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = $"Error {ex.Message} tratando de recuperar DoctorAvailability.";
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
                var availability = await (from da in _medicalAppointmentContext.DoctorAvailability
                                          where da.AvailabilityID == id && da.IsActive == true
                                          select new
                                          {
                                              da.AvailabilityID,
                                              da.DoctorID,
                                              da.AvailableDate,
                                              da.StartTime,
                                              da.EndTime,
                                              da.IsActive,
                                              da.CreatedAt,
                                              da.UpdatedAt,

                                          }).FirstOrDefaultAsync();

                if (availability == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "DoctorAvailability no encontrado.";
                    return operationResult;
                }

                operationResult.Success = true;
                operationResult.Data = availability;
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