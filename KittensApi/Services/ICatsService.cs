using System.Threading.Tasks;

namespace KittensApi.Services
{
    public interface ICatsService
    {
        public Task<byte[]?> GetUpsideDownCat();
    }
}