using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces.Sendings
{
    public interface ISendingScoreRepository
    {
        Task<Result<IEnumerable<SendingScore>>> GetAllSendingScoresAsync();
        Task<Result<SendingScore>> GetSendingScoreByIdAsync(int id);
        Task<Result<SendingScore>> GetSendingScoreBySendingIdAsync(int sendingId);
        Task<Result> AddSendingScoreAsync(SendingScore sendingScore);
        Task<Result> UpdateSendingScoreAsync(SendingScore sendingScore);
        Task<Result> DeleteSendingScoreAsync(int id);
        Task<Result<IEnumerable<Sending>>> GetSendingsByBestScore();
        Task<Result<IEnumerable<Sending>>> GetSendingsByMostSubscriber();
    }
}
