using Application.DTOs.Companies;
using Application.DTOs.Users;
using Application.UseCases.SignUp;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {

        private readonly ISignUpUseCase _signUpUseCase;

        public SignUpController(ISignUpUseCase signUpUseCase)
        {
            _signUpUseCase = signUpUseCase;
        }

        [HttpPost("user")]
        public async Task<IResult> RegisterUser([FromBody] UserSignUpDto userSignUpDto)
        {
            var result = await _signUpUseCase.UserSignUpAsync(userSignUpDto);
            return result.IsFailure ? result.ToProblemDetails() : Results.Ok();
        }

        [HttpPost("company")]
        public async Task<IResult> RegisterCompany([FromForm] CompanyProfileDto companyProfileDto)
        {
            var result = await _signUpUseCase.CompanySignUpAsync(companyProfileDto);
            return result.IsFailure ? result.ToProblemDetails() : Results.Ok();
        }
    }
}
