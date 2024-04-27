using Domain.Abstraction;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CompanyRecoveryTokenRepository : ICompanyRecoveryTokenRepository
    {
        private readonly SenderDbContext _dbContext;

        public CompanyRecoveryTokenRepository(SenderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<CompanyRecoveryToken>>> GetAllCompanyRecoveryTokensAsync()
        {
            var recoveryTokens = await _dbContext.CompanyRecoveryTokens.AsNoTracking().ToListAsync();
            return Result<IEnumerable<CompanyRecoveryToken>>.Success(recoveryTokens);
        }

        public async Task<Result<CompanyRecoveryToken>> GetCompanyRecoveryTokenByIdAsync(int id)
        {
            var recoveryToken = await _dbContext.CompanyRecoveryTokens.FindAsync(id);
            if (recoveryToken == null)
                return Result<CompanyRecoveryToken>.Failure(CompanyRecoveryTokenErrors.CompanyRecoveryTokenNotFoundById(id));

            return Result<CompanyRecoveryToken>.Success(recoveryToken);
        }

        public async Task<Result<CompanyRecoveryToken>> GetCompanyRecoveryTokenByTokenAsync(string token)
        {
            var recoveryToken = await _dbContext.CompanyRecoveryTokens.FirstOrDefaultAsync(rt => rt.Token == token);
            if (recoveryToken == null)
                return Result<CompanyRecoveryToken>.Failure(CompanyRecoveryTokenErrors.CompanyRecoveryTokenNotFoundByToken(token));

            return Result<CompanyRecoveryToken>.Success(recoveryToken);
        }

        public async Task<Result<IEnumerable<CompanyRecoveryToken>>> GetCompanyRecoveryTokensByCompanyIdAsync(int companyId)
        {
            var recoveryTokens = await _dbContext.CompanyRecoveryTokens.Where(rt => rt.CompanyId == companyId).AsNoTracking().ToListAsync();
            return Result<IEnumerable<CompanyRecoveryToken>>.Success(recoveryTokens);
        }

        public async Task<Result> AddCompanyRecoveryTokenAsync(CompanyRecoveryToken companyRecoveryToken)
        {
            await _dbContext.CompanyRecoveryTokens.AddAsync(companyRecoveryToken);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateCompanyRecoveryTokenAsync(CompanyRecoveryToken companyRecoveryToken)
        {
            var existingRecoveryToken = await _dbContext.CompanyRecoveryTokens.FindAsync(companyRecoveryToken.Id);
            if (existingRecoveryToken == null)
                return Result.Failure(CompanyRecoveryTokenErrors.CompanyRecoveryTokenNotFoundById(companyRecoveryToken.Id));

            _dbContext.Entry(existingRecoveryToken).CurrentValues.SetValues(companyRecoveryToken);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteCompanyRecoveryTokenAsync(int id)
        {
            var recoveryToken = await _dbContext.CompanyRecoveryTokens.FindAsync(id);
            if (recoveryToken == null)
                return Result.Failure(CompanyRecoveryTokenErrors.CompanyRecoveryTokenNotFoundById(id));

            _dbContext.CompanyRecoveryTokens.Remove(recoveryToken);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteExpiredCompanyRecoveryTokensAsync()
        {
            var expiredTokens = _dbContext.CompanyRecoveryTokens
                .Where(rt => rt.ExpiresAt < DateTime.UtcNow);

            _dbContext.CompanyRecoveryTokens.RemoveRange(expiredTokens);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

    }
}
