using anis_UserManagment.Queries.Application;
using anis_UserManagment.Queries.Infra.Services.Logger;
using anis_UserManagment.Queries.Infra;
using Serilog;
using Anis_UserManagment.Queries.GRPC.ExceptionHandler;
using Anis_UserManagment.Queries.GRPC.Interceptors;
using Anis_UserManagment.Queries.GRPC.setup;
using Anis_UserManagment.Queries.GRPC.Validators.Main;
using Calzolari.Grpc.AspNetCore.Validation;
using anis_UserManagment.Queries.Infra.Persistance;
using Microsoft.EntityFrameworkCore;
using Anis_UserManagment.Queries.GRPC.Services;

namespace Anis_UserManagment.Queries.GRPC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = LoggerServicesBuilder.Build();

            builder.Services.AddApplicationServices();
            builder.Services.AddInfraServices(builder.Configuration);

            // Add services to the container.
            builder.Services.AddGrpc(option =>
            {
                option.Interceptors.Add<ThreadCultureInterceptor>();
                option.EnableMessageValidation();
                option.Interceptors.Add<ExceptionHandlingInterceptor>();
            });
            builder.Services.AddExternalServices(builder.Configuration);
            builder.Services.AddAppValidators();


            builder.Host.UseSerilog();
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            app.MapGrpcService<UsersQueriesService>();
            app.MapGrpcService<RebuildService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}