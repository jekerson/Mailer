using Domain.Abstraction;

namespace Domain.Errors
{
    public static class CompanyRoleErrors
    {
        public static Error CompanyRoleNotFoundById(int id) =>
            Error.NotFound("CompanyRole.NotFoundById", $"Company role with ID {id} was not found.");

        public static Error CompanyRoleAlreadyExist(int companyId, int roleId) =>
            Error.Conflict("CompanyRole.AlreadyExist", $"Company role with Company ID {companyId} and Role ID {roleId} already exists.");
    }
}
