using System.Security.Claims;
using System.Threading.Tasks;
using KittensApi.Domain;

namespace KittensApi.Services
{
    public interface IAuthService
    {
        public Task<(User user, string token)> RegisterUser(string email, string userName, string password);
        
        public Task<(User user, string token)> Login(string userName, string password);
        
        public void RefreshJwtToken(string oldToken);

        public Task<User> GetUserByClaimsPrincipal(ClaimsPrincipal claimsPrincipal);
    }
}
