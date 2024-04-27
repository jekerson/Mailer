using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces.Reviews
{
    public interface ISendingReviewRepository
    {
        Task<Result<IEnumerable<SendingReview>>> GetAllSendingReviewsAsync();
        Task<Result<SendingReview>> GetSendingReviewByIdAsync(int id);
        Task<Result<IEnumerable<SendingReview>>> GetSendingReviewsBySendingIdAsync(int sendingId);
        Task<Result<IEnumerable<SendingReview>>> GetSendingReviewsByReviewIdAsync(int reviewId);
        Task<Result> AddSendingReviewAsync(SendingReview sendingReview);
        Task<Result> UpdateSendingReviewAsync(SendingReview sendingReview);
        Task<Result> DeleteSendingReviewAsync(int id);
    }
}
