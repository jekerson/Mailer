using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Generals
{
    public record GeneralPasswordRecoveryDto(
        string Email,
        string IpAddress,
        string UserAgent);
}
