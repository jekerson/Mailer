using Application.DTOs.Reviews;
using Domain.Abstraction;

namespace Application.Validators.Reviews
{
    public interface IReviewValidator
    {
        Task<Result> ValidateAsync(CreateReviewDto createReviewDto);

    }
}
