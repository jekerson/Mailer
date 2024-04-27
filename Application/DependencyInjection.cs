using Application.Validators.General;
using Application.Validators.Registration;
using Application.Validators.SignIn;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            //Validators
            services.AddScoped<IGeneralInputValidator, GeneralInputValidator>();
            services.AddScoped<ISignUpValidator, SignUpValidator>();
            services.AddScoped<ISignInValidator, SignInValidator>();

            // Services

            services.AddAutoMapper(assembly);

            return services;
        }
    }
}
