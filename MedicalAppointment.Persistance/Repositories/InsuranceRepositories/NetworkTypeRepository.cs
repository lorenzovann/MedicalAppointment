

using MedicalAppointment.Domain.Entities.Insurance;
using MedicalAppointment.Domain.Result;
using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Persistance.Repositories.InsuranceRepositories
{
    public class NetworkTypeRepository : BaseRepository<NetworkType>, INetworkTypeRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<NetworkTypeRepository> _logger;

        public NetworkTypeRepository(MedicalAppointmentContext context,
                                 ILogger<NetworkTypeRepository> logger) : base(context)
        {
            _logger = logger;
            _medicalAppointmentContext = context;

        }



        public async override Task<OperationResult> Save(NetworkType entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity.NetworkTypeID == 0 || string.IsNullOrEmpty(entity.Name))
            {
                operationResult.Success = false;
                operationResult.Message = "Estos campos son obligatorios.";
                return operationResult;

            }

            if (entity.NetworkTypeID == 0)
            {
                operationResult.Success = false;
                operationResult.Message = "NetworkTypeID no puede ser menor a cero.";
                return operationResult;

            }

            try
            {
                await base.Save(entity);
                operationResult.Data = entity;
                operationResult.Success = true;
                operationResult.Message = "NetworkType guardado exitosamente.";

            }

            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = $"Error {ex.Message} tratando de guardar NetworkType.";
                _logger.LogError(operationResult.Message, ex.ToString());

            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(NetworkType entity)
        {
            OperationResult operationResult = new OperationResult();


            if (entity.NetworkTypeID == 0 || string.IsNullOrEmpty(entity.Name))
            {
                operationResult.Success = false;
                operationResult.Message = "NetworkTypeID y Name son campos obligatorios.";
                return operationResult;
            }

            try
            {

                NetworkType? networkTypeToUpdate = await _medicalAppointmentContext.NetworkType.FindAsync(entity);

                if (networkTypeToUpdate == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "NetworkType no encontrado.";
                    return operationResult;
                }

                networkTypeToUpdate.NetworkTypeID = entity.NetworkTypeID;
                networkTypeToUpdate.Name = entity.Name;
                networkTypeToUpdate.Description = entity.Description;
                networkTypeToUpdate.UpdatedAt = entity.UpdatedAt;



                operationResult = await base.Update(networkTypeToUpdate);
                operationResult.Data = networkTypeToUpdate;
                operationResult.Success = true;
                operationResult.Message = "NetworkType actualizado exitosamente.";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = $"Error {ex.Message} tratando de actualizar NetworkType.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }


        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var networkType = await _medicalAppointmentContext.NetworkType.ToListAsync();

                operationResult.Data = networkType;
                operationResult.Success = true;
                operationResult.Message = " NetworkType recuperadas con exito.";

            }

            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = $"Error {ex.Message} tratando de recuperar NetworkType.";
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
                operationResult.Message = "El id no puede ser menor a cero.";
                return operationResult;

            }

            try
            {
                var networkType = await (from nt in _medicalAppointmentContext.NetworkType
                                         where nt.NetworkTypeID == id && nt.IsActive == true
                                         select new
                                         {
                                             nt.NetworkTypeID,
                                             nt.Name,
                                             nt.Description,
                                             nt.CreatedAt,
                                             nt.UpdatedAt,
                                             nt.IsActive,


                                         }).FirstOrDefaultAsync();

                if (networkType == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Networktype no encontrado.";
                    return operationResult;

                }

            }

            catch (Exception ex)
            {
                operationResult.Success= false;
                operationResult.Message = $"Ocurrio un error {ex.Message}.";
                _logger.LogError(operationResult.Message , ex.ToString());
            }
            return operationResult;
        }
    }
}
