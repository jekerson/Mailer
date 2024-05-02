using Application.DTOs.Sendings;
using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Companies.Sendings.Managment
{
    public interface ISendingManagmentUseCase
    {
        Task<Result> CreateSendingAsync(CreateSendingDto createSendingDto);

        Task<Result> DeleteSendingAsync(DeleteSendingDto deleteSendingDto);
    }
}
