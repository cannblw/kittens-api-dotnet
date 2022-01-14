using System.Threading.Tasks;
using KittensApi.Domain;

namespace KittensApi.Services
{
    public interface IAuthService
    {
        public Task<(User, string)> RegisterUser(string email, string userName, string password);
    }
}