using Domain.Abstraction;

namespace Application.UseCases.Companies.Profile
{
    public static class CompanyProfileUseCaseErrors
    {
        public static Error InvalidPassword => Error.Validation(
            "Company.InvalidPassword",
            "The provided password is invalid.");
    }
}
