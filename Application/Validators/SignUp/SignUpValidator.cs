using Application.DTOs.Companies;
using Application.DTOs.Users;
using Application.Validators.General;
using Domain.Abstraction;

namespace Application.Validators.SignUp
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

        public async Task<Result> ValidateCompanyAsync(CompanyProfileDto companyProfileDto)
        {
            var nameResult = await _generalInputValidator.ValidateNameAsync(companyProfileDto.Name);
            if (nameResult.IsFailure)
            {
                return nameResult;
            }

            var emailResult = await _generalInputValidator.ValidateEmailAsync(companyProfileDto.Email);
            if (emailResult.IsFailure)
            {
                return emailResult;
            }

            var passwordResult = await _generalInputValidator.ValidatePasswordAsync(companyProfileDto.Password);
            if (passwordResult.IsFailure)
            {
                return passwordResult;
            }

            if (string.IsNullOrWhiteSpace(companyProfileDto.Description))
            {
                return Result.Failure(SignUpValidationErrors.CompanyDescriptionRequired);
            }

            if (companyProfileDto.Description.Length > 512)
            {
                return Result.Failure(SignUpValidationErrors.CompanyDescriptionTooLong);
            }

            if (companyProfileDto.Logo == null)
            {
                return Result.Failure(SignUpValidationErrors.CompanyLogoRequired);
            }

            return Result.Success();
        }
    }
}
