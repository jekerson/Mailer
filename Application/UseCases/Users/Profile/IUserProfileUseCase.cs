using Application.DTOs.Generals;
using Domain.Abstraction;

namespace Application.UseCases.Users.Profile
{
    public interface IUserProfileUseCase
    {
        Task<Result> ResetUserPassword(GeneralResetPasswordDto generalResetPasswordDto);

        Task<Result> ChangeUserEmail(GeneralChangeEmailDto generalChangeEmailDto);

        Task<Result> DeleteUserAccount(GeneralSignInDto generalSignInDto);

    }
}
