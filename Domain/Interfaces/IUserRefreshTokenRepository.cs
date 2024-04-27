using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRefreshTokenRepository
    {
        Task<Result<IEnumerable<UserRefreshToken>>> GetAllUserRefreshTokensAsync();
        Task<Result<UserRefreshToken>> GetUserRefreshTokenByIdAsync(int id);
        Task<Result<UserRefreshToken>> GetUserRefreshTokenByTokenAsync(string token);
        Task<Result<IEnumerable<UserRefreshToken>>> GetUserRefreshTokensByUserIdAsync(int userId);
        Task<Result> AddUserRefreshTokenAsync(UserRefreshToken userRefreshToken);
        Task<Result> UpdateUserRefreshTokenAsync(UserRefreshToken userRefreshToken);
        Task<Result> DeleteUserRefreshTokenAsync(int id);
        Task<Result> DeleteExpiredUserRefreshTokensAsync();
    }
}
