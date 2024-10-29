

namespace MedicalAppoiment.Aplication.cs.Base
{
    public interface IBaseServices<TResponse, TSaveDto, TUpdateDto>
    {

        Task<TResponse> SaveAsync(TSaveDto dto);
        Task<TResponse> UpdateAsync(TUpdateDto dto);
        Task<TResponse> getall();
        Task<TResponse> GetById(int id);

    }
}
