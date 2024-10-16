using Medical.Domain.Entities.Confi.Systems;
using Medical.Percistances.cs.Base;
using Medical.Percistances.cs.Context;
using MedicalAppointment.Domain.Result;
using MedicalAppointment.Persistance.Interfaces.Configuration.SystemIntefaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Persistance.Repositorie.Configuration
{
    public class StatusRepositorie : BaseRepositorie<Status>, IStatusInterfaces
    {

        private readonly MedicalContext _context;
        private readonly Logger<StatusRepositorie> _logger; 
        public StatusRepositorie(MedicalContext context,
                    Logger<StatusRepositorie> logger) : base(context)
        {
            _context = context;
            this._logger = logger;
        }


        public override async Task<OperationResult> Add(Status entities)
        {
            OperationResult result = new OperationResult();

            if(entities.StatusId <= 0)
            {
                result.Sucess = false;
                result.Message = "Error no puedes generar id menor o igual 0";
                return result; 

            }

            if (string.IsNullOrEmpty(entities.StatusName))
            {
                result.Sucess= false;
                result.Message = " No puedes dejar campos vasios! ";
                return result;

            }


            try
            {
                result.data = await base.Add(entities);
                result.Message = " Status agendado! ";
            }
            catch (Exception ex)
            {
                result.Sucess = false; 
                result.Message = $" Error tipo {ex.Message} tratando de guardar el Status ";
               _logger.LogError(result.Message, ToString());
            }

            return result; 

        }

        public override async Task<OperationResult> Delete(Status entities)
        { 

            OperationResult result = new OperationResult();

            if (entities.StatusId <= 0)
            {
                result.Sucess = false;
                result.Message = "Error no puedes generar id menor o igual 0";
                return result;
            }

            if (string.IsNullOrEmpty(entities.StatusName))
            {
                result.Sucess = false;
                result.Message = " No puedes dejar campos vasios! ";
                return result;

            }

            try
            {
                Status? statusRemove = await _context.Status.FindAsync(entities.StatusId);
                if (statusRemove == null)
                    {
                        result.Sucess = false;
                        result.Message = " Status no encontrado ";
                        return result;
                    }

                result.data = await base.Delete(entities);
                result.Message = $"Status {entities.StatusId} elimianado exitosamente! ";

            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $" Error tipo {ex.Message} tratando de elimianar el Status ";
               _logger.LogError(result.Message, ToString());
            }


            return result;
        }


        public override async Task<OperationResult> Update(Status entities)
        {
            OperationResult result = new OperationResult();

            try
            {
                Status? StatusUpdate = await _context.Status.FindAsync(entities.StatusId);
                if (StatusUpdate == null)
                {
                    result.Sucess = false;
                    result.Message = " Status no encontrado ";
                    return result;
                }

                StatusUpdate.StatusId = entities.StatusId;  
                StatusUpdate.StatusName = entities.StatusName;
                
                result.data = await base.Update(StatusUpdate);
                result.Message = "Status Modicado exitosamente! ";
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $" Error tipo {ex.Message} tratando de Modificar el Status ";
               _logger.LogError(result.Message, ToString());
            }

            return result;

        }

        public override async Task<OperationResult> Getall()
        {
            OperationResult result = new OperationResult();

            try
            {
                var ListValues = await (from Status in _context.Status
                                        select new
                                        {
                                            StatusName = Status.StatusName,
                                            StatuId = Status.StatusId,
                                        }).ToListAsync();  

                result.data = ListValues;
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $" Error tipo {ex.Message} tratando de listar los Status ";
                _logger.LogError(result.Message, ToString());
            }
            return result;
        }

        public override async Task<OperationResult> GetEntitiebyId(int id)
        { 
            OperationResult result = new OperationResult();

             if(id <= 0)
            {
                result.Sucess = false;
                result.Message = " Error no puedes ingresar id menores e iguales a 0! ";
                return result;
            }
            try
            {
                result.data = await (from Status in _context.Status
                                     where
                                     Status.StatusId == id
                                     select new
                                     {
                                       StatusName = Status.StatusName,
                                       StatusId = Status.StatusId,
                                     }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                 result.Sucess = false;
                 result.Message = $" Error tipo {ex.Message} tratando el id de los Status ";
                _logger.LogError(result.Message, ToString());
            }
            return result;
        }




    }
}
