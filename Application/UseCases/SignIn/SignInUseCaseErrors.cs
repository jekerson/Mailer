using Domain.Abstraction;

namespace Application.UseCases.SignIn
{
    public static class SignInUseCaseErrors
    {
        public static Error InvalidCredentials => Error.Validation(
            "SignIn.InvalidCredentials",
            "Invalid email or password.");
    }
}
