using Application.DTOs.Companies;
using Application.DTOs.Users;
using Domain.Abstraction;

namespace Application.Validators.Registration
{
    public interface ISignUpValidator
    {
        Task<Result> ValidateUserAsync(UserSignUpDto userSignUpDto);

        Task<Result> ValidateCompanyAsync(CompanySignUpDto companySignUpDto);
    }
}
