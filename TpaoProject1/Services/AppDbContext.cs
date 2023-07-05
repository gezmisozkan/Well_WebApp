using Microsoft.EntityFrameworkCore;
using TpaoProject1.Model;

namespace TpaoProject1.Services
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Game> Game { get; set; }
    }
}
