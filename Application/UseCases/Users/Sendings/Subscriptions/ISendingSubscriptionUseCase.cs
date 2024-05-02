using Application.DTOs.Sendings;
using Domain.Abstraction;

namespace Application.UseCases.Users.Sendings.Subscriptions
{
    public interface ISendingSubscriptionUseCase
    {
        Task<Result> SubscribeToSendingAsync(SendingSubscriptionDto sendingSubscriptionDto);
        Task<Result> UnsubscribeFromSendingAsync(SendingSubscriptionDto sendingSubscriptionDto);
    }
}
