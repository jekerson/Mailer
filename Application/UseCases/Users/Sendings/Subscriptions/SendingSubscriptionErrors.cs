using Domain.Abstraction;

namespace Application.UseCases.Users.Sendings.Subscriptions
{
    public static class SendingSubscriptionErrors
    {
        public static Error AlreadySubscribed => Error.Conflict(
            "SendingSubscription.AlreadySubscribed",
            "User is already subscribed to this sending.");
        public static Error NotSubscribed => Error.NotFound(
            "SendingSubscription.NotSubscribed",
            "User is not subscribed to this sending.");
        public static Error InvalidId => Error.Validation(
            "SendingSubscription.InvalidId",
            "Invalid sending id or user id");
    }
}
