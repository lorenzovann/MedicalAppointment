using Medical.Percistances.cs.Context;
using MedicalAppointment.Domain.IBaseRepositorie;
using MedicalAppointment.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Medical.Percistances.cs.Base
{
    public abstract class BaseContext<TEntities> : IBaseRepositorie<TEntities> where TEntities : class
    {
        // Implementación de los métodos de la interfaz IBaseRepositorie
        private readonly DbContext _context;
        public DbSet<TEntities> entities1;


        public BaseContext(DbContext context)
        {
            _context = context;
            this.entities1 = _context.Set<TEntities>();
        }

        public virtual async Task<OperationResult> Exist(Expression<Func<TEntities, bool>> filter)
        {
             OperationResult result = new OperationResult();

            try
            {

                var exist = await this.entities1.AnyAsync(filter);
                result.data = exist;
                result.Sucess = true;
                result.Message = "Consulta ejecutada exitasamante ";

            }
            catch (Exception ex)
            {
                result.Message = $"Ocurrio un error tipo {ex.Message} tratando de veificar si el registro existe! ";
                result.Sucess = false;

            }
            return result; 

        }

        public virtual Task<OperationResult> Add(TEntities entities)
        {
            OperationResult result = new OperationResult();

            try
            {
                  var valueadd = this.entities1.AddAsync(entities);
                  result.data = valueadd;
                  result.Sucess = true;
                  result.Message = " Consulta ejecutada Exitosamente!";
 
            }
            catch (Exception ex)
            {
                result.Sucess = false; 
                result.Message = $"Ocurrio un error tipo {ex.Message} tratando de agregar este articulo! ";
            }

            return Task.FromResult(result);

        }

        public virtual async Task<OperationResult> Delete(TEntities entities)
        {
            OperationResult result = new OperationResult();

            try
            {
                // Eliminar la entidad del contexto
                _context.Set<TEntities>().Remove(entities);

                // Confirmar los cambios en la base de datos
                await _context.SaveChangesAsync();

                result.Sucess = true;
                result.Message = "Artículo eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $"Ocurrió un error: {ex.Message} al intentar eliminar este artículo.";
            }

            return result;
        }

        public async Task<OperationResult> Getall()
        {
            OperationResult result = new OperationResult();
            try
            {
                var datos = await this.entities1.ToListAsync();  
                result.data = datos; 
                result.Sucess = true;
                result.Message = " Consulta ejecutada eexitosamente! ";
            }
            catch (Exception ex)
            {
                result.Sucess = false;
                result.Message = $" Error tipo {ex.Message} tratando de Listar Registro "; 

            }

            return result; 
        }

        public virtual Task<OperationResult> Update(TEntities entities)
        {
            throw new NotImplementedException();
        }

        public virtual Task<OperationResult> GetEntitiebyId(int id)
        {
            throw new NotImplementedException();
        }

    
    }
}
            