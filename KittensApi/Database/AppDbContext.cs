using Microsoft.EntityFrameworkCore;
using KittensApi.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KittensApi.Database
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
