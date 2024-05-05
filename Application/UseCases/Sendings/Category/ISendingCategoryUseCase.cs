using Application.Abstraction.Pagging;
using Application.DTOs.Sendings;
using Domain.Abstraction;

namespace Application.UseCases.Sendings.Category
{
    public interface ISendingCategoryUseCase
    {
        Task<Result<SendingByCategoryDto>> GetSendingsByCategoryAsync(
            int categoryId,
            int page, PageSizeType pageSize,
            SortingType sortingType);
        Task<Result<IEnumerable<SendingByCategoryDto>>> GetSomeSendingByCategoryAsync(int? CompanyId);
    }
}
