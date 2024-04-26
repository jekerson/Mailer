using Domain.Abstraction;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Result<IEnumerable<Company>>> GetAllCompaniesAsync();
        Task<Result<Company>> GetCompanyByIdAsync(int id);
        Task<Result<Company>> GetCompanyByNameAsync(string name);
        Task<Result<Company>> GetCompanyByEmailAsync(string email);
        Task<Result> AddCompanyAsync(Company company);
        Task<Result> UpdateCompanyAsync(Company company);
        Task<Result> DeleteCompanyAsync(int id);
    }
}
