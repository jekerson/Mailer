using Domain.Abstraction;

namespace Application.DTOs.Companies
{
    public record CompanyProfileDto(string Name, string Email, string Password, string Description, IFileData Logo);

}
