﻿using Alta.EFCore.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Alta.EFCore.Repositories;
using Alta.Entities.Interfaces;

namespace Alta.EFCore
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddEF(this IServiceCollection services, IConfiguration configuration )
        {
            services.AddDbContext<AltaContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("AltaDbConnectionString")));

            services.AddScoped<IUnitOfWork, SQLServerUnitOfWork>();
            services.AddScoped<IUserLoginRepository, UserLoginRepository>();
            return services;
        }
    }
}
