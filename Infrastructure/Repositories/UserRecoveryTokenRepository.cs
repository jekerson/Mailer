using Domain.Abstraction;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRecoveryTokenRepository : IUserRecoveryTokenRepository
    {
        private readonly SenderDbContext _dbContext;

        public UserRecoveryTokenRepository(SenderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<UserRecoveryToken>>> GetAllUserRecoveryTokensAsync()
        {
            var recoveryTokens = await _dbContext.UserRecoveryTokens.AsNoTracking().ToListAsync();
            return Result<IEnumerable<UserRecoveryToken>>.Success(recoveryTokens);
        }

        public async Task<Result<UserRecoveryToken>> GetUserRecoveryTokenByIdAsync(int id)
        {
            var recoveryToken = await _dbContext.UserRecoveryTokens.FindAsync(id);
            if (recoveryToken == null)
                return Result<UserRecoveryToken>.Failure(UserRecoveryTokenErrors.UserRecoveryTokenNotFoundById(id));

            return Result<UserRecoveryToken>.Success(recoveryToken);
        }

        public async Task<Result<UserRecoveryToken>> GetUserRecoveryTokenByTokenAsync(string token)
        {
            var recoveryToken = await _dbContext.UserRecoveryTokens.FirstOrDefaultAsync(rt => rt.Token == token);
            if (recoveryToken == null)
                return Result<UserRecoveryToken>.Failure(UserRecoveryTokenErrors.UserRecoveryTokenNotFoundByToken(token));

            return Result<UserRecoveryToken>.Success(recoveryToken);
        }

        public async Task<Result<IEnumerable<UserRecoveryToken>>> GetUserRecoveryTokensByUserIdAsync(int userId)
        {
            var recoveryTokens = await _dbContext.UserRecoveryTokens.Where(rt => rt.UserId == userId).AsNoTracking().ToListAsync();
            return Result<IEnumerable<UserRecoveryToken>>.Success(recoveryTokens);
        }

        public async Task<Result> AddUserRecoveryTokenAsync(UserRecoveryToken userRecoveryToken)
        {
            await _dbContext.UserRecoveryTokens.AddAsync(userRecoveryToken);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateUserRecoveryTokenAsync(UserRecoveryToken userRecoveryToken)
        {
            var existingRecoveryToken = await _dbContext.UserRecoveryTokens.FindAsync(userRecoveryToken.Id);
            if (existingRecoveryToken == null)
                return Result.Failure(UserRecoveryTokenErrors.UserRecoveryTokenNotFoundById(userRecoveryToken.Id));

            _dbContext.Entry(existingRecoveryToken).CurrentValues.SetValues(userRecoveryToken);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteUserRecoveryTokenAsync(int id)
        {
            var recoveryToken = await _dbContext.UserRecoveryTokens.FindAsync(id);
            if (recoveryToken == null)
                return Result.Failure(UserRecoveryTokenErrors.UserRecoveryTokenNotFoundById(id));

            _dbContext.UserRecoveryTokens.Remove(recoveryToken);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteExpiredUserRecoveryTokensAsync()
        {
            var expiredTokens = _dbContext.UserRecoveryTokens
                .Where(rt => rt.ExpiresAt < DateTime.UtcNow);

            _dbContext.UserRecoveryTokens.RemoveRange(expiredTokens);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
