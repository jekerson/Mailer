using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Companies
{
    public record CompanyUpdateDataDto(string Email, string Name, string Description, IFileData Logo);

}
