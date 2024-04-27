using Application.DTOs;
using Application.Validators.General;
using Domain.Abstraction;

namespace Application.Validators.SignIn
{
    public class SignInValidator : ISignInValidator
    {
        private readonly IGeneralInputValidator _generalInputValidator;

        public SignInValidator(IGeneralInputValidator generalInputValidator)
        {
            _generalInputValidator = generalInputValidator;
        }

        public async Task<Result> ValidateUserSignInAsync(GeneralSignInDto userSignInDto)
        {
            var emailResult = await _generalInputValidator.ValidateEmailAsync(userSignInDto.Email);
            if (emailResult.IsFailure)
            {
                return emailResult;
            }

            var passwordResult = await _generalInputValidator.ValidatePasswordAsync(userSignInDto.Password);
            if (passwordResult.IsFailure)
            {
                return passwordResult;
            }

            return Result.Success();
        }

        public async Task<Result> ValidateCompanySignInAsync(GeneralSignInDto companySignInDto)
        {
            var emailResult = await _generalInputValidator.ValidateEmailAsync(companySignInDto.Email);
            if (emailResult.IsFailure)
            {
                return emailResult;
            }

            var passwordResult = await _generalInputValidator.ValidatePasswordAsync(companySignInDto.Password);
            if (passwordResult.IsFailure)
            {
                return passwordResult;
            }

            return Result.Success();
        }

        public Result ValidateRefreshToken(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return Result.Failure(SignInValidationErrors.RefreshTokenRequired);
            }

            return Result.Success();
        }
    }
}
