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
    public class RoleRepositorie : BaseRepositorie<Role>, IRoleRepository
    {

        private readonly MedicalContext _context; 
        private readonly ILogger<RoleRepositorie> _logger;
        public RoleRepositorie(MedicalContext context, 
                ILogger<RoleRepositorie> _logger) : base(context)
        { 
            _context = context;
            this._logger = _logger; 

        }


        public override async Task<OperationResult> Add(Role entities)
        {
            OperationResult result = new OperationResult();

            // exepciones 

        

            if (string.IsNullOrEmpty(entities.RoleName))
            {
                result.Sucess = false;
                result.Message = " No puedes dejar campos vasios! ";
                return result;
            }


            if (await base.Exist(e => e.RoleID == entities.RoleID))
            {
                result.Sucess = false;
                result.Message = " id ya se encuentra registrado! ";
                return result;
             }


            try
            {
                  await base.Add(entities);
                  result.data = entities;
                  result.Message = " Role agregado exitosamente! ";
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo {ex.Message} tratando de guardar el Role ";
                _logger.LogError(result.Message, ToString());
            }



            return result; 

        }

        public override async Task<OperationResult> Delete(Role entities)
        {
            OperationResult result = new OperationResult();


            if (string.IsNullOrEmpty(entities.RoleName))
            {
                result.Sucess = false;
                result.Message = " No puedes dejar campos vasios! ";
                return result;
            }

            try
            {
                 
                Role? roleremove = await _context.Roles.FindAsync(entities.RoleID);
                if (roleremove == null)
                {
                    result.Sucess = false;
                    result.Message = " id no encontrado! ";
                    return result;
                }

                result.data = await base.Delete(roleremove);
                result.Message = $" Role id {entities.RoleName} eliminado exitosamente!"; 
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo {ex.Message} tratando de Eliminar el Role ";
               _logger.LogError(result.Message, ToString());
            }

            return result;
        }

        public override async Task<OperationResult> Update(Role entities)
        {
            OperationResult result = new OperationResult();

            if (entities.RoleID <= 0)
            {
                result.Sucess = false;
                result.Message = "No puedes generar id menores e iguales a 0";
                return result;

            }

            if (string.IsNullOrEmpty(entities.RoleName))
            {
                result.Sucess = false;
                result.Message = " No puedes dejar campos vasios! ";
                return result;
            }

            try
            {
                Role? RoleUpdate = await _context.Roles.FindAsync(entities.RoleID); 

                if (RoleUpdate == null)
                {
                    result.Sucess = false;
                    result.Message = " Role no encontrado! ";
                    return result;
                }

                RoleUpdate.RoleID = entities.RoleID;
                RoleUpdate.RoleName = entities.RoleName;
                RoleUpdate.UpdatedAt = entities.UpdatedAt;
                RoleUpdate.CreatedAt = entities.CreatedAt;
                RoleUpdate.IsActive = entities.IsActive;


                result.data = await base.Update(RoleUpdate);
                result.Message = "Role modificado exitosamente! ";
        
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo {ex.Message} tratando de Modificar el Role ";
               _logger.LogError(result.Message, ToString());
            }

            return result; 
        }

        public override async Task<OperationResult> Getall()
        {
            OperationResult result = new OperationResult();

            try
            {


                result.data = await _context.Roles
               .AsNoTracking()
               .OrderByDescending(rol => rol.CreatedAt)
               .ToListAsync();

            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo {ex.Message} tratando de listar los Role ";
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
                result.Message = " No puedes generar id menores e iguales a 0";
                return result; 

            }


            try
            {
                // manejo con la  base de datos
                     var FindValue = await _context.Roles
                    .AsNoTracking()
                    .Where(role => role.RoleID == id && role.IsActive)
                    .OrderByDescending(rol => rol.CreatedAt)
                    .FirstOrDefaultAsync();
                  

                result.data = FindValue;

            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Error tipo {ex.Message} tratando de conseguir el id ";
               _logger.LogError(result.Message, ToString());
            }

            return result; 

        }


    }
}
