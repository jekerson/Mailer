using Application.DTOs.Companies;
using Application.DTOs.Generals;
using Application.Validators.General;
using Domain.Abstraction;
using Domain.Helpers;
using Domain.Interfaces.Companies;
using Domain.Interfaces.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Companies
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyRecoveryTokenRepository _companyRecoveryTokenRepository;
        private readonly IImageStorageRepository _imageStorageRepository;
        private readonly IGeneralInputValidator _generalInputValidator;

        public CompanyService(
            ICompanyRepository companyRepository,
            ICompanyRecoveryTokenRepository companyRecoveryTokenRepository,
            IImageStorageRepository imageStorageRepository,
            IGeneralInputValidator generalInputValidator)
        {
            _companyRepository = companyRepository;
            _companyRecoveryTokenRepository = companyRecoveryTokenRepository;
            _imageStorageRepository = imageStorageRepository;
            _generalInputValidator = generalInputValidator;
        }

        public async Task<Result> ResetCompanyPassword(GeneralResetPasswordDto generalResetPasswordDto)
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

            var companyResult = await _companyRepository.GetCompanyByEmailAsync(generalResetPasswordDto.Email);
            if (companyResult.IsFailure)
            {
                return companyResult;
            }

            var company = companyResult.Value;
            if (!PasswordUtils.VerifyPassword(generalResetPasswordDto.Password, company.HashedPassword, company.Salt))
            {
                return Result.Failure(CompanyServiceErrors.InvalidPassword);
            }

            company.Salt = PasswordUtils.GenerateSalt(PasswordUtils.SALT_LENGTH);
            company.HashedPassword = PasswordUtils.HashPassword(generalResetPasswordDto.NewPassword, company.Salt);

            return await _companyRepository.UpdateCompanyAsync(company);
        }

        public async Task<Result> ChangeCompanyEmail(GeneralChangeEmailDto generalChangeEmailDto)
        {
            var emailValidationResult = await _generalInputValidator.ValidateEmailAsync(generalChangeEmailDto.NewEmail);
            if (emailValidationResult.IsFailure)
            {
                return emailValidationResult;
            }

            var companyResult = await _companyRepository.GetCompanyByEmailAsync(generalChangeEmailDto.OldEmail);
            if (companyResult.IsFailure)
            {
                return companyResult;
            }

            var company = companyResult.Value;
            if (!PasswordUtils.VerifyPassword(generalChangeEmailDto.Password, company.HashedPassword, company.Salt))
            {
                return Result.Failure(CompanyServiceErrors.InvalidPassword);
            }

            company.Email = generalChangeEmailDto.NewEmail;

            return await _companyRepository.UpdateCompanyAsync(company);
        }

        public async Task<Result> DeleteCompanyAccount(string email, string password)
        {
            var companyResult = await _companyRepository.GetCompanyByEmailAsync(email);
            if (companyResult.IsFailure)
            {
                return companyResult;
            }

            var company = companyResult.Value;
            if (!PasswordUtils.VerifyPassword(password, company.HashedPassword, company.Salt))
            {
                return Result.Failure(CompanyServiceErrors.InvalidPassword);
            }

            return await _companyRepository.DeleteCompanyAsync(company.Id);
        }

        public async Task<Result> UpdateCompanyData(CompanyUpdateDataDto companyUpdateDataDto)
        {
            var emailValidationResult = await _generalInputValidator.ValidateEmailAsync(companyUpdateDataDto.Email);
            if (emailValidationResult.IsFailure)
            {
                return emailValidationResult;
            }

            var nameValidationResult = await _generalInputValidator.ValidateNameAsync(companyUpdateDataDto.Name);
            if (nameValidationResult.IsFailure)
            {
                return nameValidationResult;
            }

            var companyResult = await _companyRepository.GetCompanyByEmailAsync(companyUpdateDataDto.Email);
            if (companyResult.IsFailure)
            {
                return companyResult;
            }

            var company = companyResult.Value;
            company.Name = companyUpdateDataDto.Name;
            company.Description = companyUpdateDataDto.Description;

            if (companyUpdateDataDto.Logo != null)
            {
                var logoSaveResult = await _imageStorageRepository.SaveCompanyLogoAsync(companyUpdateDataDto.Logo, company.Id);
                if (logoSaveResult.IsFailure)
                {
                    return Result.Failure(logoSaveResult.Error);
                }

                company.ImagePath = logoSaveResult.Value;
            }

            return await _companyRepository.UpdateCompanyAsync(company);
        }
    }
}
