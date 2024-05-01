using Application.DTOs.Reviews;
using Domain.Abstraction;

namespace Application.Validators.Reviews
{
    public class ReviewValidator : IReviewValidator
    {
        public async Task<Result> ValidateAsync(CreateReviewDto createReviewDto)
        {
            if (createReviewDto.SendingId <= 0)
            {
                return Result.Failure(ReviewValidationErrors.InvalidSendingId);
            }
            if (createReviewDto.UserId <= 0)
            {
                return Result.Failure(ReviewValidationErrors.InvalidUserId);
            }

            if (createReviewDto.Rating < 1 || createReviewDto.Rating > 5)
            {
                return Result.Failure(ReviewValidationErrors.InvalidRating);
            }

            if (string.IsNullOrWhiteSpace(createReviewDto.Comment))
            {
                return Result.Failure(ReviewValidationErrors.CommentRequired);
            }

            return Result.Success();
        }
    }
}
