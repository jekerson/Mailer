using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.RefreshToken
{
    public static class TokenRefreshErrors
    {
        public static Error InvalidRefreshToken => Error.Validation(
            "SignIn.InvalidRefreshToken",
            "Invalid refresh token.");

        public static Error ExpiredRefreshToken => Error.Validation(
            "SignIn.ExpiredRefreshToken",
            "Refresh token has expired.");
    }
}
