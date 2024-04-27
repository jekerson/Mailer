using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
