using Domain.Abstraction;
using System.Text.RegularExpressions;

namespace Application.Validators.General
{
    public class GeneralInputValidator : IGeneralInputValidator
    {
        public async Task<Result> ValidateEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result.Failure(GeneralValidationErrors.EmailRequired);
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return Result.Failure(GeneralValidationErrors.InvalidEmail);
            }

            return Result.Success();
        }

        public async Task<Result> ValidatePasswordAsync(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return Result.Failure(GeneralValidationErrors.PasswordRequired);
            }

            if (password.Length < 8)
            {
                return Result.Failure(GeneralValidationErrors.PasswordTooShort);
            }

            if (password.Length > 100)
            {
                return Result.Failure(GeneralValidationErrors.PasswordTooLong);
            }

            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,100}$"))
            {
                return Result.Failure(GeneralValidationErrors.PasswordMissingRequiredCharacters);
            }

            return Result.Success();
        }

        public async Task<Result> ValidateNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure(GeneralValidationErrors.NameRequired);
            }

            if (name.Length > 100)
            {
                return Result.Failure(GeneralValidationErrors.NameTooLong);
            }

            return Result.Success();
        }
    }
}
