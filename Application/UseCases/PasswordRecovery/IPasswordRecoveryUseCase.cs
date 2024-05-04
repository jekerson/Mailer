using Application.DTOs.Generals;
using Domain.Abstraction;

namespace Application.UseCases.PasswordRecovery
{
    public interface IPasswordRecoveryUseCase
    {
        Task<Result> SendRecoveryCodeAsync(GeneralPasswordRecoveryDto generalPasswordRecoveryDto);
        Task<Result> VerifyRecoveryCodeAsync(GeneralRecoveryCodeDto generalRecoveryCodeDto);
    }

}
