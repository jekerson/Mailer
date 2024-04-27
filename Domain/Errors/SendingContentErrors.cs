using Domain.Abstraction;

namespace Domain.Errors
{
    public static class SendingContentErrors
    {
        public static Error SendingContentNotFoundById(int id) =>
            Error.NotFound("SendingContent.NotFoundById", $"Sending content with ID {id} was not found.");

        public static Error SendingContentNotFoundByName(string name) =>
            Error.NotFound("SendingContent.NotFoundByName", $"Sending content with name {name} was not found.");

        public static Error SendingContentAlreadyExistByName(string name) =>
            Error.Conflict("SendingContent.AlreadyExistByName", $"Sending content with name {name} already exists.");
    }
}
