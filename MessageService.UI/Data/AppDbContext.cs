using MessageService.UI.Models;
using Microsoft.EntityFrameworkCore;

namespace MessageService.UI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
