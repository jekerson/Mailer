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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly SenderDbContext _dbContext;

        public CompanyRepository(SenderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<Company>>> GetAllCompaniesAsync()
        {
            var companies = await _dbContext.Companies.ToListAsync();
            return Result<IEnumerable<Company>>.Success(companies);
        }

        public async Task<Result<Company>> GetCompanyByIdAsync(int id)
        {
            var company = await _dbContext.Companies.FindAsync(id);
            if (company == null)
                return Result<Company>.Failure(CompanyErrors.CompanyNotFoundById(id));

            return Result<Company>.Success(company);
        }

        public async Task<Result<Company>> GetCompanyByNameAsync(string name)
        {
            var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Name == name);
            if (company == null)
                return Result<Company>.Failure(CompanyErrors.CompanyNotFoundByName(name));

            return Result<Company>.Success(company);
        }

        public async Task<Result<Company>> GetCompanyByEmailAsync(string email)
        {
            var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Email == email);
            if (company == null)
                return Result<Company>.Failure(CompanyErrors.CompanyNotFoundByEmail(email));

            return Result<Company>.Success(company);
        }
        public async Task<Result> AddCompanyAsync(Company company)
        {
            var errors = await CheckCompanyExistsAsync(company.Name, company.Email);
            if (errors.Any())
                return Result.Failure(errors.First());

            await _dbContext.Companies.AddAsync(company);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateCompanyAsync(Company company)
        {
            var errors = await CheckCompanyExistsAsync(company.Name, company.Email);
            if (errors.Any())
                return Result.Failure(errors.First());

            var existingCompany = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == company.Id);
            if (existingCompany == null)
                return Result.Failure(CompanyErrors.CompanyNotFoundById(company.Id));

            _dbContext.Entry(existingCompany).CurrentValues.SetValues(company);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }


        public async Task<Result> DeleteCompanyAsync(int id)
        {
            var company = await _dbContext.Companies.FindAsync(id);
            if (company == null)
                return Result.Failure(CompanyErrors.CompanyNotFoundById(id));

            _dbContext.Companies.Remove(company);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        private async Task<IEnumerable<Error>> CheckCompanyExistsAsync(string name, string email)
        {
            var errors = new List<Error>();
            if (await _dbContext.Companies.AnyAsync(c => c.Name == name))
                errors.Add(CompanyErrors.CompanyAlreadyExistByName(name));
            if (await _dbContext.Companies.AnyAsync(c => c.Email == email))
                errors.Add(CompanyErrors.CompanyAlreadyExistByEmail(email));

            return errors;
        }

    }
}
