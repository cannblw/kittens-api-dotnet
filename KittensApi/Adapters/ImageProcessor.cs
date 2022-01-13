using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace KittensApi.Adapters
{
    public class ImageProcessor : IImageProcessor
    {
        public async Task<byte[]> RotateImageUpsideDown(byte[] imageBytes)
        {
            await using (var outputStream = new MemoryStream())
            using (var image = Image.Load(imageBytes))
            {
                image.Mutate(x => x
                    .Rotate(RotateMode.Rotate180));
                
                await image.SaveAsync(outputStream, JpegFormat.Instance);
                return outputStream.ToArray();
            }
        }
    }
}