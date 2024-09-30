using MedicalAppointment.Domain.Result;
using System;
using System.Collections.Generic;

using System.Linq.Expressions;


namespace MedicalAppointment.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<OperationResult> Save(TEntity entity);
        Task<OperationResult> Update (TEntity entity);
        Task<OperationResult> Delete (TEntity entity);
        Task<OperationResult> GetAll();
        Task<OperationResult> GetEntityBy(int id);
        Task<OperationResult> Exist(Expression<Func<TEntity, bool>> filter);
    }
}
