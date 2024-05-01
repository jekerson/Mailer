using Domain.Abstraction;

namespace Application.UseCases.Companies.Profile
{
    public static class CompanyProfileErrors
    {
        public static Error InvalidPassword => Error.Validation(
            "CompanyProfile.InvalidPassword",
            "The provided password is invalid.");
    }
}
