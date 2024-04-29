using Application.DTOs.Generals;
using Domain.Abstraction;

namespace Application.Validators.SignIn
{
    public interface ISignInValidator
    {
        Task<Result> ValidateUserSignInAsync(GeneralSignInDto userSignInDto);
        Task<Result> ValidateCompanySignInAsync(GeneralSignInDto companySignInDto);
        Result ValidateRefreshToken(string refreshToken);
    }
}
