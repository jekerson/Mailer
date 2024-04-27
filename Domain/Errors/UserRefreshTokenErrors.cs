using Domain.Abstraction;

namespace Domain.Errors
{
    public static class UserRefreshTokenErrors
    {
        public static Error UserRefreshTokenNotFoundById(int id) =>
            Error.NotFound("UserRefreshToken.NotFoundById", $"User refresh token with ID {id} was not found.");

        public static Error UserRefreshTokenNotFoundByToken(string token) =>
            Error.NotFound("UserRefreshToken.NotFoundByToken", $"User refresh token with token {token} was not found.");
    }
}
