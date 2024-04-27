using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces.Companies
{
    public interface ICompanyRoleRepository
    {
        Task<Result<IEnumerable<CompanyRole>>> GetAllCompanyRolesAsync();
        Task<Result<IEnumerable<CompanyRole>>> GetCompanyRolesByCompanyIdAsync(int companyId);
        Task<Result<IEnumerable<CompanyRole>>> GetCompanyRolesByRoleIdAsync(int roleId);
        Task<Result> AddCompanyRoleAsync(CompanyRole companyRole);
        Task<Result> UpdateCompanyRoleAsync(CompanyRole companyRole);
        Task<Result> DeleteCompanyRoleAsync(int id);
    }
}
