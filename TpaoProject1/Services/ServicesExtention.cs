using Microsoft.EntityFrameworkCore;
using System;

namespace TpaoProject1.Services
{
    public static class ServicesExtention
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("SqlCon"));
                option.EnableSensitiveDataLogging(true);
            }
    );
        }
    }
}