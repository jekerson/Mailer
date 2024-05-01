using Domain.Entities;

namespace Application.Abstraction.Messaging
{
    public interface IJwtProvider
    {
        string GenerateToken(string memberId, string email, string userType);
        string GenerateCompanyToken(Company company);
        string GenerateUserToken(UserInfo user);
        string GenerateRefreshToken();

    }
}
