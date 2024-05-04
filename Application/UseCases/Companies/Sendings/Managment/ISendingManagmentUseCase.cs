using Application.DTOs.Sendings;
using Domain.Abstraction;

namespace Application.UseCases.Companies.Sendings.Managment
{
    public interface ISendingManagmentUseCase
    {
        Task<Result> CreateSendingAsync(CreateSendingDto createSendingDto);

        Task<Result> DeleteSendingAsync(DeleteSendingDto deleteSendingDto);
    }
}
