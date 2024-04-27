using Domain.Abstraction;

namespace Domain.Errors
{
    public static class UserSendingErrors
    {
        public static Error UserSendingNotFoundById(int id) =>
            Error.NotFound("UserSending.NotFoundById", $"User sending with ID {id} was not found.");

        public static Error UserSendingAlreadyExist(int userId, int sendingId) =>
            Error.Conflict("UserSending.AlreadyExist", $"User sending with User ID {userId} and Sending ID {sendingId} already exists.");
        public static Error UserSendingNotFound => Error.NotFound("UserSending.NotFound", "The specified user sending was not found.");
    }
}
