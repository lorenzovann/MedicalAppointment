using MedicalAppointment.Domain.Result;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;  

namespace MedicalAppointment.Domain.IBaseRepositorie
{
    public interface IBaseRepositorie<TEntities> where TEntities : class
    {
        // Métodos CRUD

        Task<OperationResult> Add(TEntities entities);
        Task<OperationResult> Delete(TEntities entities);
        Task<OperationResult> Update(TEntities entities);
        Task<OperationResult> GetEntitiebyId(int id);     
        Task<OperationResult> Getall();
        Task<bool> Exist(Expression<Func<TEntities, bool>> filter);  
    }
}
     