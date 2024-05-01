using Application.DTOs.Generals;
using Domain.Abstraction;

namespace Application.UseCases.SignIn
{
    public interface ISignInUseCase
    {
        Task<AuthenticationResult> UserSignInAsync(GeneralSignInDto generalSignInDto);
        Task<AuthenticationResult> CompanySignInAsync(GeneralSignInDto generalSignInDto);
    }

}
