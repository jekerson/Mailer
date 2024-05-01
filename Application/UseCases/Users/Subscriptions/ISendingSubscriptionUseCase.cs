using Application.DTOs.Sendings;
using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Subscriptions
{
    public interface ISendingSubscriptionUseCase
    {
        Task<Result> SubscribeToSendingAsync(SendingSubscriptionDto sendingSubscriptionDto);
        Task<Result> UnsubscribeFromSendingAsync(SendingSubscriptionDto sendingSubscriptionDto);
    }
}
