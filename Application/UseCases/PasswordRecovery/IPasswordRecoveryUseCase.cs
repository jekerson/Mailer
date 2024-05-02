using Application.DTOs.Generals;
using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.PasswordRecovery
{
    public interface IPasswordRecoveryUseCase
    {
        Task<Result> SendRecoveryCodeAsync(GeneralPasswordRecoveryDto generalPasswordRecoveryDto);
        Task<Result> VerifyRecoveryCodeAsync(GeneralRecoveryCodeDto generalRecoveryCodeDto);
    }

}
