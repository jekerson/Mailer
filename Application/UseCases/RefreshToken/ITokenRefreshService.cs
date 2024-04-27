using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.RefreshToken
{
    public interface ITokenRefreshService
    {
        Task<AuthenticationResult> RefreshUserTokenAsync(string refreshToken);
        Task<AuthenticationResult> RefreshCompanyTokenAsync(string refreshToken);
    }

}
