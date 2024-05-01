using Domain.Abstraction;

namespace Application.UseCases.Users.Profile
{
    public static class UserProfileErrors
    {
        public static Error InvalidPassword => Error.Validation(
            "UserProfile.InvalidPassword",
            "The provided password is invalid.");
    }
}
