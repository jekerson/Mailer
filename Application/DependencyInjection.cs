using Application.UseCases.RefreshToken;
using Application.UseCases.SignIn;
using Application.UseCases.SignUp;
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
            services.AddScoped<ISignUpService, SignUpService>();
            services.AddScoped<ITokenRefreshService, TokenRefreshService>();
            services.AddScoped<ISignInService, SignInService>();

            services.AddAutoMapper(assembly);

            return services;
        }
    }
}
