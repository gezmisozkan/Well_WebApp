using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TpaoProject1.Areas.Identity.Data;
using TpaoProject1.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DatabaseContextConnection") ?? throw new InvalidOperationException("Connection string 'DatabaseContextConnection' not found.");

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDefaultIdentity<ApplicationUser>().AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});
builder.Services.AddRazorPages();

//builder.Services.ConfigureDbContext(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();



app.Run();
