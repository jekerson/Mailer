using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces.Sendings
{
    public interface ISendingRepository
    {
        Task<Result<IEnumerable<Sending>>> GetAllSendingsAsync();
        Task<Result<Sending>> GetSendingByIdAsync(int id);
        Task<Result<IEnumerable<Sending>>> GetSendingsBySendingTypeIdAsync(int sendingTypeId);
        Task<Result<IEnumerable<Sending>>> GetSendingsBySendingTimeIdAsync(int sendingTimeId);
        Task<Result<IEnumerable<Sending>>> GetSendingsBySendingCategoryIdAsync(int sendingCategoryId);
        Task<Result<IEnumerable<Sending>>> GetSendingsByCompanyIdAsync(int companyId);
        Task<Result<Sending>> GetSendingByNameAsync(string name);
        Task<Result> AddSendingAsync(Sending sending);
        Task<Result> UpdateSendingAsync(Sending sending);
        Task<Result> DeleteSendingAsync(int id);
        Task<Result<IEnumerable<Sending>>> GetRandomSendingsByCategoryAsync(int categoryId, int count);
    }
}
