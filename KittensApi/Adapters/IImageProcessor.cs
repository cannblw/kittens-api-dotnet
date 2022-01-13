using System.Threading.Tasks;

namespace KittensApi.Adapters
{
    public interface IImageProcessor
    {
        Task<byte[]> RotateImageUpsideDown(byte[] imageBytes);
    }
}