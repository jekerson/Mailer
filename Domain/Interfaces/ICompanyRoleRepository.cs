using Domain.Abstraction;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
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
