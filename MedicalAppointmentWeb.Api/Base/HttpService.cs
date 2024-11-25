using MedicalAppointmentWeb.Api.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http;

namespace MedicalAppointmentWeb.Api.Base
{
        public class HttpService : IHttpService
        {
            private readonly HttpClient _httpClient;
           
            public HttpService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

        public async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); 
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T> GetByIdAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T> PostAsync<T>(string url, T data)
        {
            var response = await _httpClient.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T> PutAsync<T>(string url, T data)
        {
            var response = await _httpClient.PutAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
    

    }
}
