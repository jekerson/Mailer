using Domain.Abstraction;

namespace Application.DTOs.Sendings
{
    public record CreateSendingDto(
        string Name,
        string Description,
        int CategoryId,
        int SendingTypeId,
        int SendingTimeId,
        int CompanyId,
        IFileData Logo);
}
