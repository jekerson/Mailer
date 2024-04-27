using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return services;
        }
    }
}
