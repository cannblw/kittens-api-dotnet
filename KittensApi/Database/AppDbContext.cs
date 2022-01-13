using Microsoft.EntityFrameworkCore;
using KittensApi.Domain;

namespace KittensApi.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
