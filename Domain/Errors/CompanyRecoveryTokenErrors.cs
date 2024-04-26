using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors
{
    public static class CompanyRecoveryTokenErrors
    {
        public static Error CompanyRecoveryTokenNotFoundById(int id) =>
            Error.NotFound("CompanyRecoveryToken.NotFoundById", $"Company recovery token with ID {id} was not found.");

        public static Error CompanyRecoveryTokenNotFoundByToken(string token) =>
            Error.NotFound("CompanyRecoveryToken.NotFoundByToken", $"Company recovery token with token {token} was not found.");
    }
}
