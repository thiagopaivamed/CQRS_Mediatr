using CQRS_Mediatr.Domain.Entities;
using CQRS_Mediatr.Domain.Interfaces;
using CQRS_Mediatr.Domain.Notifications;
using CQRS_Mediatr.Domain.Validation;
using CQRS_Mediatr.Infrastructure.Context;
using CQRS_Mediatr.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_Mediatr.CrossCutting
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConexaoBD"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddScoped<NotificationList>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IValidator<Customer>, CustomerValidation>();            

            return services;
        }
    }
}
