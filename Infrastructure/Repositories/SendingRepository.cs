using Domain.Abstraction;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SendingRepository : ISendingRepository
    {
        private readonly SenderDbContext _dbContext;

        public SendingRepository(SenderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<Sending>>> GetAllSendingsAsync()
        {
            var sendings = await _dbContext.Sendings.AsNoTracking().ToListAsync();
            return Result<IEnumerable<Sending>>.Success(sendings);
        }

        public async Task<Result<Sending>> GetSendingByIdAsync(int id)
        {
            var sending = await _dbContext.Sendings.FirstOrDefaultAsync(s => s.Id == id);
            if (sending == null)
                return Result<Sending>.Failure(SendingErrors.SendingNotFoundById(id));

            return Result<Sending>.Success(sending);
        }

        public async Task<Result<IEnumerable<Sending>>> GetSendingsBySendingTypeIdAsync(int sendingTypeId)
        {
            var sendings = await _dbContext.Sendings.Where(s => s.SendingTypeId == sendingTypeId).AsNoTracking().ToListAsync();
            return Result<IEnumerable<Sending>>.Success(sendings);
        }

        public async Task<Result<IEnumerable<Sending>>> GetSendingsBySendingTimeIdAsync(int sendingTimeId)
        {
            var sendings = await _dbContext.Sendings.Where(s => s.SendingTimeId == sendingTimeId).AsNoTracking().ToListAsync();
            return Result<IEnumerable<Sending>>.Success(sendings);
        }

        public async Task<Result<IEnumerable<Sending>>> GetSendingsBySendingCategoryIdAsync(int sendingCategoryId)
        {
            var sendings = await _dbContext.Sendings.Where(s => s.SendingCategoryId == sendingCategoryId).AsNoTracking().ToListAsync();
            return Result<IEnumerable<Sending>>.Success(sendings);
        }

        public async Task<Result<IEnumerable<Sending>>> GetSendingsByCompanyIdAsync(int companyId)
        {
            var sendings = await _dbContext.Sendings.Where(s => s.CompanyId == companyId).AsNoTracking().ToListAsync();
            return Result<IEnumerable<Sending>>.Success(sendings);
        }

        public async Task<Result<Sending>> GetSendingByNameAsync(string name)
        {
            var sending = await _dbContext.Sendings.FirstOrDefaultAsync(s => s.Name == name);
            if (sending == null)
                return Result<Sending>.Failure(SendingErrors.SendingNotFoundByName(name));

            return Result<Sending>.Success(sending);
        }

        public async Task<Result> AddSendingAsync(Sending sending)
        {
            var duplicateCheckResult = await CheckForDuplicateNameSendingAsync(sending.Name);
            if (duplicateCheckResult.IsFailure)
                return duplicateCheckResult;

            await _dbContext.Sendings.AddAsync(sending);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateSendingAsync(Sending sending)
        {
            var existingSending = await _dbContext.Sendings.FindAsync(sending.Id);
            if (existingSending == null)
                return Result.Failure(SendingErrors.SendingNotFoundById(sending.Id));

            var duplicateCheckResult = await CheckForDuplicateNameSendingAsync(sending.Name, sending.Id);
            if (duplicateCheckResult.IsFailure)
                return duplicateCheckResult;

            _dbContext.Entry(existingSending).CurrentValues.SetValues(sending);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteSendingAsync(int id)
        {
            var sending = await _dbContext.Sendings.FindAsync(id);
            if (sending == null)
                return Result.Failure(SendingErrors.SendingNotFoundById(id));

            _dbContext.Sendings.Remove(sending);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<IEnumerable<Sending>>> GetRandomSendingsByCategoryAsync(int categoryId, int count)
        {
            var sendings = await _dbContext.Sendings
                .Where(s => s.SendingCategoryId == categoryId)
                .OrderBy(s => Guid.NewGuid())
                .Take(count)
                .ToListAsync();

            return Result<IEnumerable<Sending>>.Success(sendings);
        }

        private async Task<Result> CheckForDuplicateNameSendingAsync(string name, int? id = null)
        {
            var existingSending = await _dbContext.Sendings
                .Where(s => s.Name == name && (!id.HasValue || s.Id != id))
                .FirstOrDefaultAsync();

            if (existingSending != null)
                return Result.Failure(SendingErrors.SendingAlreadyExistByName(name));

            return Result.Success();
        }


    }
}
