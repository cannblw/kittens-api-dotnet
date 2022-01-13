using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using KittensApi.Config;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

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
            var catImage = await _httpClient.GetByteArrayAsync("cat");

            using (var outputStream = new MemoryStream())
            using (var image = Image.Load(catImage))
            {
                image.Mutate(x => x
                    .Rotate(RotateMode.Rotate180));
                
                await image.SaveAsync(outputStream, JpegFormat.Instance);
                return outputStream.ToArray();
            }
        }
    }
}
