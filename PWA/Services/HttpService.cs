using Blazored.LocalStorage;
using PWA.Utilities;

namespace PWA.Services
{
    public sealed class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public HttpService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public Task<CustomResponse<T>> DeleteAsync<T>(string uri)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponse<T>> GetAsync<T>(string uri)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponse<T>> PatchAsync<T>(string uri, object data)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponse<T>> PostAsync<T>(string uri, object data)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponse<T>> SendAsync<T>(HttpRequestMessage message)
        {
            throw new NotImplementedException();
        }
    }

    public interface IHttpService
    {
        Task<CustomResponse<T>> GetAsync<T>(string uri);
        Task<CustomResponse<T>> PostAsync<T>(string uri, object data);
        Task<CustomResponse<T>> PatchAsync<T>(string uri, object data);
        Task<CustomResponse<T>> DeleteAsync<T>(string uri);

        Task<CustomResponse<T>> SendAsync<T>(HttpRequestMessage message);
    }
}
