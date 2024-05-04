using Application.Services.RefreshToken;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshTokenController : ControllerBase
    {
        private readonly ITokenRefreshService _tokenRefreshService;

        public RefreshTokenController(ITokenRefreshService tokenRefreshService)
        {
            _tokenRefreshService = tokenRefreshService;
        }

        [HttpPost("user")]
        public async Task<IResult> RefreshUserToken([FromQuery] string refreshToken)
        {
            var result = await _tokenRefreshService.RefreshUserTokenAsync(refreshToken);
            if (result.IsFailure)
                return result.ToProblemDetails();

            return Results.Ok(new
            {
                result.Value.AccessToken,
                result.Value.RefreshToken
            });
        }

        [HttpPost("company")]
        public async Task<IResult> RefreshCompanyToken([FromQuery] string refreshToken)
        {
            var result = await _tokenRefreshService.RefreshCompanyTokenAsync(refreshToken);
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
