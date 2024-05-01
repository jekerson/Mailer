using Application.DTOs.Sendings;
using Domain.Abstraction;

namespace Application.Validators.Sendings
{
    public class SendingValidator : ISendingValidator
    {
        public async Task<Result> ValidateAsync(CreateSendingDto createSendingDto)
        {
            if (string.IsNullOrWhiteSpace(createSendingDto.Name))
            {
                return Result.Failure(SendingValidationErrors.EmptyName);
            }

            if (createSendingDto.Name.Length > 100)
            {
                return Result.Failure(SendingValidationErrors.NameTooLong);
            }

            if (string.IsNullOrWhiteSpace(createSendingDto.Description))
            {
                return Result.Failure(SendingValidationErrors.EmptyDescription);
            }

            if (createSendingDto.Description.Length > 500)
            {
                return Result.Failure(SendingValidationErrors.DescriptionTooLong);
            }

            if (createSendingDto.Logo == null)
            {
                return Result.Failure(SendingValidationErrors.EmptyLogo);
            }

            return Result.Success();
        }
    }
}
