using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.SignIn
{
    public static class SignInErrors
    {
        public static Error InvalidCredentials => Error.Validation(
            "SignIn.InvalidCredentials",
            "Invalid email or password.");
    }
}
