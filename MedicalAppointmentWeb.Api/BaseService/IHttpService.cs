namespace MedicalAppointmentWeb.Api.Services
{
    public interface IHttpService
    {
        Task<T> GetAsync<T>(string url);
        Task<T> GetByIdAsync<T>(string url);
        Task<T> PostAsync<T>(string url, T data);
        Task<T> PutAsync<T>(string url, T data);
        

    }
}
