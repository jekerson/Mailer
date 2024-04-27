using Application.DTOs;
using Application.DTOs.Companies;
using Application.DTOs.Users;
using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.SignIn
{
    public interface ISignInService
    {
        Task<AuthenticationResult> UserSignInAsync(GeneralSignInDto generalSignInDto);
        Task<AuthenticationResult> CompanySignInAsync(GeneralSignInDto generalSignInDto);
    }

}
