using Domain.Abstraction;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Sendings;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Sendings
{
    public class SendingContentRepository : ISendingContentRepository
    {
        private readonly SenderDbContext _dbContext;

        public SendingContentRepository(SenderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<SendingContent>>> GetAllSendingContentsAsync()
        {
            var sendingContents = await _dbContext.SendingContents.AsNoTracking().ToListAsync();
            return Result<IEnumerable<SendingContent>>.Success(sendingContents);
        }

        public async Task<Result<SendingContent>> GetSendingContentByIdAsync(int id)
        {
            var sendingContent = await _dbContext.SendingContents.FindAsync(id);
            if (sendingContent == null)
                return Result<SendingContent>.Failure(SendingContentErrors.SendingContentNotFoundById(id));

            return Result<SendingContent>.Success(sendingContent);
        }

        public async Task<Result<IEnumerable<SendingContent>>> GetSendingContentsBySendingIdAsync(int sendingId)
        {
            var sendingContents = await _dbContext.SendingContents.Where(sc => sc.SendingId == sendingId).AsNoTracking().ToListAsync();
            return Result<IEnumerable<SendingContent>>.Success(sendingContents);
        }

        public async Task<Result<SendingContent>> GetSendingContentByNameAsync(string name)
        {
            var sendingContent = await _dbContext.SendingContents.FirstOrDefaultAsync(sc => sc.Name == name);
            if (sendingContent == null)
                return Result<SendingContent>.Failure(SendingContentErrors.SendingContentNotFoundByName(name));

            return Result<SendingContent>.Success(sendingContent);
        }

        public async Task<Result> AddSendingContentAsync(SendingContent sendingContent)
        {
            await _dbContext.SendingContents.AddAsync(sendingContent);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateSendingContentAsync(SendingContent sendingContent)
        {
            var existingSendingContent = await _dbContext.SendingContents.FindAsync(sendingContent.Id);
            if (existingSendingContent == null)
                return Result.Failure(SendingContentErrors.SendingContentNotFoundById(sendingContent.Id));

            _dbContext.Entry(existingSendingContent).CurrentValues.SetValues(sendingContent);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteSendingContentAsync(int id)
        {
            var sendingContent = await _dbContext.SendingContents.FindAsync(id);
            if (sendingContent == null)
                return Result.Failure(SendingContentErrors.SendingContentNotFoundById(id));

            _dbContext.SendingContents.Remove(sendingContent);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        public async Task<Result<IEnumerable<SendingContent>>> GetApprovedSendingContentsAsync(DateOnly sendingDate)
        {
            var sendingContents = await _dbContext.SendingContents
                .Where(sc => sc.SendingDate == sendingDate && sc.IsApproved)
                .AsNoTracking()
                .ToListAsync();

            return Result<IEnumerable<SendingContent>>.Success(sendingContents);
        }
    }
}
