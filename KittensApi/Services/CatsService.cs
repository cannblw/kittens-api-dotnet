using System;
using System.Net.Http;
using System.Threading.Tasks;
using KittensApi.Config;

namespace KittensApi.Services
{
    public class CatsService : ICatsService
    {
        private readonly HttpClient _httpClient;

        public CatsService(
            HttpClient httpClient,
            AppSettings appSettings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(appSettings.CatsApi.BaseUrl);
        }

        public async Task<byte[]?> GetUpsideDownCat()
        {
            return await _httpClient.GetByteArrayAsync("cat");
        }
    }
}
