using Domain.Abstraction;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly SenderDbContext _dbContext;

        public ReviewRepository(SenderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<Review>>> GetAllReviewsAsync()
        {
            var reviews = await _dbContext.Reviews.AsNoTracking().ToListAsync();
            return Result<IEnumerable<Review>>.Success(reviews);
        }


        public async Task<Result<Review>> GetReviewByIdAsync(int id)
        {
            var review = await _dbContext.Reviews.FindAsync(id);
            if (review == null)
                return Result<Review>.Failure(ReviewErrors.ReviewNotFoundById(id));

            return Result<Review>.Success(review);
        }

        public async Task<Result<IEnumerable<Review>>> GetReviewsByUserIdAsync(int userId)
        {
            var reviews = await _dbContext.Reviews.Where(r => r.UserId == userId).AsNoTracking().ToListAsync();
            return Result<IEnumerable<Review>>.Success(reviews);
        }

        public async Task<Result> AddReviewAsync(Review review)
        {
            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateReviewAsync(Review review)
        {
            var existingReview = await _dbContext.Reviews.FindAsync(review.Id);
            if (existingReview == null)
                return Result.Failure(ReviewErrors.ReviewNotFoundById(review.Id));

            _dbContext.Entry(existingReview).CurrentValues.SetValues(review);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteReviewAsync(int id)
        {
            var review = await _dbContext.Reviews.FindAsync(id);
            if (review == null)
                return Result.Failure(ReviewErrors.ReviewNotFoundById(id));

            _dbContext.Reviews.Remove(review);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        public async Task<Result<IEnumerable<Review>>> GetReviewsByIdsAsync(IEnumerable<int> ids)
        {
            var reviews = await _dbContext.Reviews
                .Where(r => ids.Contains(r.Id))
                .AsNoTracking()
                .ToListAsync();

            return Result<IEnumerable<Review>>.Success(reviews);
        }
    }
}
