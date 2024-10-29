using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Domain.Repositories;
using MedicalAppointment.Domain.Result;
using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.appointments;
using MedicalAppointment.Persistance.Models.appointments.AppointmentsCRUD;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;


namespace MedicalAppointment.Persistance.Repositories.appointmentsRepositories
{
    public class AppointmentsRepository : BaseRepository<Appointments>, IAppointmentsRepository
    {


        private readonly MedicalAppointmentContext _medicalAppoitmentContext;
        private readonly ILogger<AppointmentsRepository> logger;

        public AppointmentsRepository(MedicalAppointmentContext context,
               ILogger<AppointmentsRepository> logger) : base(context)
        {
            _medicalAppoitmentContext = context;
            this.logger = logger;

        }


        //public async override Task<OperationResult> Delete(Appointments entity)
        //{
        //    OperationResult operationResult = new OperationResult();

        //    if (entity == null) 
        //    { 
        //        operationResult.Success = false;
        //        operationResult.Message = "La entidad es requerida";
        //        return operationResult;

        //    }

        //    if (entity.AppointmentID <= 0)
        //    {
        //        operationResult.Success = false;
        //        operationResult.Message = "Se requiere el AppointmentID para realizar esta operacion";
        //        return operationResult;

        //    }

        //    try
        //    {
        //        Appointments? appoitmentsToRemove = await _medicalAppoitmentContext.Appointments.FindAsync(entity.AppointmentID);   
        //        appoitmentsToRemove.IsActived = false;   
        //        appoitmentsToRemove.CreatedAt = entity.CreatedAt;
        //        appoitmentsToRemove.UpdatedAt = entity.UpdatedAt;

        //        await base.Update(appoitmentsToRemove);
        //    }

        //    catch (Exception ex) 
        //    {
        //        operationResult.Success = false;
        //        operationResult.Message = "Error desactivando Appointments";
        //        logger.LogError(operationResult.Message, ex.ToString());

        //    }

        //    return operationResult;
        //}



        public async override Task<OperationResult> Save(Appointments entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida.";
                return operationResult;
            }

            if (entity.PatientID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El PatientID no puede ser menor a cero.";
                return operationResult;
            }

            if (entity.DoctorID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El DoctorID no puede ser menor a cero.";
                return operationResult;
            }

            if (entity.StatusID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El StatusID no puede ser menir a cero.";
                return operationResult;
            }

            if (await base.Exist(appointments => appointments.DoctorID == entity.DoctorID
                                             && appointments.StatusID == entity.StatusID))
            {
                operationResult.Success = true;
                operationResult.Message = "El status se encuentra registrado";
                return operationResult;

            }

            try
            {
                operationResult = await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error guardando Appointments";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }


        public async override Task<OperationResult> Update(Appointments entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida.";
                return operationResult;
            }

            if (entity.AppointmentID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "AppoitmentsID no puede ser menor a cero.";
                return operationResult;
            }


            if (entity.PatientID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El PatientID no puede ser menor a cero.";
                return operationResult;
            }

            if (entity.DoctorID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El DoctorID no puede ser menor a cero.";
                return operationResult;
            }

            if (entity.StatusID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "El StatusID no puede ser menir a cero.";
                return operationResult;
            }

            try
            {
                Appointments? appoitnmentsToUpdate = await _medicalAppoitmentContext.Appointments.FindAsync(entity.AppointmentID);

                appoitnmentsToUpdate.AppointmentDate = entity.AppointmentDate;
                appoitnmentsToUpdate.StatusID = entity.StatusID;
                appoitnmentsToUpdate.DoctorID = entity.DoctorID;
                appoitnmentsToUpdate.PatientID = entity.PatientID;

                operationResult = await base.Update(appoitnmentsToUpdate);


            }

            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error actualizando Appointments";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;

        }


        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var appointmentsWithDoctorAvailability = await (from appointments in _medicalAppoitmentContext.Appointments
                                                                join availableAppointments in _medicalAppoitmentContext.DoctorAvailability on appointments.AppointmentID equals availableAppointments.AvailabilityID
                                                                select new AppointmentsBaseModel
                                                                {
                                                                   AppointmentID = appointments.AppointmentID,
                                                                   PatientID =appointments.PatientID,
                                                                   DoctorID = appointments.DoctorID,
                                                                   StatusID = appointments.StatusID,
                                                                   AppointmentDate = appointments.AppointmentDate,
                                                                   CreatedAt = appointments.CreatedAt,
                                                                   UpdatedAt = appointments.UpdatedAt,
                                                                   //availableAppointments.AvailabilityID



                                                                }).ToListAsync();

                return operationResult.Data = appointmentsWithDoctorAvailability;
            
            }

            catch (Exception ex)

            {
                operationResult.Success = false;
                operationResult.Message = $"Error {ex.Message} tratando de obtener Appointments.";
                logger.LogError(operationResult.Message, ex.ToString());
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
                var appointmentswithDoctorAvailability = await (from appointments in _medicalAppoitmentContext.Appointments
                                                                join availableAppointments in _medicalAppoitmentContext.DoctorAvailability on appointments.AppointmentID equals availableAppointments.AvailabilityID
                                                                where appointments.AppointmentID == id
                                                                && appointments.IsActive == true
                                                                orderby appointments descending
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

                if (appointmentswithDoctorAvailability == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Disponibilidad de doctor no encontrado.";
                    return operationResult;
                }
                operationResult.Data = appointmentswithDoctorAvailability;
            }


            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Appointments no encontrado.";
                return operationResult;

            }
            return operationResult;
        }

        public Task<OperationResult> GetAppointmentsByAppointmentId(int appointmentsId)
        {
            throw new NotImplementedException();
        }

    }


}
