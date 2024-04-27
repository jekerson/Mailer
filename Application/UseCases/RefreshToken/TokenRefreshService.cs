using Application.Abstraction.Messaging;
using Application.Validators.SignIn;
using Domain.Abstraction;
using Domain.Interfaces.Companies;
using Domain.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.RefreshToken
{
    public class TokenRefreshService: ITokenRefreshService
    {
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly ICompanyRefreshTokenRepository _companyRefreshTokenRepository;
        private readonly ISignInValidator _signInValidator;
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IJwtProvider _jwtProvider;

        public TokenRefreshService(
            IUserRefreshTokenRepository userRefreshTokenRepository,
            ISignInValidator signInValidator,
            ICompanyRefreshTokenRepository companyRefreshTokenRepository,
            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            IJwtProvider jwtProvider)
        {
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _signInValidator = signInValidator;
            _companyRefreshTokenRepository = companyRefreshTokenRepository;
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<AuthenticationResult> RefreshUserTokenAsync(string refreshToken)
        {
            var validationResult = _signInValidator.ValidateRefreshToken(refreshToken);
            if (validationResult.IsFailure)
            {
                return AuthenticationResult.Failure(validationResult.Error);
            }

            var userRefreshToken = await _userRefreshTokenRepository.GetUserRefreshTokenByTokenAsync(refreshToken);
            if (userRefreshToken.IsFailure)
            {
                return AuthenticationResult.Failure(TokenRefreshErrors.InvalidRefreshToken);
            }

            if (userRefreshToken.Value.ExpiresAt < DateTime.UtcNow)
            {
                return AuthenticationResult.Failure(TokenRefreshErrors.ExpiredRefreshToken);
            }

            var user = (await _userRepository.GetUserByIdAsync(userRefreshToken.Value.UserId)).Value;
            var token = _jwtProvider.GenerateUserToken(user);
            var newRefreshToken = _jwtProvider.GenerateRefreshToken();

            userRefreshToken.Value.Token = newRefreshToken;
            userRefreshToken.Value.ExpiresAt = DateTime.UtcNow.AddDays(7).ToLocalTime();

            await _userRefreshTokenRepository.UpdateUserRefreshTokenAsync(userRefreshToken.Value);

            return AuthenticationResult.Success(token, newRefreshToken);
        }

        public async Task<AuthenticationResult> RefreshCompanyTokenAsync(string refreshToken)
        {
            var validationResult = _signInValidator.ValidateRefreshToken(refreshToken);
            if (validationResult.IsFailure)
            {
                return AuthenticationResult.Failure(validationResult.Error);
            }

            var companyRefreshToken = await _companyRefreshTokenRepository.GetCompanyRefreshTokenByTokenAsync(refreshToken);
            if (companyRefreshToken.IsFailure)
            {
                return AuthenticationResult.Failure(TokenRefreshErrors.InvalidRefreshToken);
            }

            if (companyRefreshToken.Value.ExpiresAt < DateTime.UtcNow)
            {
                return AuthenticationResult.Failure(TokenRefreshErrors.ExpiredRefreshToken);
            }

            var company = (await _companyRepository.GetCompanyByIdAsync(companyRefreshToken.Value.CompanyId)).Value;
            var token = _jwtProvider.GenerateCompanyToken(company);
            var newRefreshToken = _jwtProvider.GenerateRefreshToken();

            companyRefreshToken.Value.Token = newRefreshToken;
            companyRefreshToken.Value.ExpiresAt = DateTime.UtcNow.AddDays(7).ToLocalTime();

            await _companyRefreshTokenRepository.UpdateCompanyRefreshTokenAsync(companyRefreshToken.Value);

            return AuthenticationResult.Success(token, newRefreshToken);
        }
    }
}
