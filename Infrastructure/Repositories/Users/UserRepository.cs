using Domain.Abstraction;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Users;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly SenderDbContext _dbContext;

        public UserRepository(SenderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<UserInfo>>> GetAllUsersAsync()
        {
            var users = await _dbContext.UserInfos.ToListAsync();
            return Result<IEnumerable<UserInfo>>.Success(users);
        }

        public async Task<Result<UserInfo>> GetUserByIdAsync(int id)
        {
            var user = await _dbContext.UserInfos.FindAsync(id);
            if (user == null)
                return Result<UserInfo>.Failure(UserErrors.UserNotFoundById(id));

            return Result<UserInfo>.Success(user);
        }

        public async Task<Result<UserInfo>> GetUserByEmailAsync(string email)
        {
            var user = await _dbContext.UserInfos.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return Result<UserInfo>.Failure(UserErrors.UserNotFoundByEmail(email));

            return Result<UserInfo>.Success(user);
        }

        public async Task<Result> AddUserAsync(UserInfo user)
        {
            if (await IsUserExistByEmailAsync(user.Email))
                return Result.Failure(UserErrors.UserAlreadyExist(user.Email));

            await _dbContext.UserInfos.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateUserAsync(UserInfo user)
        {
            var existingUser = await _dbContext.UserInfos.FindAsync(user.Id);
            if (existingUser == null)
                return Result.Failure(UserErrors.UserNotFoundById(user.Id));

            if (existingUser.Email != user.Email && await IsUserExistByEmailAsync(user.Email))
                return Result.Failure(UserErrors.UserAlreadyExist(user.Email));

            _dbContext.Entry(existingUser).CurrentValues.SetValues(user);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteUserAsync(int id)
        {
            var user = await _dbContext.UserInfos.FindAsync(id);
            if (user == null)
                return Result.Failure(UserErrors.UserNotFoundById(id));

            _dbContext.UserInfos.Remove(user);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        private async Task<bool> IsUserExistByEmailAsync(string email)
        {
            return await _dbContext.UserInfos.AnyAsync(u => u.Email == email);
        }
    }
}
