namespace Application.Abstraction.Messaging
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest request);

    }
}
