using Domain.Abstraction;

namespace Application.Validators.Sendings
{
    public static class SendingValidationErrors
    {
        public static Error EmptyName => Error.Validation(
            "SendingValidation.EmptyName",
            "Sending name cannot be empty.");
        public static Error NameTooLong => Error.Validation(
            "SendingValidation.NameTooLong",
            "Sending name cannot exceed 100 characters.");
        public static Error EmptyDescription => Error.Validation(
            "SendingValidation.EmptyDescription",
            "Sending description cannot be empty.");
        public static Error DescriptionTooLong => Error.Validation(
            "SendingValidation.DescriptionTooLong",
            "Sending description cannot exceed 500 characters.");
        public static Error EmptyLogo => Error.Validation(
            "SendingValidation.EmptyLogo",
            "Sending logo cannot be empty.");
    }
}
