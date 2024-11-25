namespace MedicalWeb.cs.Services.BaseServices
{
    public interface IHttpServices
    {
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(string url, T data);
        Task<T> PutAsync<T>(string url, T data);
        Task DeleteAsync(string url);
    }
}