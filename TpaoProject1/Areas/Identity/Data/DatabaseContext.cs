using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TpaoProject1.Areas.Identity.Data;
using TpaoProject1.Model;
using TpaoProject1.Models;

namespace TpaoProject1.Data;

public class DatabaseContext : IdentityDbContext<ApplicationUser>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }
    public DbSet<WellTop> WellTops { get; set; }

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

        var appUser = new ApplicationUser
        {
            Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
            Email = "admin@admin.com",
            EmailConfirmed = true,
            FirstName = "admin",
            LastName = "admin",
            UserName = "admin@admin.com",
            NormalizedUserName = "ADMIN@ADMIN.COM"
        };

        PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
        appUser.PasswordHash = ph.HashPassword(appUser, "Admin_1");

        builder.Entity<ApplicationUser>().HasData(appUser);



        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "Admin",
            NormalizedName = "ADMIN",
            Id = "341743f0-asd2–42de-afbf-59kmkkmk72cf6",
            ConcurrencyStamp = "341743f0-asd2–42de-afbf-59kmkkmk72cf6"
        });

        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "User",
            NormalizedName = "USER",
            Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
            ConcurrencyStamp = "02174cf0–9412–4cfe-afbf-59f706d72cf6"
        });

        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = "341743f0-asd2–42de-afbf-59kmkkmk72cf6",
            UserId = "02174cf0–9412–4cfe-afbf-59f706d72cf6"
        });
    }

}
   
