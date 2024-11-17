

namespace MedicalCoreAplications.cs.Base
{
    public interface IBaseServices<TResponse,  TSaveDto, TUpdateDto, TGetDto>
    {

        Task<TResponse> getall();
        Task<TResponse> SaveAsync(TSaveDto dto);    
        Task<TResponse> UpdateAsync(TUpdateDto dto);
        Task<TResponse> GetById(int id); 


    }
}
