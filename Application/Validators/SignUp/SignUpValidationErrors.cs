using Domain.Abstraction;

namespace Application.Validators.Registration
{
    public static class SignUpValidationErrors
    {
        public static Error CompanyDescriptionRequired => Error.Validation(
            "SignUp.CompanyDescriptionRequired",
            "Company description is required.");

        public static Error CompanyDescriptionTooLong => Error.Validation(
            "SignUp.CompanyDescriptionTooLong",
            "The company description is too long. It must be at most 512 characters long.");

        public static Error CompanyLogoRequired => Error.Validation(
            "SignUp.CompanyLogoRequired",
            "Company logo is required.");
    }
}
