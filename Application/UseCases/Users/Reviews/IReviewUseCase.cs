using Application.DTOs.Reviews;
using Domain.Abstraction;

namespace Application.UseCases.Users.Reviews
{
    public interface IReviewUseCase
    {
        Task<Result> CreateReviewAsync(CreateReviewDto createReviewDto);
        Task<Result> DeleteReviewAsync(DeleteReviewDto deleteReviewDto);
    }
}
