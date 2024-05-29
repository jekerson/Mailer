using Application.Abstraction.Messaging;
using Domain.Interfaces.Sendings;
using Domain.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.EmailSending
{
    public class EmailPostSendingService : IEmailPostSendingService
    {
        private readonly IEmailService _emailService;
        private readonly ISendingRepository _sendingRepository;
        private readonly IUserSendingRepository _userSendingRepository;
        private readonly ISendingContentRepository _sendingContentRepository;
        private readonly IUserRepository _userRepository;

        public EmailPostSendingService(IEmailService emailService, ISendingRepository sendingRepository,
            IUserSendingRepository userSendingRepository, ISendingContentRepository sendingContentRepository, IUserRepository userRepository)
        {
            _emailService = emailService;
            _sendingRepository = sendingRepository;
            _userSendingRepository = userSendingRepository;
            _sendingContentRepository = sendingContentRepository;
            _userRepository = userRepository;
        }

        public async Task SendEmailsAsync()
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Today);
            var sendingContentsResult = await _sendingContentRepository.GetApprovedSendingContentsAsync(currentDate);
            if (sendingContentsResult.IsSuccess)
            {
                var sendingContents = sendingContentsResult.Value;
                foreach (var sendingContent in sendingContents)
                {
                    var sending = await _sendingRepository.GetSendingByIdAsync(sendingContent.SendingId);
                    var userSendings = await _userSendingRepository.GetUserSendingsBySendingIdAsync(sending.Value.Id);

                    foreach (var userSending in userSendings.Value)
                    {
                        var user = await _userRepository.GetUserByIdAsync(userSending.UserId);

                        var emailRequest = new EmailRequest
                        {
                            Name = sendingContent.Name,
                            Subject = sending.Value.Name,
                            To = user.Value.Email,
                            Text = sendingContent.Content
                        };

                        await _emailService.SendEmailAsync(emailRequest);
                        await Task.Delay(TimeSpan.FromSeconds(7));
                    }
                }
            }

        }
    }
}
