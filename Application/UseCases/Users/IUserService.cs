using Application.DTOs.Generals;
using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Users
{
    public interface IUserService
    {
        Task<Result> ResetUserPassword(GeneralResetPasswordDto generalResetPasswordDto);

        Task<Result> ChangeUserEmail(GeneralChangeEmailDto generalChangeEmailDto);

        Task<Result> DeleteUserAccount(GeneralSignInDto generalSignInDto);

    }
}
