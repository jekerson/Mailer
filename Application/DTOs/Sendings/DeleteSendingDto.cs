using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Sendings
{
    public record DeleteSendingDto(int CompanyId, int SendingId);
}
