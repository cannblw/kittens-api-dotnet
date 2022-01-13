using System.Threading.Tasks;
using KittensApi.Database;
using KittensApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace KittensApi.Services
{
    public class UsersService : IUsersService
    {
        private readonly AppDbContext _context;
        
        public UsersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetById(long id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
