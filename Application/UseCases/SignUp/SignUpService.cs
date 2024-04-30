using Application.DTOs.Companies;
using Application.DTOs.Users;
using Application.Validators.SignUp;
using AutoMapper;
using Domain.Abstraction;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces.Companies;
using Domain.Interfaces.Files;
using Domain.Interfaces.Users;

namespace Application.UseCases.SignUp
{
    public class SignUpService : ISignUpService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ISignUpValidator _signUpValidator;
        private readonly IImageStorageRepository _imageStorageRepository;

        public SignUpService(IUserRepository userRepository, ICompanyRepository companyRepository, IMapper mapper, ISignUpValidator signUpValidator, IImageStorageRepository imageStorageRepository)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
            _signUpValidator = signUpValidator;
            _imageStorageRepository = imageStorageRepository;
        }

        public async Task<Result> UserSignUpAsync(UserSignUpDto userSignUpDto)
        {
            var validationResult = await _signUpValidator.ValidateUserAsync(userSignUpDto);
            if (validationResult.IsFailure)
            {
                return validationResult;
            }

            var user = _mapper.Map<UserInfo>(userSignUpDto);
            user.Salt = PasswordUtils.GenerateSalt(PasswordUtils.SALT_LENGTH);
            user.HashedPassword = PasswordUtils.HashPassword(userSignUpDto.Password, user.Salt);
            user.CreatedAt = DateTime.Now;

            var result = await _userRepository.AddUserAsync(user);
            return result;
        }

        public async Task<Result> CompanySignUpAsync(CompanyProfileDto companyProfileDto)
        {
            var validationResult = await _signUpValidator.ValidateCompanyAsync(companyProfileDto);
            if (validationResult.IsFailure)
            {
                return validationResult;
            }

            var company = _mapper.Map<Company>(companyProfileDto);
            company.Salt = PasswordUtils.GenerateSalt(PasswordUtils.SALT_LENGTH);
            company.HashedPassword = PasswordUtils.HashPassword(companyProfileDto.Password, company.Salt);
            company.CreatedAt = DateTime.Now;
            company.ImagePath = string.Empty;

            var addCompanyResult = await _companyRepository.AddCompanyAsync(company);
            if (addCompanyResult.IsFailure)
            {
                return addCompanyResult;
            }

            var logoSaveResult = await _imageStorageRepository.SaveCompanyLogoAsync(companyProfileDto.Logo, company.Id);
            if (logoSaveResult.IsFailure)
            {
                await _companyRepository.DeleteCompanyAsync(company.Id);
                return Result.Failure(logoSaveResult.Error);
            }

            // Update logo path
            company.ImagePath = logoSaveResult.Value;
            var updateCompanyResult = await _companyRepository.UpdateCompanyAsync(company);
            if (updateCompanyResult.IsFailure)
            {
                await _companyRepository.DeleteCompanyAsync(company.Id);
                return updateCompanyResult;
            }

            return Result.Success();
        }

    }
}
