using Domain.Abstraction;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CompanyRoleRepository : ICompanyRoleRepository
    {
        private readonly SenderDbContext _dbContext;

        public CompanyRoleRepository(SenderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<CompanyRole>>> GetAllCompanyRolesAsync()
        {
            var companyRoles = await _dbContext.CompanyRoles.AsNoTracking().ToListAsync();
            return Result<IEnumerable<CompanyRole>>.Success(companyRoles);
        }

        public async Task<Result<IEnumerable<CompanyRole>>> GetCompanyRolesByCompanyIdAsync(int companyId)
        {
            var companyRoles = await _dbContext.CompanyRoles.Where(cr => cr.CompanyId == companyId).AsNoTracking().ToListAsync();
            return Result<IEnumerable<CompanyRole>>.Success(companyRoles);
        }

        public async Task<Result<IEnumerable<CompanyRole>>> GetCompanyRolesByRoleIdAsync(int roleId)
        {
            var companyRoles = await _dbContext.CompanyRoles.Where(cr => cr.RoleId == roleId).AsNoTracking().ToListAsync();
            return Result<IEnumerable<CompanyRole>>.Success(companyRoles);
        }

        public async Task<Result> AddCompanyRoleAsync(CompanyRole companyRole)
        {
            if (await IsCompanyRoleExistAsync(companyRole.CompanyId, companyRole.RoleId))
                return Result.Failure(CompanyRoleErrors.CompanyRoleAlreadyExist(companyRole.CompanyId, companyRole.RoleId));

            await _dbContext.CompanyRoles.AddAsync(companyRole);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateCompanyRoleAsync(CompanyRole companyRole)
        {
            var existingCompanyRole = await _dbContext.CompanyRoles.FindAsync(companyRole.Id);
            if (existingCompanyRole == null)
                return Result.Failure(CompanyRoleErrors.CompanyRoleNotFoundById(companyRole.Id));

            _dbContext.Entry(existingCompanyRole).CurrentValues.SetValues(companyRole);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteCompanyRoleAsync(int id)
        {
            var companyRole = await _dbContext.CompanyRoles.FindAsync(id);
            if (companyRole == null)
                return Result.Failure(CompanyRoleErrors.CompanyRoleNotFoundById(id));

            _dbContext.CompanyRoles.Remove(companyRole);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        private async Task<bool> IsCompanyRoleExistAsync(int companyId, int roleId)
        {
            return await _dbContext.CompanyRoles.AnyAsync(cr => cr.CompanyId == companyId && cr.RoleId == roleId);
        }
    }
}
