using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserSendingRepository
    {
        Task<Result<IEnumerable<UserSending>>> GetAllUserSendingsAsync();
        Task<Result<UserSending>> GetUserSendingByIdAsync(int id);
        Task<Result<IEnumerable<UserSending>>> GetUserSendingsByUserIdAsync(int userId);
        Task<Result<IEnumerable<UserSending>>> GetUserSendingsBySendingIdAsync(int sendingId);
        Task<Result> AddUserSendingAsync(UserSending userSending);
        Task<Result> UpdateUserSendingAsync(UserSending userSending);
        Task<Result> DeleteUserSendingAsync(int id);
        Task<Result<UserSending>> GetUserSendingByUserIdAndSendingIdAsync(int userId, int sendingId);
    }
}
