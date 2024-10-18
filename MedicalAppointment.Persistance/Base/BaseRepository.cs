using MedicalAppointment.Domain.Repositories;
using MedicalAppointment.Domain.Result;
using MedicalAppointment.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net.Http.Metrics;

namespace MedicalAppointment.Persistance.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly MedicalAppointmentContext _MedicalAppointmentContext;
        private DbSet<TEntity> _entities;
        public BaseRepository(MedicalAppointmentContext medicalAppointmentContext)
        {
            _MedicalAppointmentContext = medicalAppointmentContext;
            this._entities = medicalAppointmentContext.Set<TEntity>();
        }

        public virtual async Task<bool> Exist(Expression<Func<TEntity, bool>> filter)
        {
            return await this._entities.AnyAsync(filter);
        }

        virtual public async Task<OperationResult> GetAll()
        {
            
            OperationResult result = new OperationResult();

            try
            {
                var datos = await this._entities.ToListAsync();
                result.Data = datos;    
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message =  $"Ocurrio un error {ex.Message} obteniendo los datos";

            }
            
            return result;

        }

        virtual public async Task<OperationResult> GetAll(Expression<Func<TEntity, bool>> filter)
        { 
            
            OperationResult result = new OperationResult();

            try
            {
                var datos = await this._entities.Where(filter).ToListAsync(); 
                result.Data = datos;
            }

            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrio un error {ex.Message} obteniendo los datos";
            }
            return result;
        
        }

        virtual public async  Task<OperationResult> GetEntityBy(int id)
        {
            
            OperationResult result = new OperationResult();

            try
            {
                var entity = await this._entities.FindAsync();
                result.Data = entity;

            }

            catch (Exception ex) 
            { 
                result.Success = false;
                result.Message = $"Ocurrio un error {ex.Message} obteniendo la entidad";
            
            }
            return result;
        }

        virtual public async Task<OperationResult> Delete(TEntity entity)
        {
          
            OperationResult result = new OperationResult();

            try 
            { 
                _entities.Remove(entity);
                await _MedicalAppointmentContext.SaveChangesAsync();    
            }

            catch (Exception ex) 
            {
                result.Success = false;
                result.Message = $"Ocurrio un error {ex.Message} tratando de eliminar los cambios";
            
            }
            return result;  
        }
        public async Task<OperationResult> Save(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try 
            { 
                _entities.Add(entity);
                await _MedicalAppointmentContext.SaveChangesAsync();
                   
            }

            catch (Exception ex) 
            {
                result.Success = false;
                result.Message = $"Ocurrio un error {ex.Message} tratando de guardar los cambios";
            
            }
            return result;  
        }

        public async Task<OperationResult> Update(TEntity entity)
        {
            OperationResult result = new OperationResult() ;

            try
            {
                _entities.Update(entity);
                await _MedicalAppointmentContext.SaveChangesAsync();
            }
            catch (Exception ex) 
            { 
                result.Success = false;
                result.Message = $"Ocurrio un error {ex.Message} tratando de actualizar los cambios";
            
            }
            return result;

        }
    }
}
