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
    public class RoleRepositorie : BaseRepositorie<Role>, IRoleInterfaces
    {

        private readonly MedicalContext _context; 
        private readonly Logger<RoleRepositorie> _logger;
        public RoleRepositorie(MedicalContext context, 
                Logger<RoleRepositorie> _logger) : base(context)
        { 
            _context = context;
            this._logger = _logger; 

        }


        public override async Task<OperationResult> Add(Role entities)
        {
            OperationResult result = new OperationResult();

            // exepciones 

            if(entities.RoleId <= 0)
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


            if (await base.Exist(e => e.RoleId == entities.RoleId))
            {
                result.Sucess = false;
                result.Message = " id ya se encuentra registrado! ";
                return result;
             }


            try
            {     result.data = await base.Add(entities);
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

            if (entities.RoleId <= 0)
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
                 
                Role? roleremove = await _context.Roles.FindAsync(entities.RoleId);
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

            if (entities.RoleId <= 0)
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
                Role? RoleUpdate = await _context.Roles.FindAsync(entities.RoleId); 

                if (RoleUpdate == null)
                {
                    result.Sucess = false;
                    result.Message = " Role no encontrado! ";
                    return result;
                }

                RoleUpdate.RoleId = entities.RoleId;
                RoleUpdate.RoleName = entities.RoleName;
                RoleUpdate.UpdateAt = DateTime.Now;
                RoleUpdate.CreateAt = DateTime.Now;
                RoleUpdate.IsActive = true;


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
                var RoleList = await (from Role in _context.Roles
                                      select new
                                      {
                                          roleid = Role.RoleId,
                                          rolename = Role.RoleName,
                                          roleCreateAt = Role.CreateAt,
                                          roleUpdateAt = Role.UpdateAt,
                                          roleActive = Role.IsActive
                                      }).ToListAsync();

                result.data = RoleList;

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
                var FindValue = await (from Role in _context.Roles
                                       where
                                       Role.RoleId == id
                                       &&
                                       Role.IsActive == true
                                       orderby Role descending
                                       select new
                                       {
                                           Roleid = Role.RoleId,
                                           rolename = Role.RoleName,
                                           roleCreateAt = Role.CreateAt,
                                           roleUpdateAt = Role.UpdateAt,
                                       }).FirstOrDefaultAsync(); 

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
