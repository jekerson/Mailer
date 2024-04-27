using Domain.Abstraction;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

public class SendingTimeRepository : ISendingTimeRepository
{
    private readonly SenderDbContext _dbContext;
    private readonly IMemoryCache _cache;
    private const string CacheKey = "SendingTimes";

    public SendingTimeRepository(SenderDbContext dbContext, IMemoryCache cache)
    {
        _dbContext = dbContext;
        _cache = cache;
    }

    public async Task<Result<IEnumerable<SendingTime>>> GetAllSendingTimesAsync()
    {
        if (!_cache.TryGetValue(CacheKey, out IEnumerable<SendingTime> times))
        {
            times = await _dbContext.SendingTimes.AsNoTracking().ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromHours(1));
            _cache.Set(CacheKey, times, cacheEntryOptions);
        }
        return Result<IEnumerable<SendingTime>>.Success(times);
    }

    public async Task<Result<SendingTime>> GetSendingTimeByIdAsync(int id)
    {
        var time = await _dbContext.SendingTimes.FindAsync(id);
        if (time == null)
            return Result<SendingTime>.Failure(SendingTimeErrors.SendingTimeNotFoundById(id));

        return Result<SendingTime>.Success(time);
    }

    public async Task<Result<SendingTime>> GetSendingTimeBySendTimeAsync(TimeOnly sendTime)
    {
        var time = await _dbContext.SendingTimes.FirstOrDefaultAsync(t => t.SendTime == sendTime);
        if (time == null)
            return Result<SendingTime>.Failure(SendingTimeErrors.SendingTimeNotFoundBySendTime(sendTime));

        return Result<SendingTime>.Success(time);
    }

    public async Task<Result> AddSendingTimeAsync(SendingTime sendingTime)
    {
        if (await IsSendingTimeExistBySendTimeAsync(sendingTime.SendTime))
            return Result.Failure(SendingTimeErrors.SendingTimeAlreadyExistBySendTime(sendingTime.SendTime));

        await _dbContext.SendingTimes.AddAsync(sendingTime);
        await _dbContext.SaveChangesAsync();
        _cache.Remove(CacheKey);
        return Result.Success();
    }

    public async Task<Result> UpdateSendingTimeAsync(SendingTime sendingTime)
    {
        var existingTime = await _dbContext.SendingTimes.FindAsync(sendingTime.Id);
        if (existingTime == null)
            return Result.Failure(SendingTimeErrors.SendingTimeNotFoundById(sendingTime.Id));

        if (existingTime.SendTime != sendingTime.SendTime && await IsSendingTimeExistBySendTimeAsync(sendingTime.SendTime))
            return Result.Failure(SendingTimeErrors.SendingTimeAlreadyExistBySendTime(sendingTime.SendTime));

        _dbContext.Entry(existingTime).CurrentValues.SetValues(sendingTime);
        await _dbContext.SaveChangesAsync();
        _cache.Remove(CacheKey);
        return Result.Success();
    }

    public async Task<Result> DeleteSendingTimeAsync(int id)
    {
        var time = await _dbContext.SendingTimes.FindAsync(id);
        if (time == null)
            return Result.Failure(SendingTimeErrors.SendingTimeNotFoundById(id));

        _dbContext.SendingTimes.Remove(time);
        await _dbContext.SaveChangesAsync();
        _cache.Remove(CacheKey);
        return Result.Success();
    }

    private async Task<bool> IsSendingTimeExistBySendTimeAsync(TimeOnly sendTime)
    {
        return await _dbContext.SendingTimes.AnyAsync(t => t.SendTime == sendTime);
    }
}
