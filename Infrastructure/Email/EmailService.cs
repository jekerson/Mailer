using Application.Abstraction.Messaging;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly string _fromAddress;

        public EmailService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _apiKey = configuration["SmtpApi:ApiKey"];
            _baseUrl = configuration["SmtpApi:BaseUrl"];
            _fromAddress = configuration["SmtpApi:FromAddress"];
        }

        public async Task SendEmailAsync(EmailRequest request)
        {
            var url = $"{_baseUrl}/v1/smtp/send";
            var client = _clientFactory.CreateClient();

            var formData = new MultipartFormDataContent
        {
            { new StringContent(_fromAddress), "from" },
            { new StringContent(request.Name), "name" },
            { new StringContent(request.Subject), "subject" },
            { new StringContent(request.To), "to" },
            { new StringContent(request.Text), "text" }
        };

            client.DefaultRequestHeaders.Add("Authorization", _apiKey);

            var response = await client.PostAsync(url, formData);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to send email: {await response.Content.ReadAsStringAsync()}");
            }
        }
    }
}
