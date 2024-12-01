using Calzolari.Grpc.AspNetCore.Validation;

namespace Anis_UserManagment.Queries.GRPC.Validators.Main
{
    public static class ValidationContainer
    {
        public static IServiceCollection AddAppValidators(this IServiceCollection services)
        {
            services.AddGrpcValidation();
            services.AddScoped<GrpcValidator>();
            services.AddValidator<GetUsersRequestValidator>();



            return services;
        }
    }
}
