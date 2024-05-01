using Domain.Abstraction;

namespace Application.UseCases.Users.Profile
{
    public static class UserProfileUseCaseErrors
    {
        public static Error InvalidPassword => Error.Validation(
            "Company.InvalidPassword",
            "The provided password is invalid.");
    }
}
