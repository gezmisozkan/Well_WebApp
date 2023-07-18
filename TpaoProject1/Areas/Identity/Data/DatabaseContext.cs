using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TpaoProject1.Areas.Identity.Data;
using TpaoProject1.Model;

namespace TpaoProject1.Data;

public class DatabaseContext : IdentityDbContext<ApplicationUser>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }
    public DbSet<WellTop> WellTops { get; set; }

    public DbSet<Formation> Formation { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<WellTop>()
            .HasOne(ke => ke.User)
            .WithMany(u => u.WellTops)
            .HasForeignKey(ke => ke.UserId);
    
    }
}
