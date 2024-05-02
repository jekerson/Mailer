using Application.DTOs.Reviews;
using Application.Validators.Reviews;
using Domain.Abstraction;
using Domain.Entities;
using Domain.Interfaces.Reviews;
using Domain.Interfaces.Users;

namespace Application.UseCases.Users.ReviewManagment
{
    public class ReviewUseCase : IReviewUseCase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ISendingReviewRepository _sendingReviewRepository;
        private readonly IReviewValidator _reviewValidator;
        private readonly IUserRepository _userRepository;

        public ReviewUseCase(IReviewRepository reviewRepository, ISendingReviewRepository sendingReviewRepository, IReviewValidator reviewValidator, IUserRepository userRepository)
        {
            _reviewRepository = reviewRepository;
            _sendingReviewRepository = sendingReviewRepository;
            _reviewValidator = reviewValidator;
            _userRepository = userRepository;
        }

        public async Task<Result> CreateReviewAsync(CreateReviewDto createReviewDto)
        {
            var validationResult = await _reviewValidator.ValidateAsync(createReviewDto);
            if (validationResult.IsFailure)
            {
                return validationResult;
            }

            var userResult = await _userRepository.GetUserByIdAsync(createReviewDto.UserId);
            if (userResult.IsFailure)
            {
                return userResult;
            }
            var user = userResult.Value;

            var review = new Review
            {
                Rating = createReviewDto.Rating,
                Comment = createReviewDto.Comment,
                CreatedAt = DateTime.UtcNow.ToLocalTime(),
                UserId = user.Id
            };

            var addReviewResult = await _reviewRepository.AddReviewAsync(review);
            if (addReviewResult.IsFailure)
            {
                return addReviewResult;
            }

            var sendingReview = new SendingReview
            {
                SendingId = createReviewDto.SendingId,
                ReviewId = review.Id
            };

            var addSendingReviewResult = await _sendingReviewRepository.AddSendingReviewAsync(sendingReview);
            if (addSendingReviewResult.IsFailure)
            {
                return addSendingReviewResult;
            }

            return Result.Success();
        }

        public async Task<Result> DeleteReviewAsync(DeleteReviewDto deleteReviewDto)
        {
            return await _reviewRepository.DeleteReviewAsync(deleteReviewDto.ReviewId);
        }
    }
}
