using Domain.Abstraction;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Sendings;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories.Sendings
{
    public class SendingScoreRepository : ISendingScoreRepository
    {
        private readonly SenderDbContext _dbContext;
        private readonly IMemoryCache _cache;
        private const string BestScoreCacheKey = "bestScoreCache";
        private const string MostSubscribersCacheKey = "mostSubscribersCache";

        public SendingScoreRepository(SenderDbContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _cache = memoryCache;
        }

        public async Task<Result<IEnumerable<SendingScore>>> GetAllSendingScoresAsync()
        {
            var sendingScores = await _dbContext.SendingScores.ToListAsync();
            return Result<IEnumerable<SendingScore>>.Success(sendingScores);
        }

        public async Task<Result<SendingScore>> GetSendingScoreByIdAsync(int id)
        {
            var sendingScore = await _dbContext.SendingScores.FindAsync(id);
            if (sendingScore == null)
                return Result<SendingScore>.Failure(SendingScoreErrors.SendingScoreNotFoundById(id));

            return Result<SendingScore>.Success(sendingScore);
        }

        public async Task<Result<SendingScore>> GetSendingScoreBySendingIdAsync(int sendingId)
        {
            var sendingScore = await _dbContext.SendingScores.FirstOrDefaultAsync(ss => ss.SendingId == sendingId);
            if (sendingScore == null)
                return Result<SendingScore>.Failure(SendingScoreErrors.SendingScoreNotFoundBySendingId(sendingId));

            return Result<SendingScore>.Success(sendingScore);
        }

        public async Task<Result> AddSendingScoreAsync(SendingScore sendingScore)
        {
            if (await IsSendingScoreExistBySendingIdAsync(sendingScore.SendingId))
                return Result.Failure(SendingScoreErrors.SendingScoreAlreadyExistBySendingId(sendingScore.SendingId));
            await _dbContext.SendingScores.AddAsync(sendingScore);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateSendingScoreAsync(SendingScore sendingScore)
        {
            var existingSendingScore = await _dbContext.SendingScores.FindAsync(sendingScore.Id);
            if (existingSendingScore == null)
                return Result.Failure(SendingScoreErrors.SendingScoreNotFoundById(sendingScore.Id));

            _dbContext.Entry(existingSendingScore).CurrentValues.SetValues(sendingScore);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteSendingScoreAsync(int id)
        {
            var sendingScore = await _dbContext.SendingScores.FindAsync(id);
            if (sendingScore == null)
                return Result.Failure(SendingScoreErrors.SendingScoreNotFoundById(id));

            _dbContext.SendingScores.Remove(sendingScore);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<IEnumerable<Sending>>> GetSendingsByBestScore()
        {
            if (!_cache.TryGetValue(BestScoreCacheKey, out IEnumerable<Sending> sendings))
            {
                sendings = await _dbContext.SendingScores
                    .OrderByDescending(ss => ss.ReviewScore)
                    .Select(ss => ss.Sending)
                    .AsNoTracking()
                    .ToListAsync();
                _cache.Set(BestScoreCacheKey, sendings, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            }
            return Result<IEnumerable<Sending>>.Success(sendings);
        }

        public async Task<Result<IEnumerable<Sending>>> GetSendingsByMostSubscriber()
        {
            if (!_cache.TryGetValue(MostSubscribersCacheKey, out IEnumerable<Sending> sendings))
            {
                sendings = await _dbContext.SendingScores
                    .OrderByDescending(ss => ss.CurrentSubscriber)
                    .Select(ss => ss.Sending)
                    .AsNoTracking()
                    .ToListAsync();
                _cache.Set(MostSubscribersCacheKey, sendings, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            }
            return Result<IEnumerable<Sending>>.Success(sendings);
        }


        private async Task<bool> IsSendingScoreExistBySendingIdAsync(int sendingId)
        {
            return await _dbContext.SendingScores.AnyAsync(ss => ss.SendingId == sendingId);
        }
    }
}
