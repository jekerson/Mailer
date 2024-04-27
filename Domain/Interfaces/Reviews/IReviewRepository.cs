using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces.Reviews
{
    public interface IReviewRepository
    {
        Task<Result<IEnumerable<Review>>> GetAllReviewsAsync();
        Task<Result<Review>> GetReviewByIdAsync(int id);
        Task<Result<IEnumerable<Review>>> GetReviewsByUserIdAsync(int userId);
        Task<Result<IEnumerable<Review>>> GetReviewsByIdsAsync(IEnumerable<int> ids);
        Task<Result> AddReviewAsync(Review review);
        Task<Result> UpdateReviewAsync(Review review);
        Task<Result> DeleteReviewAsync(int id);
    }
}
