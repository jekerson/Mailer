using Domain.Abstraction;

namespace Domain.Errors
{
    public static class UserErrors
    {
        public static Error UserNotFoundById(int id) =>
            Error.NotFound("User.NotFoundById", $"User with ID {id} was not found.");

        public static Error UserNotFoundByEmail(string email) =>
            Error.NotFound("User.NotFoundByEmail", $"User with email {email} was not found.");

        public static Error UserAlreadyExist(string email) =>
            Error.Conflict("User.AlreadyExist", $"User with email {email} already exists.");
    }
}
