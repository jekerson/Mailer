using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.PasswordRecovery
{
    public static class PasswordRecoveryErrors
    {
        public static Error InvalidRecoveryCode => Error.Validation(
            "RecoveryPassword.InvalidRecoveryCode",
            "Invalid recovery code.");
        public static Error EmailNotFound => Error.NotFound(
            "RecoveryPassword.EmailNotFound",
            "Email not found.");
    }
}
