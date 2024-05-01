using Application.Abstraction.Messaging;
using Application.DTOs.Generals;
using Application.Validators.SignIn;
using Domain.Abstraction;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces.Companies;
using Domain.Interfaces.Users;

namespace Application.UseCases.SignIn
{
    public class SignInUseCase : ISignInUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISignInValidator _signInValidator;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly ICompanyRefreshTokenRepository _companyRefreshTokenRepository;

        public SignInUseCase(
            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            ISignInValidator signInValidator,
            IJwtProvider jwtProvider,
            IUserRefreshTokenRepository userRefreshTokenRepository,
            ICompanyRefreshTokenRepository companyRefreshTokenRepository)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _signInValidator = signInValidator;
            _jwtProvider = jwtProvider;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _companyRefreshTokenRepository = companyRefreshTokenRepository;
        }
        public async Task<AuthenticationResult> UserSignInAsync(GeneralSignInDto generalSignInDto)
        {
            var validationResult = await _signInValidator.ValidateUserSignInAsync(generalSignInDto);
            if (validationResult.IsFailure)
            {
                return AuthenticationResult.Failure(validationResult.Error);
            }

            var user = (await _userRepository.GetUserByEmailAsync(generalSignInDto.Email)).Value;
            if (!PasswordUtils.VerifyPassword(generalSignInDto.Password, user.HashedPassword, user.Salt))
            {
                return AuthenticationResult.Failure(SignInUseCaseErrors.InvalidCredentials);
            }

            var token = _jwtProvider.GenerateUserToken(user);
            var refreshToken = _jwtProvider.GenerateRefreshToken();

            var userRefreshToken = new UserRefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7).ToLocalTime()
            };


            await _userRefreshTokenRepository.AddUserRefreshTokenAsync(userRefreshToken);

            return AuthenticationResult.Success(token, refreshToken);
        }

        public async Task<AuthenticationResult> CompanySignInAsync(GeneralSignInDto generalSignInDto)
        {
            var validationResult = await _signInValidator.ValidateCompanySignInAsync(generalSignInDto);
            if (validationResult.IsFailure)
            {
                return AuthenticationResult.Failure(validationResult.Error);
            }

            var company = (await _companyRepository.GetCompanyByEmailAsync(generalSignInDto.Email)).Value;
            if (!PasswordUtils.VerifyPassword(generalSignInDto.Password, company.HashedPassword, company.Salt))
            {
                return AuthenticationResult.Failure(SignInUseCaseErrors.InvalidCredentials);
            }

            var token = _jwtProvider.GenerateCompanyToken(company);
            var refreshToken = _jwtProvider.GenerateRefreshToken();

            var companyRefreshToken = new CompanyRefreshToken
            {
                Token = refreshToken,
                CompanyId = company.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7).ToLocalTime()
            };


            await _companyRefreshTokenRepository.AddCompanyRefreshTokenAsync(companyRefreshToken);

            return AuthenticationResult.Success(token, refreshToken);
        }
    }
}
