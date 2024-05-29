using Application.Services.EmailSending;
using Application.Services.RefreshToken;
using Application.UseCases.Companies.Profile;
using Application.UseCases.Sendings.Category;
using Application.UseCases.Sendings.Details;
using Application.UseCases.SignIn;
using Application.UseCases.SignUp;
using Application.UseCases.Users.Profile;
using Application.UseCases.Users.Sendings.Subscriptions;
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
            services.AddAutoMapper(assembly);

            //Validators
            services.AddScoped<IGeneralInputValidator, GeneralInputValidator>();
            services.AddScoped<ISignUpValidator, SignUpValidator>();
            services.AddScoped<ISignInValidator, SignInValidator>();
            services.AddScoped<ISendingValidator, SendingValidator>();
            services.AddScoped<IReviewValidator, ReviewValidator>();

            // Services

            services.AddScoped<ITokenRefreshService, TokenRefreshService>();
            services.AddScoped<IEmailPostSendingService, EmailPostSendingService>();

            // Use Cases
            services.AddScoped<ISignUpUseCase, SignUpUseCase>();
            services.AddScoped<ISignInUseCase, SignInUseCase>();
            services.AddScoped<ICompanyProfileUseCase, CompanyProfileUseCase>();
            services.AddScoped<IUserProfileUseCase, UserProfileUseCase>();
            services.AddScoped<ISendingSubscriptionUseCase, SendingSubscriptionUseCase>();
            services.AddScoped<ISendingCategoryUseCase, SendingCategoryUseCase>();
            services.AddScoped<ISendingDetailsUseCase, SendingDetailsUseCase>();

            return services;
        }
    }
}
