using Domain.Abstraction;

namespace Application.Validators.General
{
    public static class GeneralValidationErrors
    {
        public static Error EmailRequired => Error.Validation(
            "GeneralInputValidation.EmailRequired",
            "Email is required.");

        public static Error InvalidEmail => Error.Validation(
            "GeneralValidation.InvalidEmail",
            "The provided email is invalid.");

        public static Error PasswordRequired => Error.Validation(
            "GeneralInputValidation.PasswordRequired",
            "Password is required.");

        public static Error PasswordTooShort => Error.Validation(
            "GeneralInputValidation.PasswordTooShort",
            "The password is too short. It must be at least 8 characters long.");

        public static Error PasswordTooLong => Error.Validation(
            "GeneralInputValidation.PasswordTooLong",
            "The password is too long. It must be at most 100 characters long.");

        public static Error PasswordMissingRequiredCharacters => Error.Validation(
            "GeneralInputValidation.PasswordMissingRequiredCharacters",
            "The password must contain at least one uppercase letter, one lowercase letter, one digit, and one non-alphanumeric character.");

        public static Error NameRequired => Error.Validation(
            "GeneralInputValidation.NameRequired",
            "Name is required.");

        public static Error NameTooLong => Error.Validation(
            "GeneralInputValidation.NameTooLong",
            "The name is too long. It must be at most 100 characters long.");
    }
}
