using Domain.Abstraction;
using Domain.Entities;

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
