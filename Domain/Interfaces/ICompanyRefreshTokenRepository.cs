using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICompanyRefreshTokenRepository
    {
        Task<Result<IEnumerable<CompanyRefreshToken>>> GetAllCompanyRefreshTokensAsync();
        Task<Result<CompanyRefreshToken>> GetCompanyRefreshTokenByIdAsync(int id);
        Task<Result<CompanyRefreshToken>> GetCompanyRefreshTokenByTokenAsync(string token);
        Task<Result<IEnumerable<CompanyRefreshToken>>> GetCompanyRefreshTokensByCompanyIdAsync(int companyId);
        Task<Result> AddCompanyRefreshTokenAsync(CompanyRefreshToken companyRefreshToken);
        Task<Result> UpdateCompanyRefreshTokenAsync(CompanyRefreshToken companyRefreshToken);
        Task<Result> DeleteCompanyRefreshTokenAsync(int id);
        Task<Result> DeleteExpiredCompanyRefreshTokensAsync();
    }
}
