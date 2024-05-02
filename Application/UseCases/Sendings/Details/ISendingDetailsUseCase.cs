using Application.DTOs.Sendings;
using Domain.Abstraction;

namespace Application.UseCases.Sendings.Details
{
    public interface ISendingDetailsUseCase
    {
        Task<Result<SendingWithDetailsDto>> GetSendingDetailsAsync(int sendingId);
    }
}
