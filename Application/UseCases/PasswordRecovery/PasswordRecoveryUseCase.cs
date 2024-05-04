using Application.Abstraction.Messaging;
using Application.DTOs.Generals;
using Domain.Abstraction;
using Domain.Entities;
using Domain.Interfaces.Companies;
using Domain.Interfaces.Users;

namespace Application.UseCases.PasswordRecovery
{
    public class PasswordRecoveryUseCase : IPasswordRecoveryUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRecoveryTokenRepository _userRecoveryTokenRepository;
        private readonly ICompanyRecoveryTokenRepository _companyRecoveryTokenRepository;
        private readonly IEmailService _emailService;

        public PasswordRecoveryUseCase(
            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            IUserRecoveryTokenRepository userRecoveryTokenRepository,
            ICompanyRecoveryTokenRepository companyRecoveryTokenRepository,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _userRecoveryTokenRepository = userRecoveryTokenRepository;
            _companyRecoveryTokenRepository = companyRecoveryTokenRepository;
            _emailService = emailService;
        }

        public async Task<Result> SendRecoveryCodeAsync(GeneralPasswordRecoveryDto generalPasswordRecoveryDto)
        {
            var email = generalPasswordRecoveryDto.Email;

            var userResult = await _userRepository.GetUserByEmailAsync(email);
            if (userResult.IsSuccess)
            {
                var user = userResult.Value;
                var recoveryToken = GenerateRecoveryToken();
                var userRecoveryToken = new UserRecoveryToken
                {
                    UserId = user.Id,
                    Token = recoveryToken,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                    CreatedAt = DateTime.UtcNow,
                    IpAddress = generalPasswordRecoveryDto.IpAddress,
                    UserAgent = generalPasswordRecoveryDto.UserAgent
                };

                var addTokenResult = await _userRecoveryTokenRepository.AddUserRecoveryTokenAsync(userRecoveryToken);
                if (addTokenResult.IsFailure)
                {
                    return Result.Failure(addTokenResult.Error);
                }

                await SendRecoveryEmail(user.Email, recoveryToken);
                return Result.Success();
            }

            var companyResult = await _companyRepository.GetCompanyByEmailAsync(email);
            if (companyResult.IsSuccess)
            {
                var company = companyResult.Value;
                var recoveryToken = GenerateRecoveryToken();
                var companyRecoveryToken = new CompanyRecoveryToken
                {
                    CompanyId = company.Id,
                    Token = recoveryToken,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                    CreatedAt = DateTime.UtcNow,
                    IpAddress = generalPasswordRecoveryDto.IpAddress,
                    UserAgent = generalPasswordRecoveryDto.UserAgent
                };

                var addTokenResult = await _companyRecoveryTokenRepository.AddCompanyRecoveryTokenAsync(companyRecoveryToken);
                if (addTokenResult.IsFailure)
                {
                    return Result.Failure(addTokenResult.Error);
                }

                await SendRecoveryEmail(company.Email, recoveryToken);
                return Result.Success();
            }

            return Result.Failure(PasswordRecoveryErrors.EmailNotFound);
        }

        public async Task<Result> VerifyRecoveryCodeAsync(GeneralRecoveryCodeDto generalRecoveryCodeDto)
        {
            var email = generalRecoveryCodeDto.Email;
            var code = generalRecoveryCodeDto.RecoveryCode;

            var userResult = await _userRepository.GetUserByEmailAsync(email);
            if (userResult.IsSuccess)
            {
                var user = userResult.Value;
                var tokenResult = await _userRecoveryTokenRepository.GetUserRecoveryTokenByTokenAsync(code);
                if (tokenResult.IsFailure)
                {
                    return Result.Failure(PasswordRecoveryErrors.InvalidRecoveryCode);
                }

                var token = tokenResult.Value;
                if (token.UserId != user.Id || token.ExpiresAt < DateTime.UtcNow)
                {
                    return Result.Failure(PasswordRecoveryErrors.InvalidRecoveryCode);
                }

                await _userRecoveryTokenRepository.DeleteUserRecoveryTokenAsync(token.Id);
                return Result.Success();
            }

            var companyResult = await _companyRepository.GetCompanyByEmailAsync(email);
            if (companyResult.IsSuccess)
            {
                var company = companyResult.Value;
                var tokenResult = await _companyRecoveryTokenRepository.GetCompanyRecoveryTokenByTokenAsync(code);
                if (tokenResult.IsFailure)
                {
                    return Result.Failure(PasswordRecoveryErrors.InvalidRecoveryCode);
                }

                var token = tokenResult.Value;
                if (token.CompanyId != company.Id || token.ExpiresAt < DateTime.UtcNow)
                {
                    return Result.Failure(PasswordRecoveryErrors.InvalidRecoveryCode);
                }

                await _companyRecoveryTokenRepository.DeleteCompanyRecoveryTokenAsync(token.Id);
                return Result.Success();
            }

            return Result.Failure(PasswordRecoveryErrors.EmailNotFound);
        }

        private string GenerateRecoveryToken()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private async Task SendRecoveryEmail(string email, string recoveryToken)
        {
            var emailRequest = new EmailRequest
            {
                Name = "Password Recovery",
                Subject = "Recovery Code",
                To = email,
                Text = $"Your recovery code is: {recoveryToken}"
            };

            await _emailService.SendEmailAsync(emailRequest);
        }
    }

}
