using Application.DTOs.Companies;
using Application.DTOs.Users;
using Domain.Abstraction;

namespace Application.UseCases.SignUp
{
    public interface ISignUpService
    {
        Task<Result> UserSignUpAsync(UserSignUpDto userSignUpDto);

        Task<Result> CompanySignUpAsync(CompanySignUpDto companySignUpDto);
    }
}
