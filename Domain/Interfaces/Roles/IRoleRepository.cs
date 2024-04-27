using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces.Roles
{
    public interface IRoleRepository
    {
        Task<Result<IEnumerable<Role>>> GetAllRolesAsync();
        Task<Result> AddRoleAsync(Role role);
        Task<Result<Role>> GetRoleByIdAsync(int id);
        Task<Result<Role>> GetRoleByNameAsync(string name);
        Task<Result> UpdateRoleAsync(Role role);
        Task<Result> DeleteRoleAsync(int id);
    }
}
