using Domain.Abstraction;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICompanyRecoveryTokenRepository
    {
        Task<Result<IEnumerable<CompanyRecoveryToken>>> GetAllCompanyRecoveryTokensAsync();
        Task<Result<CompanyRecoveryToken>> GetCompanyRecoveryTokenByIdAsync(int id);
        Task<Result<CompanyRecoveryToken>> GetCompanyRecoveryTokenByTokenAsync(string token);
        Task<Result<IEnumerable<CompanyRecoveryToken>>> GetCompanyRecoveryTokensByCompanyIdAsync(int companyId);
        Task<Result> AddCompanyRecoveryTokenAsync(CompanyRecoveryToken companyRecoveryToken);
        Task<Result> UpdateCompanyRecoveryTokenAsync(CompanyRecoveryToken companyRecoveryToken);
        Task<Result> DeleteCompanyRecoveryTokenAsync(int id);
        Task<Result> DeleteExpiredCompanyRecoveryTokensAsync();
    }
}
