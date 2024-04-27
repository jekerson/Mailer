using Domain.Abstraction;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Sendings;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories.Sendings
{
    public class SendingTypeRepository : ISendingTypeRepository
    {
        private readonly SenderDbContext _dbContext;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "SendingTypes";

        public SendingTypeRepository(SenderDbContext dbContext, IMemoryCache cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }

        public async Task<Result<IEnumerable<SendingType>>> GetAllSendingTypesAsync()
        {
            if (!_cache.TryGetValue(CacheKey, out IEnumerable<SendingType> types))
            {
                types = await _dbContext.SendingTypes.AsNoTracking().ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));
                _cache.Set(CacheKey, types, cacheEntryOptions);
            }
            return Result<IEnumerable<SendingType>>.Success(types);
        }

        public async Task<Result<SendingType>> GetSendingTypeByIdAsync(int id)
        {
            var type = await _dbContext.SendingTypes.FindAsync(id);
            if (type == null)
                return Result<SendingType>.Failure(SendingTypeErrors.SendingTypeNotFoundById(id));

            return Result<SendingType>.Success(type);
        }

        public async Task<Result<SendingType>> GetSendingTypeByNameAsync(string name)
        {
            var type = await _dbContext.SendingTypes.FirstOrDefaultAsync(t => t.Name == name);
            if (type == null)
                return Result<SendingType>.Failure(SendingTypeErrors.SendingTypeNotFoundByName(name));

            return Result<SendingType>.Success(type);
        }

        public async Task<Result> AddSendingTypeAsync(SendingType sendingType)
        {
            if (await IsSendingTypeExistByNameAsync(sendingType.Name))
                return Result.Failure(SendingTypeErrors.SendingTypeAlreadyExistByName(sendingType.Name));

            await _dbContext.SendingTypes.AddAsync(sendingType);
            await _dbContext.SaveChangesAsync();
            _cache.Remove(CacheKey);
            return Result.Success();
        }

        public async Task<Result> UpdateSendingTypeAsync(SendingType sendingType)
        {
            var existingType = await _dbContext.SendingTypes.FindAsync(sendingType.Id);
            if (existingType == null)
                return Result.Failure(SendingTypeErrors.SendingTypeNotFoundById(sendingType.Id));

            if (existingType.Name != sendingType.Name && await IsSendingTypeExistByNameAsync(sendingType.Name))
                return Result.Failure(SendingTypeErrors.SendingTypeAlreadyExistByName(sendingType.Name));

            _dbContext.Entry(existingType).CurrentValues.SetValues(sendingType);
            await _dbContext.SaveChangesAsync();
            _cache.Remove(CacheKey);
            return Result.Success();
        }

        public async Task<Result> DeleteSendingTypeAsync(int id)
        {
            var type = await _dbContext.SendingTypes.FindAsync(id);
            if (type == null)
                return Result.Failure(SendingTypeErrors.SendingTypeNotFoundById(id));

            _dbContext.SendingTypes.Remove(type);
            await _dbContext.SaveChangesAsync();
            _cache.Remove(CacheKey);
            return Result.Success();
        }

        private async Task<bool> IsSendingTypeExistByNameAsync(string name)
        {
            return await _dbContext.SendingTypes.AnyAsync(t => t.Name == name);
        }
    }
}
