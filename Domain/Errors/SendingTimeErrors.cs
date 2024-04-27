using Domain.Abstraction;

namespace Domain.Errors
{
    public static class SendingTimeErrors
    {
        public static Error SendingTimeNotFoundById(int id) =>
            Error.NotFound("SendingTime.NotFoundById", $"Sending time with ID {id} was not found.");

        public static Error SendingTimeNotFoundBySendTime(TimeOnly sendTime) =>
            Error.NotFound("SendingTime.NotFoundBySendTime", $"Sending time with send time {sendTime} was not found.");

        public static Error SendingTimeAlreadyExistBySendTime(TimeOnly sendTime) =>
            Error.Conflict("SendingTime.AlreadyExistBySendTime", $"Sending time with send time {sendTime} already exists.");
    }
}
