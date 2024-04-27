using Domain.Abstraction;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRefreshTokenRepository : IUserRefreshTokenRepository
    {
        private readonly SenderDbContext _dbContext;

        public UserRefreshTokenRepository(SenderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<UserRefreshToken>>> GetAllUserRefreshTokensAsync()
        {
            var refreshTokens = await _dbContext.UserRefreshTokens.ToListAsync();
            return Result<IEnumerable<UserRefreshToken>>.Success(refreshTokens);
        }

        public async Task<Result<UserRefreshToken>> GetUserRefreshTokenByIdAsync(int id)
        {
            var refreshToken = await _dbContext.UserRefreshTokens.FindAsync(id);
            if (refreshToken == null)
                return Result<UserRefreshToken>.Failure(UserRefreshTokenErrors.UserRefreshTokenNotFoundById(id));

            return Result<UserRefreshToken>.Success(refreshToken);
        }

        public async Task<Result<UserRefreshToken>> GetUserRefreshTokenByTokenAsync(string token)
        {
            var refreshToken = await _dbContext.UserRefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
            if (refreshToken == null)
                return Result<UserRefreshToken>.Failure(UserRefreshTokenErrors.UserRefreshTokenNotFoundByToken(token));

            return Result<UserRefreshToken>.Success(refreshToken);
        }

        public async Task<Result<IEnumerable<UserRefreshToken>>> GetUserRefreshTokensByUserIdAsync(int userId)
        {
            var refreshTokens = await _dbContext.UserRefreshTokens.Where(rt => rt.UserId == userId).ToListAsync();
            return Result<IEnumerable<UserRefreshToken>>.Success(refreshTokens);
        }

        public async Task<Result> AddUserRefreshTokenAsync(UserRefreshToken userRefreshToken)
        {
            await _dbContext.UserRefreshTokens.AddAsync(userRefreshToken);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateUserRefreshTokenAsync(UserRefreshToken userRefreshToken)
        {
            var existingRefreshToken = await _dbContext.UserRefreshTokens.FindAsync(userRefreshToken.Id);
            if (existingRefreshToken == null)
                return Result.Failure(UserRefreshTokenErrors.UserRefreshTokenNotFoundById(userRefreshToken.Id));

            _dbContext.Entry(existingRefreshToken).CurrentValues.SetValues(userRefreshToken);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteUserRefreshTokenAsync(int id)
        {
            var refreshToken = await _dbContext.UserRefreshTokens.FindAsync(id);
            if (refreshToken == null)
                return Result.Failure(UserRefreshTokenErrors.UserRefreshTokenNotFoundById(id));

            _dbContext.UserRefreshTokens.Remove(refreshToken);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteExpiredUserRefreshTokensAsync()
        {
            var expiredTokens = _dbContext.UserRefreshTokens
                .Where(rt => rt.ExpiresAt < DateTime.UtcNow);

            _dbContext.UserRefreshTokens.RemoveRange(expiredTokens);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

    }
}
