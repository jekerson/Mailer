using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Companies
{
    public static class CompanyServiceErrors
    {
        public static Error InvalidPassword => Error.Validation(
            "Company.InvalidPassword",
            "The provided password is invalid.");
    }
}
