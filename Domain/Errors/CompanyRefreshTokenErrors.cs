using Domain.Abstraction;

namespace Domain.Errors
{
    public static class CompanyRefreshTokenErrors
    {
        public static Error CompanyRefreshTokenNotFoundById(int id) =>
            Error.NotFound("CompanyRefreshToken.NotFoundById", $"Company refresh token with ID {id} was not found.");

        public static Error CompanyRefreshTokenNotFoundByToken(string token) =>
            Error.NotFound("CompanyRefreshToken.NotFoundByToken", $"Company refresh token with token {token} was not found.");
    }
}
