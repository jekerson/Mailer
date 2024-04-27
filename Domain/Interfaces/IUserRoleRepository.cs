﻿using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<Result<IEnumerable<UserRole>>> GetAllUserRolesAsync();
        Task<Result<IEnumerable<UserRole>>> GetUserRolesByUserIdAsync(int userId);
        Task<Result<IEnumerable<UserRole>>> GetUserRolesByRoleIdAsync(int roleId);
        Task<Result> AddUserRoleAsync(UserRole userRole);
        Task<Result> UpdateUserRoleAsync(UserRole userRole);
        Task<Result> DeleteUserRoleAsync(int id);
    }
}
