using anis_UserManagment.Queries.Application.Contracts.Repositories;
using anis_UserManagment.Queries.Infra.Persistance.Repositories;
using anis_UserManagment.Queries.Infra.Persistance;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Azure.Messaging.ServiceBus;
using anis_UserManagment.Queries.Infra.Services.ServiceBus;

namespace anis_UserManagment.Queries.Infra
{
    public static class InfraContainer
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DataBase")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton(s =>
            {
                return new ServiceBusClient(configuration.GetConnectionString("ServiceBus"));
            });

            services.AddHostedService<AccountUsersListener>();

            return services;
        }

    }
}
