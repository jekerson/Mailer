using Application.DTOs.Sendings;
using Domain.Abstraction;

namespace Application.Validators.Sendings
{
    public interface ISendingValidator
    {
        Task<Result> ValidateAsync(CreateSendingDto createSendingDto);

    }
}
