using Application.UseCases.Companies;
using Application.UseCases.RefreshToken;
using Application.UseCases.SignIn;
using Application.UseCases.SignUp;
using Application.UseCases.Users;
using Application.Validators.General;
using Application.Validators.Reviews;
using Application.Validators.Sendings;
using Application.Validators.SignIn;
using Application.Validators.SignUp;
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
            services.AddScoped<ISendingValidator, SendingValidator>();
            services.AddScoped<IReviewValidator, ReviewValidator>();

            // Services
            services.AddScoped<ISignUpService, SignUpService>();
            services.AddScoped<ITokenRefreshService, TokenRefreshService>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<ICompanyService,  CompanyService>();
            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(assembly);

            return services;
        }
    }
}
