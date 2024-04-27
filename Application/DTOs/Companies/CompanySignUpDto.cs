using Domain.Abstraction;

namespace Application.DTOs.Companies
{
    public record CompanySignUpDto(string Name, string Email, string Password, string Description, IFileData Logo);

}
