using Domain.Abstraction;

namespace Domain.Errors
{
    public static class CompanyErrors
    {
        public static Error CompanyNotFoundById(int id) =>
            Error.NotFound("Company.NotFoundById", $"Company with ID {id} was not found.");

        public static Error CompanyNotFoundByName(string name) =>
            Error.NotFound("Company.NotFoundByName", $"Company with name {name} was not found.");

        public static Error CompanyNotFoundByEmail(string email) =>
            Error.NotFound("Company.NotFoundByEmail", $"Company with email {email} was not found.");

        public static Error CompanyAlreadyExistByName(string name) =>
            Error.Conflict("Company.AlreadyExistByName", $"Company with name {name} already exists.");

        public static Error CompanyAlreadyExistByEmail(string email) =>
            Error.Conflict("Company.AlreadyExistByEmail", $"Company with email {email} already exists.");
    }
}
