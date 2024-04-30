using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Sendings
{
    public record CreateSendingDto(
        string Name,
        string Description,
        int CategoryId,
        int CompanyId,
        IFileData Logo);
}
