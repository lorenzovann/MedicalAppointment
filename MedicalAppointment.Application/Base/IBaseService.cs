

namespace MedicalAppointment.Application.Base
{
    public interface IBaseService<TResponse, TSaveDto, TUpdateDto>
    {
        Task<TResponse> SaveAsync(TSaveDto dto);
        Task<TResponse> UpdateAsync(TSaveDto dto);
        Task<TResponse> GetAll();
        Task<TResponse> GetById(int id);
        
    }
}
