using Application.DTOs.Sendings;
using Domain.Abstraction;

namespace Application.UseCases.Sendings.Sort
{
    public interface ISendingSortingUseCase
    {
        Task<Result<IEnumerable<SendingByCategoryDto>>> GetSendingByCompany(int companyId);
        Task<Result<SendingByCategoryDto>> GetSendingsByCategorySortedByReviewsAsync(int categoryId);
        Task<Result<SendingByCategoryDto>> GetSendingsByCategorySortedBySubscribersAsync(int categoryId);
    }
}
