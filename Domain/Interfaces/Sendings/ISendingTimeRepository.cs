using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces.Sendings
{
    public interface ISendingTimeRepository
    {
        Task<Result<IEnumerable<SendingTime>>> GetAllSendingTimesAsync();
        Task<Result<SendingTime>> GetSendingTimeByIdAsync(int id);
        Task<Result<SendingTime>> GetSendingTimeBySendTimeAsync(TimeOnly sendTime);
        Task<Result> AddSendingTimeAsync(SendingTime sendingTime);
        Task<Result> UpdateSendingTimeAsync(SendingTime sendingTime);
        Task<Result> DeleteSendingTimeAsync(int id);
    }
}
