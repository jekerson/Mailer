using Application.DTOs.Sendings;
using Domain.Abstraction;
using Domain.Entities;
using Domain.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Subscriptions
{
    public class SendingSubscriptionUseCase : ISendingSubscriptionUseCase
    {
        private readonly IUserSendingRepository _userSendingRepository;

        public SendingSubscriptionUseCase(IUserSendingRepository userSendingRepository)
        {
            _userSendingRepository = userSendingRepository;
        }

        public async Task<Result> SubscribeToSendingAsync(SendingSubscriptionDto sendingSubscriptionDto)
        {
            var validation = ValidateSubscriptionDto(sendingSubscriptionDto);
            if (!validation.IsSuccess)
                return validation;

            var existingSubscription = await _userSendingRepository.GetUserSendingByUserIdAndSendingIdAsync(sendingSubscriptionDto.UserId, sendingSubscriptionDto.SendingId);
            if (existingSubscription.IsSuccess && existingSubscription.Value != null)
            {
                return Result.Failure(SendingSubscriptionErrors.AlreadySubscribed);
            }

            var userSending = new UserSending
            {
                UserId = sendingSubscriptionDto.UserId,
                SendingId = sendingSubscriptionDto.SendingId
            };

            return await _userSendingRepository.AddUserSendingAsync(userSending);
        }

        public async Task<Result> UnsubscribeFromSendingAsync(SendingSubscriptionDto sendingSubscriptionDto)
        {
            var validation = ValidateSubscriptionDto(sendingSubscriptionDto);
            if (!validation.IsSuccess)
                return validation;

            var existingSubscription = await _userSendingRepository.GetUserSendingByUserIdAndSendingIdAsync(sendingSubscriptionDto.UserId, sendingSubscriptionDto.SendingId);
            if (existingSubscription.IsFailure || existingSubscription.Value == null)
            {
                return Result.Failure(SendingSubscriptionErrors.NotSubscribed);
            }

            return await _userSendingRepository.DeleteUserSendingAsync(existingSubscription.Value.Id);
        }

        public async Task<bool> IsSubscribedAsync(SendingSubscriptionDto sendingSubscriptionDto)
        {
            var subscription = await _userSendingRepository.GetUserSendingByUserIdAndSendingIdAsync(sendingSubscriptionDto.UserId, sendingSubscriptionDto.SendingId);
            return subscription.IsSuccess && subscription.Value != null;
        }

        private static Result ValidateSubscriptionDto(SendingSubscriptionDto dto)
        {
            if (dto.UserId <= 0 || dto.SendingId <= 0)
            {
                return Result.Failure(SendingSubscriptionErrors.InvalidId);
            }
            return Result.Success();
        }
    }
}
