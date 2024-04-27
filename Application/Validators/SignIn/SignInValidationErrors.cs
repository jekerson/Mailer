using Domain.Abstraction;

namespace Application.Validators.SignIn
{
    public static class SignInValidationErrors
    {
        public static Error RefreshTokenRequired => Error.Validation(
            "SignIn.RefreshTokenRequired",
            "Refresh token is required.");
    }
}
