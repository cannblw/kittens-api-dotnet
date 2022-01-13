using System;
using System.Net.Http;
using System.Threading.Tasks;
using KittensApi.Adapters;
using KittensApi.Config;

namespace KittensApi.Services
{
    public class CatsService : ICatsService
    {
        private readonly HttpClient _httpClient;
        private readonly IImageProcessor _imageProcessor;

        public CatsService(
            HttpClient httpClient,
            AppSettings appSettings,
            IImageProcessor imageProcessor)
        {
            _httpClient = httpClient;
            _imageProcessor = imageProcessor;

            _httpClient.BaseAddress = new Uri(appSettings.CatsApi.BaseUrl);
        }

        public async Task<byte[]?> GetUpsideDownCat()
        {
            var catImage = await _httpClient.GetByteArrayAsync("cat");
            return await _imageProcessor.RotateImageUpsideDown(catImage);
        }
    }
}
