using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces.Users
{
    public interface IUserRepository
    {
        Task<Result<IEnumerable<UserInfo>>> GetAllUsersAsync();
        Task<Result<UserInfo>> GetUserByIdAsync(int id);
        Task<Result<UserInfo>> GetUserByEmailAsync(string email);
        Task<Result> AddUserAsync(UserInfo user);
        Task<Result> UpdateUserAsync(UserInfo user);
        Task<Result> DeleteUserAsync(int id);

    }
}
