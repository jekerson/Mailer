using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors
{
    public static class RoleErrors
    {
        public static Error RoleNotFoundById(int id) =>
            Error.NotFound("Role.NotFoundById", $"Role with ID {id} was not found.");

        public static Error RoleNotFoundByName(string name) =>
            Error.NotFound("Role.NotFoundByName", $"Role with name {name} was not found.");

        public static Error RoleAlreadyExist(string name) =>
            Error.Conflict("Role.AlreadyExist", $"Role with name {name} already exists.");
    }
}
