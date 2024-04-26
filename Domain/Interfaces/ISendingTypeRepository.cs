using Domain.Abstraction;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISendingTypeRepository
    {
        Task<Result<IEnumerable<SendingType>>> GetAllSendingTypesAsync();
        Task<Result<SendingType>> GetSendingTypeByIdAsync(int id);
        Task<Result<SendingType>> GetSendingTypeByNameAsync(string name);
        Task<Result> AddSendingTypeAsync(SendingType sendingType);
        Task<Result> UpdateSendingTypeAsync(SendingType sendingType);
        Task<Result> DeleteSendingTypeAsync(int id);
    }
}
