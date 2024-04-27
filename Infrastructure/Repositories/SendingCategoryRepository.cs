using Domain.Abstraction;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories
{
    public class SendingCategoryRepository : ISendingCategoryRepository
    {
        private readonly SenderDbContext _dbContext;
        private readonly IMemoryCache _cache;
        private const string CategoriesCacheKey = "sendingCategoriesCache";

        public SendingCategoryRepository(SenderDbContext dbContext, IMemoryCache cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }

        public async Task<Result<IEnumerable<SendingCategory>>> GetAllSendingCategoriesAsync()
        {
            if (!_cache.TryGetValue(CategoriesCacheKey, out IEnumerable<SendingCategory> categories))
            {
                categories = await _dbContext.SendingCategories.AsNoTracking().ToListAsync();
                _cache.Set(CategoriesCacheKey, categories, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(24)));
            }
            return Result<IEnumerable<SendingCategory>>.Success(categories);
        }

        public async Task<Result<SendingCategory>> GetSendingCategoryByIdAsync(int id)
        {
            var category = await _dbContext.SendingCategories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
                return Result<SendingCategory>.Failure(SendingCategoryErrors.SendingCategoryNotFoundById(id));

            return Result<SendingCategory>.Success(category);
        }

        public async Task<Result<SendingCategory>> GetSendingCategoryByNameAsync(string name)
        {
            var category = await _dbContext.SendingCategories.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
            if (category == null)
                return Result<SendingCategory>.Failure(SendingCategoryErrors.SendingCategoryNotFoundByName(name));

            return Result<SendingCategory>.Success(category);
        }

        public async Task<Result> AddSendingCategoryAsync(SendingCategory sendingCategory)
        {
            if (await IsSendingCategoryExistByNameAsync(sendingCategory.Name))
                return Result.Failure(SendingCategoryErrors.SendingCategoryAlreadyExistByName(sendingCategory.Name));

            await _dbContext.SendingCategories.AddAsync(sendingCategory);
            await _dbContext.SaveChangesAsync();
            _cache.Remove(CategoriesCacheKey);
            return Result.Success();
        }

        public async Task<Result> UpdateSendingCategoryAsync(SendingCategory sendingCategory)
        {
            var existingCategory = await _dbContext.SendingCategories.FindAsync(sendingCategory.Id);
            if (existingCategory == null)
                return Result.Failure(SendingCategoryErrors.SendingCategoryNotFoundById(sendingCategory.Id));

            if (existingCategory.Name != sendingCategory.Name && await IsSendingCategoryExistByNameAsync(sendingCategory.Name))
                return Result.Failure(SendingCategoryErrors.SendingCategoryAlreadyExistByName(sendingCategory.Name));

            _dbContext.Entry(existingCategory).CurrentValues.SetValues(sendingCategory);
            await _dbContext.SaveChangesAsync();
            _cache.Remove(CategoriesCacheKey);
            return Result.Success();
        }

        public async Task<Result> DeleteSendingCategoryAsync(int id)
        {
            var category = await _dbContext.SendingCategories.FindAsync(id);
            if (category == null)
                return Result.Failure(SendingCategoryErrors.SendingCategoryNotFoundById(id));

            _dbContext.SendingCategories.Remove(category);
            await _dbContext.SaveChangesAsync();
            _cache.Remove(CategoriesCacheKey);
            return Result.Success();
        }

        private async Task<bool> IsSendingCategoryExistByNameAsync(string name)
        {
            return await _dbContext.SendingCategories.AsNoTracking().AnyAsync(c => c.Name == name);
        }
    }
}
