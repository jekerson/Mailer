using Domain.Abstraction;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CompanyRefreshTokenRepository : ICompanyRefreshTokenRepository
    {
        private readonly SenderDbContext _dbContext;

        public CompanyRefreshTokenRepository(SenderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<CompanyRefreshToken>>> GetAllCompanyRefreshTokensAsync()
        {
            var refreshTokens = await _dbContext.CompanyRefreshTokens.AsNoTracking().ToListAsync();
            return Result<IEnumerable<CompanyRefreshToken>>.Success(refreshTokens);
        }

        public async Task<Result<CompanyRefreshToken>> GetCompanyRefreshTokenByIdAsync(int id)
        {
            var refreshToken = await _dbContext.CompanyRefreshTokens.FindAsync(id);
            if (refreshToken == null)
                return Result<CompanyRefreshToken>.Failure(CompanyRefreshTokenErrors.CompanyRefreshTokenNotFoundById(id));

            return Result<CompanyRefreshToken>.Success(refreshToken);
        }

        public async Task<Result<CompanyRefreshToken>> GetCompanyRefreshTokenByTokenAsync(string token)
        {
            var refreshToken = await _dbContext.CompanyRefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
            if (refreshToken == null)
                return Result<CompanyRefreshToken>.Failure(CompanyRefreshTokenErrors.CompanyRefreshTokenNotFoundByToken(token));

            return Result<CompanyRefreshToken>.Success(refreshToken);
        }

        public async Task<Result<IEnumerable<CompanyRefreshToken>>> GetCompanyRefreshTokensByCompanyIdAsync(int companyId)
        {
            var refreshTokens = await _dbContext.CompanyRefreshTokens.Where(rt => rt.CompanyId == companyId).AsNoTracking().ToListAsync();
            return Result<IEnumerable<CompanyRefreshToken>>.Success(refreshTokens);
        }

        public async Task<Result> AddCompanyRefreshTokenAsync(CompanyRefreshToken companyRefreshToken)
        {
            await _dbContext.CompanyRefreshTokens.AddAsync(companyRefreshToken);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateCompanyRefreshTokenAsync(CompanyRefreshToken companyRefreshToken)
        {
            var existingRefreshToken = await _dbContext.CompanyRefreshTokens.FindAsync(companyRefreshToken.Id);
            if (existingRefreshToken == null)
                return Result.Failure(CompanyRefreshTokenErrors.CompanyRefreshTokenNotFoundById(companyRefreshToken.Id));

            _dbContext.Entry(existingRefreshToken).CurrentValues.SetValues(companyRefreshToken);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteCompanyRefreshTokenAsync(int id)
        {
            var refreshToken = await _dbContext.CompanyRefreshTokens.FindAsync(id);
            if (refreshToken == null)
                return Result.Failure(CompanyRefreshTokenErrors.CompanyRefreshTokenNotFoundById(id));

            _dbContext.CompanyRefreshTokens.Remove(refreshToken);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteExpiredCompanyRefreshTokensAsync()
        {
            var expiredTokens = _dbContext.CompanyRefreshTokens
                .Where(rt => rt.ExpiresAt < DateTime.UtcNow);

            _dbContext.CompanyRefreshTokens.RemoveRange(expiredTokens);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

    }
}
