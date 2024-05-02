using Application.DTOs.Sendings;
using Domain.Abstraction;

namespace Application.UseCases.Sendings.Category
{
    public interface ISendingCategoryUseCase
    {
        Task<Result<SendingByCategoryDto>> GetSendingsByCategoryAsync(int categoryId);
        Task<Result<IEnumerable<SendingByCategoryDto>>> GetFewSendingByCategoryAsync();
    }
}
