using Application.DTOs.Sendings;
using Domain.Abstraction;
using Domain.Interfaces.Companies;
using Domain.Interfaces.Sendings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Sendings
{
    public class SendingValidator: ISendingValidator
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
