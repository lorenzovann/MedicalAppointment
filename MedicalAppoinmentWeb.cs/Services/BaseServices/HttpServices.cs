
using MedicalWeb.cs.Services.BaseServices;

namespace MedicalWeb.cs.Services.Base
{
    public class HttpServices : IHttpServices
    {
        private readonly HttpClient _httpClient;


        public HttpServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public virtual async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        public virtual async Task<T> PostAsync<T>(string url, T data)
        {
            var response = await _httpClient.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        public virtual async Task<T> PutAsync<T>(string url, T data)
        {
            var response = await _httpClient.PutAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        public virtual async Task DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }
}
