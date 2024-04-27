using Domain.Abstraction;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly SenderDbContext _dbContext;

        public UserRoleRepository(SenderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<UserRole>>> GetAllUserRolesAsync()
        {
            var userRoles = await _dbContext.UserRoles.AsNoTracking().ToListAsync();
            return Result<IEnumerable<UserRole>>.Success(userRoles);
        }

        public async Task<Result<IEnumerable<UserRole>>> GetUserRolesByUserIdAsync(int userId)
        {
            var userRoles = await _dbContext.UserRoles.Where(ur => ur.UserId == userId).AsNoTracking().ToListAsync();
            return Result<IEnumerable<UserRole>>.Success(userRoles);
        }

        public async Task<Result<IEnumerable<UserRole>>> GetUserRolesByRoleIdAsync(int roleId)
        {
            var userRoles = await _dbContext.UserRoles.Where(ur => ur.RoleId == roleId).ToListAsync();
            return Result<IEnumerable<UserRole>>.Success(userRoles);
        }

        public async Task<Result> AddUserRoleAsync(UserRole userRole)
        {
            if (await IsUserRoleExistAsync(userRole.UserId, userRole.RoleId))
                return Result.Failure(UserRoleErrors.UserRoleAlreadyExist(userRole.UserId, userRole.RoleId));

            await _dbContext.UserRoles.AddAsync(userRole);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateUserRoleAsync(UserRole userRole)
        {
            var existingUserRole = await _dbContext.UserRoles.FindAsync(userRole.Id);
            if (existingUserRole == null)
                return Result.Failure(UserRoleErrors.UserRoleNotFoundById(userRole.Id));

            _dbContext.Entry(existingUserRole).CurrentValues.SetValues(userRole);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteUserRoleAsync(int id)
        {
            var userRole = await _dbContext.UserRoles.FindAsync(id);
            if (userRole == null)
                return Result.Failure(UserRoleErrors.UserRoleNotFoundById(id));

            _dbContext.UserRoles.Remove(userRole);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        private async Task<bool> IsUserRoleExistAsync(int userId, int roleId)
        {
            return await _dbContext.UserRoles.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }
    }
}
