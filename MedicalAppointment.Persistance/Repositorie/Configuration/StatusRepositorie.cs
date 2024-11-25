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
    public class StatusRepositorie : BaseRepositorie<Status>, IStatusRepository
    {

        private readonly MedicalContext _context;
        private readonly ILogger<StatusRepositorie> _logger; 
        public StatusRepositorie(MedicalContext context,
                    ILogger<StatusRepositorie> logger) : base(context)
        {
            _context = context;
            this._logger = logger;
        }


        public override async Task<OperationResult> Add(Status entities)
        {
            OperationResult result = new OperationResult();

        

            if (string.IsNullOrEmpty(entities.StatusName))
            {
                result.Sucess= false;
                result.Message = " No puedes dejar campos vasios! ";
                return result;

            }


            try
            {
                await base.Add(entities);
                result.data = entities;
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

   

        public override async Task<OperationResult> Update(Status entities)
        {
            OperationResult result = new OperationResult();

            try
            {
                Status? StatusUpdate = await _context.Status.FindAsync(entities.StatusID);
                if (StatusUpdate == null)
                {
                    result.Sucess = false;
                    result.Message = " Status no encontrado ";
                    return result;
                }

                StatusUpdate.StatusID = entities.StatusID; 
                StatusUpdate.StatusName = entities.StatusName;
                StatusUpdate.CreateAt = entities.CreateAt;  

                
                
                result.data = await base.Update(StatusUpdate);
                result.Message = "Status Modificado exitosamente! ";
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
                // Reemplaza la consulta anónima con una que devuelva una lista de Status.
                var ListValues = await _context.Status.AsNoTracking()
                     .OrderByDescending(x => x.CreateAt)
                     .ToListAsync();

                  

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
                
                     result.data = await _context.Status
                    .AsNoTracking()
                    .FirstOrDefaultAsync(status => status.StatusID == id); 

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
