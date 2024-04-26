using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors
{
    public static class UserRoleErrors
    {
        public static Error UserRoleNotFoundById(int id) =>
            Error.NotFound("UserRole.NotFoundById", $"User role with ID {id} was not found.");

        public static Error UserRoleAlreadyExist(int userId, int roleId) =>
            Error.Conflict("UserRole.AlreadyExist", $"User role with User ID {userId} and Role ID {roleId} already exists.");
    }
}
