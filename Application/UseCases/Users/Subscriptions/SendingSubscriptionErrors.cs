using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Subscriptions
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
