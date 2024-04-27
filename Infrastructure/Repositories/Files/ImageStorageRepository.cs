using Domain.Abstraction;
using Domain.Interfaces.Files;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace Infrastructure.Repositories.Files
{
    public class ImageStorageRepository : IImageStorageRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _companyUploadImagePath;
        private readonly string _sendingImagePath;
        private readonly string _companyBaseUrlImagePath;
        private readonly string _sendingBaseUrlImagePath;

        public ImageStorageRepository(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["Api:S3StorageKey"];
            _companyUploadImagePath = configuration["S3Storage:CompanyUploadImagePath"];
            _sendingImagePath = configuration["S3Storage:SendingUploadImagePath"];
            _companyBaseUrlImagePath = configuration["S3Storage:CompanyBaseUrlImagePath"];
            _sendingBaseUrlImagePath = configuration["S3Storage:SendingBaseUrlImagePath"];
        }

        public async Task<Result<string>> SaveCompanyLogoAsync(IFileData file, int companyId)
        {
            var url = $"{_companyUploadImagePath}";
            var result = await UploadFileAsync(file, url);

            if (result.IsSuccess)
            {
                var fullUrl = $"{_companyBaseUrlImagePath}{result.Value}";
                return Result<string>.Success(fullUrl);
            }

            return Result<string>.Failure(result.Error);
        }

        public async Task<Result<string>> SaveSendingLogoAsync(IFileData file, int sendingId)
        {
            var url = $"{_sendingImagePath}";
            var result = await UploadFileAsync(file, url);

            if (result.IsSuccess)
            {
                var fullUrl = $"{_sendingBaseUrlImagePath}{result.Value}";
                return Result<string>.Success(fullUrl);
            }

            return Result<string>.Failure(result.Error);
        }

        private async Task<Result<string>> UploadFileAsync(IFileData file, string url)
        {
            try
            {
                string newFileName = $"{Guid.NewGuid()}_{file.FileName}";

                using (var content = new MultipartFormDataContent())
                {
                    var fileContent = new StreamContent(file.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                    content.Add(fileContent, "files", newFileName);

                    var request = new HttpRequestMessage(HttpMethod.Post, url);
                    request.Headers.Add("Authorization", $"Bearer {_apiKey}");
                    request.Content = content;

                    var response = await _httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        return Result<string>.Success(newFileName);
                    }
                    else
                    {
                        return Result<string>.Failure(Error.Failure("FileUploadFailed", $"Failed to upload file. Status code: {response.StatusCode}"));
                    }
                }
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(Error.Failure("FileUploadFailed", ex.Message));
            }
        }
    }
}
