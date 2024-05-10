using System;
using Employment.Domain.Entities;
using Employment.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Employment.Services.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<IdentityEmploymentContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            services.AddDbContext<EmploymentContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            
            //        services.AddIdentity<User, IdentityRole>()
            //.AddEntityFrameworkStores<EmploymentContext>()
            //.AddDefaultTokenProviders();
        }
    }
}