using Domain.Abstraction;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
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
