using Application.DTOs.Companies;
using Application.DTOs.Generals;
using Domain.Abstraction;

namespace Application.UseCases.Companies.Profile
{
    public interface ICompanyProfileUseCase
    {
        Task<Result> ResetCompanyPassword(GeneralResetPasswordDto generalResetPasswordDto);
        Task<Result> ChangeCompanyEmail(GeneralChangeEmailDto generalChangeEmailDto);
        Task<Result> DeleteCompanyAccount(string email, string password);
        Task<Result> UpdateCompanyData(CompanyProfileDto companyProfileDto);

    }
}
