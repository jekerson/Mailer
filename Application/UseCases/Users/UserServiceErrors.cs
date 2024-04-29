using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Users
{
    public static class UserServiceErrors
    {
        public static Error InvalidPassword => Error.Validation(
            "Company.InvalidPassword",
            "The provided password is invalid.");
    }
}
