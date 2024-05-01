using Application.DTOs.Generals;
using Application.Validators.General;
using Domain.Abstraction;
using Domain.Helpers;
using Domain.Interfaces.Users;

namespace Application.UseCases.Users.Profile
{
    public class UserProfileUseCase : IUserProfileUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IGeneralInputValidator _generalInputValidator;

        public UserProfileUseCase(IUserRepository userRepository, IGeneralInputValidator generalInputValidator)
        {
            _userRepository = userRepository;
            _generalInputValidator = generalInputValidator;
        }

        public async Task<Result> ChangeUserEmail(GeneralChangeEmailDto generalChangeEmailDto)
        {
            var validationResult = await _generalInputValidator.ValidateEmailAsync(generalChangeEmailDto.NewEmail);
            if (validationResult.IsFailure)
            {
                return validationResult;
            }

            var userResult = await _userRepository.GetUserByEmailAsync(generalChangeEmailDto.OldEmail);
            if (userResult.IsFailure)
            {
                return userResult;
            }

            var user = userResult.Value;

            if (!PasswordUtils.VerifyPassword(generalChangeEmailDto.Password, user.HashedPassword, user.Salt))
            {
                return Result.Failure(UserProfileUseCaseErrors.InvalidPassword);
            }

            user.Email = generalChangeEmailDto.NewEmail;

            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<Result> DeleteUserAccount(GeneralSignInDto generalSignInDto)
        {
            var userResult = await _userRepository.GetUserByEmailAsync(generalSignInDto.Email);
            if (userResult.IsFailure)
            {
                return userResult;
            }
            var user = userResult.Value;

            if (!PasswordUtils.VerifyPassword(generalSignInDto.Password, user.HashedPassword, user.Salt))
            {
                return Result.Failure(UserProfileUseCaseErrors.InvalidPassword);
            }

            return await _userRepository.DeleteUserAsync(user.Id);

        }

        public async Task<Result> ResetUserPassword(GeneralResetPasswordDto generalResetPasswordDto)
        {
            var emailValidationResult = await _generalInputValidator.ValidateEmailAsync(generalResetPasswordDto.Email);
            if (emailValidationResult.IsFailure)
            {
                return emailValidationResult;
            }
            var passwordValidationResult = await _generalInputValidator.ValidatePasswordAsync(generalResetPasswordDto.Password);
            if (passwordValidationResult.IsFailure)
            {
                return passwordValidationResult;
            }
            var newPasswordValidationResult = await _generalInputValidator.ValidatePasswordAsync(generalResetPasswordDto.NewPassword);
            if (newPasswordValidationResult.IsFailure)
            {
                return newPasswordValidationResult;
            }
            var userResult = await _userRepository.GetUserByEmailAsync(generalResetPasswordDto.Email);
            if (userResult.IsFailure)
            {
                return userResult;
            }
            var user = userResult.Value;

            if (!PasswordUtils.VerifyPassword(generalResetPasswordDto.Password, user.HashedPassword, user.Salt))
            {
                return Result.Failure(UserProfileUseCaseErrors.InvalidPassword);
            }

            user.Salt = PasswordUtils.GenerateSalt(PasswordUtils.SALT_LENGTH);
            user.HashedPassword = PasswordUtils.HashPassword(generalResetPasswordDto.NewPassword, user.Salt);

            return await _userRepository.UpdateUserAsync(user);
        }
    }
}
