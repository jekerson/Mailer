using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRecoveryTokenRepository
    {
        Task<Result<IEnumerable<UserRecoveryToken>>> GetAllUserRecoveryTokensAsync();
        Task<Result<UserRecoveryToken>> GetUserRecoveryTokenByIdAsync(int id);
        Task<Result<UserRecoveryToken>> GetUserRecoveryTokenByTokenAsync(string token);
        Task<Result<IEnumerable<UserRecoveryToken>>> GetUserRecoveryTokensByUserIdAsync(int userId);
        Task<Result> AddUserRecoveryTokenAsync(UserRecoveryToken userRecoveryToken);
        Task<Result> UpdateUserRecoveryTokenAsync(UserRecoveryToken userRecoveryToken);
        Task<Result> DeleteUserRecoveryTokenAsync(int id);
        Task<Result> DeleteExpiredUserRecoveryTokensAsync();
    }
}
