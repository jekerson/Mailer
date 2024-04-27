using Domain.Abstraction;

namespace Domain.Errors
{
    public static class UserRecoveryTokenErrors
    {
        public static Error UserRecoveryTokenNotFoundById(int id) =>
            Error.NotFound("UserRecoveryToken.NotFoundById", $"User recovery token with ID {id} was not found.");

        public static Error UserRecoveryTokenNotFoundByToken(string token) =>
            Error.NotFound("UserRecoveryToken.NotFoundByToken", $"User recovery token with token {token} was not found.");
    }
}
