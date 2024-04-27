using Domain.Abstraction;

namespace Domain.Errors
{
    public static class SendingScoreErrors
    {
        public static Error SendingScoreNotFoundById(int id) =>
            Error.NotFound("SendingScore.NotFoundById", $"Sending score with ID {id} was not found.");

        public static Error SendingScoreNotFoundBySendingId(int sendingId) =>
            Error.NotFound("SendingScore.NotFoundBySendingId", $"Sending score with Sending ID {sendingId} was not found.");

        public static Error SendingScoreAlreadyExistBySendingId(int sendingId) =>
            Error.Conflict("SendingScore.AlreadyExistBySendingId", $"Sending score with Sending ID {sendingId} already exists.");
    }
}
