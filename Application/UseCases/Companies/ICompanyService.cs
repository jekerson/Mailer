using Application.DTOs.Companies;
using Application.DTOs.Generals;
using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Companies
{
    public interface ICompanyService
    {
        Task<Result> ResetCompanyPassword(GeneralResetPasswordDto generalResetPasswordDto);
        Task<Result> ChangeCompanyEmail(GeneralChangeEmailDto generalChangeEmailDto);
        Task<Result> DeleteCompanyAccount(string email, string password);
        Task<Result> UpdateCompanyData(CompanyUpdateDataDto companyUpdateDataDto);

    }
}
