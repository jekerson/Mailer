using Application.Services.EmailSending;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BackgroundJobs
{
    public class SendingBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<SendingBackgroundService> _logger;

        public SendingBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<SendingBackgroundService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var currentTime = DateTime.Now;
                var scheduledTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 18, 0, 0);

                if (currentTime >= scheduledTime && currentTime < scheduledTime.AddMinutes(1))
                {
                    _logger.LogInformation("Starting email sending...");

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var sendingService = scope.ServiceProvider.GetRequiredService<IEmailPostSendingService>();
                        await sendingService.SendEmailsAsync();
                    }

                    _logger.LogInformation("Email sending completed.");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
