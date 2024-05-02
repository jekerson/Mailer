using Application.DTOs.Reviews;
using Domain.Abstraction;

namespace Application.UseCases.Users.ReviewManagment
{
    public interface IReviewUseCase
    {
        Task<Result> CreateReviewAsync(CreateReviewDto createReviewDto);
        Task<Result> DeleteReviewAsync(DeleteReviewDto deleteReviewDto);
    }
}
