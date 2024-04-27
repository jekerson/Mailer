using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces.Sendings
{
    public interface ISendingCategoryRepository
    {
        Task<Result<IEnumerable<SendingCategory>>> GetAllSendingCategoriesAsync();
        Task<Result<SendingCategory>> GetSendingCategoryByIdAsync(int id);
        Task<Result<SendingCategory>> GetSendingCategoryByNameAsync(string name);
        Task<Result> AddSendingCategoryAsync(SendingCategory sendingCategory);
        Task<Result> UpdateSendingCategoryAsync(SendingCategory sendingCategory);
        Task<Result> DeleteSendingCategoryAsync(int id);
    }
}
