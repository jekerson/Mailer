using Domain.Abstraction;

namespace Application.Services.RefreshToken
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
