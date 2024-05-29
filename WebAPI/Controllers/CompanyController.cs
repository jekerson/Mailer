using Application.DTOs.Companies;
using Application.DTOs.Generals;
using Application.UseCases.Companies.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyProfileUseCase _companyProfileUseCase;

        public CompanyController(ICompanyProfileUseCase companyProfileUseCase)
        {
            _companyProfileUseCase = companyProfileUseCase;
        }

        [HttpPost("reset-password")]
        public async Task<IResult> ResetCompanyPassword([FromBody] GeneralResetPasswordDto generalResetPasswordDto)
        {
            var result = await _companyProfileUseCase.ResetCompanyPassword(generalResetPasswordDto);
            return result.IsSuccess
                ? Results.Ok(result)
                : result.ToProblemDetails();
        }

        [HttpPost("change-email")]
        public async Task<IResult> ChangeCompanyEmail([FromBody] GeneralChangeEmailDto generalChangeEmailDto)
        {
            var result = await _companyProfileUseCase.ChangeCompanyEmail(generalChangeEmailDto);
            return result.IsSuccess
                ? Results.Ok(result)
                : result.ToProblemDetails();
        }

        [HttpDelete("delete-account")]
        public async Task<IResult> DeleteCompanyAccount([FromQuery] string email, [FromQuery] string password)
        {
            var result = await _companyProfileUseCase.DeleteCompanyAccount(email, password);
            return result.IsSuccess
                ? Results.Ok(result)
                : result.ToProblemDetails();
        }

        [HttpPut("update-data")]
        public async Task<IResult> UpdateCompanyData([FromForm] CompanyProfileDto companyProfileDto)
        {
            var result = await _companyProfileUseCase.UpdateCompanyData(companyProfileDto);
            return result.IsSuccess
                ? Results.Ok(result)
                : result.ToProblemDetails();
        }
    }
}
