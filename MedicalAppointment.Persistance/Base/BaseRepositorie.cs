using Medical.Percistances.cs.Context;
using MedicalAppointment.Domain.IBaseRepositorie;
using MedicalAppointment.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Medical.Percistances.cs.Base
{
    public abstract class BaseRepositorie<TEntities> : IBaseRepositorie<TEntities> where TEntities : class
    {
        // Implementación de los métodos de la interfaz IBaseRepositorie
        private readonly DbContext _context;
        public DbSet<TEntities> entities1;


        public BaseRepositorie(DbContext context)
        {
            _context = context;
             this.entities1 = _context.Set<TEntities>();
        }


        public virtual async Task<bool> Exist(Expression<Func<TEntities, bool>> filter)
        {
            return await this.entities1.AnyAsync(filter);
        }

        public  virtual async Task<OperationResult> Add(TEntities entities)
        {
            OperationResult result = new OperationResult();

            try
            {
               var datos =  this.entities1.AddAsync(entities);
               // guardo los cambios en la base de datos
               await _context.SaveChangesAsync();
               result.data = datos; 
            }
            catch (Exception ex)
            {
                result.Sucess = false; 
                result.Message = $"Ocurrio un error tipo {ex.Message} tratando de agregar este articulo! ";
            }

            return result;

        }

        public virtual async Task<OperationResult> Delete(TEntities entities)
        {
            OperationResult result = new OperationResult();

            try
            {
                // Eliminar la entidad del contexto
                entities1.Remove(entities);
                
                // Confirmar los cambios en la base de datos
                await _context.SaveChangesAsync(); 


              
                result.Message = "Artículo eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Ocurrió un error: {ex.Message} al intentar eliminar este regitro.";
            }

            return result;
        }

        public virtual async Task<OperationResult> Getall()
        {
            OperationResult result = new OperationResult();
            try
            {
                var datos = await this.entities1.ToListAsync();  
                result.data = datos; 
                result.Message = " Consulta ejecutada exitosamente! ";
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $" Error tipo {ex.Message} tratando de Listar Registro "; 

            }

            return result; 
        }

        public virtual async Task<OperationResult> Update(TEntities entities)
        {
            OperationResult result = new OperationResult();

            try
            {  
                entities1.Update(entities);
                await _context.SaveChangesAsync();
                result.Message = "Modificacion ejecutada exitosamente!";
            }
            catch (Exception ex)
            {

                result.Sucess = false;
                result.Message = $" Error tipo {ex.Message} tratando de actualizar el registro ";

            }


            return result;

        }

        public virtual async Task<OperationResult> GetEntitiebyId(int id)
        {
            OperationResult result = new OperationResult();


            try
            {
                var datos = await this.entities1.FindAsync(id);
                result.data = datos;
                result.Message = " Consulta ejecutda correctamente! ";
            }
            catch (Exception ex)
            {

                result.Sucess = false;
                result.Message = $"Error  en la Operacion tipo {ex.Message} tratando de registrar id ";
            } 

            return result;
        }

    }
}
                                                                           