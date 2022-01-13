using System.Threading.Tasks;
using KittensApi.Domain;

namespace KittensApi.Services
{
    public interface IUsersService
    {
        public Task<User?> GetById(long id);
    }
}