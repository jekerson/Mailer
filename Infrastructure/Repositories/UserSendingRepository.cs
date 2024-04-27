using Domain.Abstraction;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserSendingRepository : IUserSendingRepository
    {
        private readonly SenderDbContext _dbContext;

        public UserSendingRepository(SenderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<UserSending>>> GetAllUserSendingsAsync()
        {
            var userSendings = await _dbContext.UserSendings.AsNoTracking().ToListAsync();
            return Result<IEnumerable<UserSending>>.Success(userSendings);
        }

        public async Task<Result<UserSending>> GetUserSendingByIdAsync(int id)
        {
            var userSending = await _dbContext.UserSendings.FindAsync(id);
            if (userSending == null)
                return Result<UserSending>.Failure(UserSendingErrors.UserSendingNotFoundById(id));

            return Result<UserSending>.Success(userSending);
        }

        public async Task<Result<IEnumerable<UserSending>>> GetUserSendingsByUserIdAsync(int userId)
        {
            var userSendings = await _dbContext.UserSendings.Where(us => us.UserId == userId).AsNoTracking().ToListAsync();
            return Result<IEnumerable<UserSending>>.Success(userSendings);
        }

        public async Task<Result<IEnumerable<UserSending>>> GetUserSendingsBySendingIdAsync(int sendingId)
        {
            var userSendings = await _dbContext.UserSendings.Where(us => us.SendingId == sendingId).AsNoTracking().ToListAsync();
            return Result<IEnumerable<UserSending>>.Success(userSendings);
        }

        public async Task<Result> AddUserSendingAsync(UserSending userSending)
        {
            if (await IsUserSendingExistAsync(userSending.UserId, userSending.SendingId))
                return Result.Failure(UserSendingErrors.UserSendingAlreadyExist(userSending.UserId, userSending.SendingId));

            await _dbContext.UserSendings.AddAsync(userSending);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateUserSendingAsync(UserSending userSending)
        {
            var existingUserSending = await _dbContext.UserSendings.FindAsync(userSending.Id);
            if (existingUserSending == null)
                return Result.Failure(UserSendingErrors.UserSendingNotFoundById(userSending.Id));

            _dbContext.Entry(existingUserSending).CurrentValues.SetValues(userSending);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteUserSendingAsync(int id)
        {
            var userSending = await _dbContext.UserSendings.FindAsync(id);
            if (userSending == null)
                return Result.Failure(UserSendingErrors.UserSendingNotFoundById(id));

            _dbContext.UserSendings.Remove(userSending);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<UserSending>> GetUserSendingByUserIdAndSendingIdAsync(int userId, int sendingId)
        {
            var userSending = await _dbContext.UserSendings
                .FirstOrDefaultAsync(us => us.UserId == userId && us.SendingId == sendingId);

            if (userSending == null)
            {
                return Result<UserSending>.Failure(UserSendingErrors.UserSendingNotFound);
            }

            return Result<UserSending>.Success(userSending);
        }

        private async Task<bool> IsUserSendingExistAsync(int userId, int sendingId)
        {
            return await _dbContext.UserSendings.AnyAsync(us => us.UserId == userId && us.SendingId == sendingId);
        }
    }
}
