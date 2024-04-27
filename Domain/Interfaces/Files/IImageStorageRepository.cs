using Domain.Abstraction;

namespace Domain.Interfaces.Files
{
    public interface IImageStorageRepository
    {
        Task<Result<string>> SaveCompanyLogoAsync(IFileData file, int companyId);
        Task<Result<string>> SaveSendingLogoAsync(IFileData file, int sendingId);
    }
}
