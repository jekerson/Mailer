using Application.DTOs.Companies;
using Application.DTOs.Users;
using Application.Validators.General;
using Domain.Abstraction;

namespace Application.Validators.Registration
{
    public class SignUpValidator : ISignUpValidator
    {
        private readonly IGeneralInputValidator _generalInputValidator;

        public SignUpValidator(IGeneralInputValidator generalInputValidator)
        {
            _generalInputValidator = generalInputValidator;
        }

        public async Task<Result> ValidateUserAsync(UserSignUpDto userSignUpDto)
        {
            var nameResult = await _generalInputValidator.ValidateNameAsync(userSignUpDto.Name);
            if (nameResult.IsFailure)
            {
                return nameResult;
            }

            var emailResult = await _generalInputValidator.ValidateEmailAsync(userSignUpDto.Email);
            if (emailResult.IsFailure)
            {
                return emailResult;
            }

            var passwordResult = await _generalInputValidator.ValidatePasswordAsync(userSignUpDto.Password);
            if (passwordResult.IsFailure)
            {
                return passwordResult;
            }

            return Result.Success();
        }

        public async Task<Result> ValidateCompanyAsync(CompanySignUpDto companySignUpDto)
        {
            var nameResult = await _generalInputValidator.ValidateNameAsync(companySignUpDto.Name);
            if (nameResult.IsFailure)
            {
                return nameResult;
            }

            var emailResult = await _generalInputValidator.ValidateEmailAsync(companySignUpDto.Email);
            if (emailResult.IsFailure)
            {
                return emailResult;
            }

            var passwordResult = await _generalInputValidator.ValidatePasswordAsync(companySignUpDto.Password);
            if (passwordResult.IsFailure)
            {
                return passwordResult;
            }

            if (string.IsNullOrWhiteSpace(companySignUpDto.Description))
            {
                return Result.Failure(SignUpValidationErrors.CompanyDescriptionRequired);
            }

            if (companySignUpDto.Description.Length > 512)
            {
                return Result.Failure(SignUpValidationErrors.CompanyDescriptionTooLong);
            }

            if (companySignUpDto.Logo == null)
            {
                return Result.Failure(SignUpValidationErrors.CompanyLogoRequired);
            }

            return Result.Success();
        }
    }
}
