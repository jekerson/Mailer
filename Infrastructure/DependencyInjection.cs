using Application.Abstraction.Messaging;
using Domain.Interfaces.Companies;
using Domain.Interfaces.Files;
using Domain.Interfaces.Reviews;
using Domain.Interfaces.Roles;
using Domain.Interfaces.Sendings;
using Domain.Interfaces.Users;
using Infrastructure.Authentication;
using Infrastructure.Data;
using Infrastructure.Repositories.Companies;
using Infrastructure.Repositories.Files;
using Infrastructure.Repositories.Reviews;
using Infrastructure.Repositories.Roles;
using Infrastructure.Repositories.Sendings;
using Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SenderDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("localdb")));

            services.AddMemoryCache();

            services.AddScoped<ICompanyRecoveryTokenRepository, CompanyRecoveryTokenRepository>();
            services.AddScoped<ICompanyRefreshTokenRepository, CompanyRefreshTokenRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyRoleRepository, CompanyRoleRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ISendingCategoryRepository, SendingCategoryRepository>();
            services.AddScoped<ISendingContentRepository, SendingContentRepository>();
            services.AddScoped<ISendingRepository, SendingRepository>();
            services.AddScoped<ISendingScoreRepository, SendingScoreRepository>();
            services.AddScoped<ISendingTimeRepository, SendingTimeRepository>();
            services.AddScoped<ISendingTypeRepository, SendingTypeRepository>();
            services.AddScoped<ISendingReviewRepository, SendingReviewRepository>();
            services.AddScoped<IUserRecoveryTokenRepository, UserRecoveryTokenRepository>();
            services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserSendingRepository, UserSendingRepository>();
            services.AddScoped<IImageStorageRepository, ImageStorageRepository>();
            services.AddScoped<IJwtProvider, JwtProvider>();

            return services;
        }
    }
}
