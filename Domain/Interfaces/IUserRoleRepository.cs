using Domain.Abstraction;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
