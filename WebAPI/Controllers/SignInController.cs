using Application.DTOs.Generals;
using Application.UseCases.SignIn;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly ISignInUseCase _signInUseCase;

        public SignInController(ISignInUseCase signInUseCase)
        {
            _signInUseCase = signInUseCase;
        }

        [HttpPost("user")]
        public async Task<IResult> UserSignIn([FromBody] GeneralSignInDto signInDto)
        {
            var result = await _signInUseCase.UserSignInAsync(signInDto);
            if (result.IsFailure)
                return result.ToProblemDetails();

            return Results.Ok(new
            {
                result.Value.AccessToken,
                result.Value.RefreshToken
            });
        }

        [HttpPost("company")]
        public async Task<IResult> CompanySignIn([FromBody] GeneralSignInDto signInDto)
        {
            var result = await _signInUseCase.CompanySignInAsync(signInDto);
            if (result.IsFailure)
                return result.ToProblemDetails();

            return Results.Ok(new
            {
                result.Value.AccessToken,
                result.Value.RefreshToken
            });
        }
    }
}
