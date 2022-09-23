using AuthenticationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApp.Context
{
    public class ApplicationContext: DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            Database.EnsureCreated();  
        }
    }
}
