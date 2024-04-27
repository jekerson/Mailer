using Domain.Abstraction;

namespace Application.Validators.General
{
    public interface IGeneralInputValidator
    {
        Task<Result> ValidateEmailAsync(string email);

        Task<Result> ValidatePasswordAsync(string password);

        Task<Result> ValidateNameAsync(string name);

    }
}
