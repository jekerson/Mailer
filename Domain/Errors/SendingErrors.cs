using Domain.Abstraction;

namespace Domain.Errors
{
    public static class SendingErrors
    {
        public static Error SendingNotFoundById(int id) =>
            Error.NotFound("Sending.NotFoundById", $"Sending with ID {id} was not found.");

        public static Error SendingNotFoundByName(string name) =>
            Error.NotFound("Sending.NotFoundByName", $"Sending with name {name} was not found.");

        public static Error SendingAlreadyExistByName(string name) =>
            Error.Conflict("Sending.AlreadyExistByName", $"Sending with name {name} already exists.");
    }
}
