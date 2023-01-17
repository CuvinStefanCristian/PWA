using Microsoft.AspNetCore.Components;
using PWA.Shared.DTOs;
using PWA.Utilities;

namespace PWA.Services
{
    public class CladiriService
    {
        private readonly IHttpService _httpService;

        public CladiriService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<CustomResponse<CladireDto>> InsertCladireAsync(CladireDto cladiriDto)
        {
            return await _httpService.PostAsync<CladireDto>("cladiri/insert", cladiriDto);
        }
    }
}
