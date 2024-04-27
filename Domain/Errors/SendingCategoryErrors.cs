using Domain.Abstraction;

namespace Domain.Errors
{
    public static class SendingCategoryErrors
    {
        public static Error SendingCategoryNotFoundById(int id) =>
            Error.NotFound("SendingCategory.NotFoundById", $"Sending category with ID {id} was not found.");

        public static Error SendingCategoryNotFoundByName(string name) =>
            Error.NotFound("SendingCategory.NotFoundByName", $"Sending category with name {name} was not found.");

        public static Error SendingCategoryAlreadyExistByName(string name) =>
            Error.Conflict("SendingCategory.AlreadyExistByName", $"Sending category with name {name} already exists.");
    }
}
