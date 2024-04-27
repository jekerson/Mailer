using Domain.Abstraction;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SendingReviewRepository : ISendingReviewRepository
    {
        private readonly SenderDbContext _dbContext;

        public SendingReviewRepository(SenderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<SendingReview>>> GetAllSendingReviewsAsync()
        {
            var sendingReviews = await _dbContext.SendingReviews.AsNoTracking().ToListAsync();
            return Result<IEnumerable<SendingReview>>.Success(sendingReviews);
        }

        public async Task<Result<SendingReview>> GetSendingReviewByIdAsync(int id)
        {
            var sendingReview = await _dbContext.SendingReviews.FindAsync(id);
            if (sendingReview == null)
                return Result<SendingReview>.Failure(SendingReviewErrors.SendingReviewNotFoundById(id));

            return Result<SendingReview>.Success(sendingReview);
        }

        public async Task<Result<IEnumerable<SendingReview>>> GetSendingReviewsBySendingIdAsync(int sendingId)
        {
            var sendingReviews = await _dbContext.SendingReviews.Where(sr => sr.SendingId == sendingId).AsNoTracking().ToListAsync();
            return Result<IEnumerable<SendingReview>>.Success(sendingReviews);
        }

        public async Task<Result<IEnumerable<SendingReview>>> GetSendingReviewsByReviewIdAsync(int reviewId)
        {
            var sendingReviews = await _dbContext.SendingReviews.Where(sr => sr.ReviewId == reviewId).AsNoTracking().ToListAsync();
            return Result<IEnumerable<SendingReview>>.Success(sendingReviews);
        }

        public async Task<Result> AddSendingReviewAsync(SendingReview sendingReview)
        {
            if (await IsSendingReviewExistAsync(sendingReview.SendingId, sendingReview.ReviewId))
                return Result.Failure(SendingReviewErrors.SendingReviewAlreadyExist(sendingReview.SendingId, sendingReview.ReviewId));

            await _dbContext.SendingReviews.AddAsync(sendingReview);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateSendingReviewAsync(SendingReview sendingReview)
        {
            var existingSendingReview = await _dbContext.SendingReviews.FindAsync(sendingReview.Id);
            if (existingSendingReview == null)
                return Result.Failure(SendingReviewErrors.SendingReviewNotFoundById(sendingReview.Id));

            _dbContext.Entry(existingSendingReview).CurrentValues.SetValues(sendingReview);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteSendingReviewAsync(int id)
        {
            var sendingReview = await _dbContext.SendingReviews.FindAsync(id);
            if (sendingReview == null)
                return Result.Failure(SendingReviewErrors.SendingReviewNotFoundById(id));

            _dbContext.SendingReviews.Remove(sendingReview);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        private async Task<bool> IsSendingReviewExistAsync(int sendingId, int reviewId)
        {
            return await _dbContext.SendingReviews.AnyAsync(sr => sr.SendingId == sendingId && sr.ReviewId == reviewId);
        }
    }
}
