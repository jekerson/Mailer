using Domain.Abstraction;

namespace Application.Services.RefreshToken
{
    public interface ITokenRefreshService
    {
        Task<AuthenticationResult> RefreshUserTokenAsync(string refreshToken);
        Task<AuthenticationResult> RefreshCompanyTokenAsync(string refreshToken);
    }

}
