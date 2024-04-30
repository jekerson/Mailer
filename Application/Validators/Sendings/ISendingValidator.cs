using Application.DTOs.Sendings;
using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Sendings
{
    public interface ISendingValidator
    {
        Task<Result> ValidateAsync(CreateSendingDto createSendingDto);

    }
}
