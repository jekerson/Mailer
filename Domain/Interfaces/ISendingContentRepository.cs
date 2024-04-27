using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISendingContentRepository
    {
        Task<Result<IEnumerable<SendingContent>>> GetAllSendingContentsAsync();
        Task<Result<IEnumerable<SendingContent>>> GetApprovedSendingContentsAsync(DateOnly sendingDate);
        Task<Result<SendingContent>> GetSendingContentByIdAsync(int id);
        Task<Result<IEnumerable<SendingContent>>> GetSendingContentsBySendingIdAsync(int sendingId);
        Task<Result<SendingContent>> GetSendingContentByNameAsync(string name);
        Task<Result> AddSendingContentAsync(SendingContent sendingContent);
        Task<Result> UpdateSendingContentAsync(SendingContent sendingContent);
        Task<Result> DeleteSendingContentAsync(int id);

    }
}
