using Application.DTOs.Reviews;
using Application.UseCases.Users.Reviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewUseCase _reviewUseCase;

        public ReviewController(IReviewUseCase reviewUseCase)
        {
            _reviewUseCase = reviewUseCase;
        }

        [HttpPost("create")]
        public async Task<IResult> CreateReview([FromBody] CreateReviewDto createReviewDto)
        {
            var result = await _reviewUseCase.CreateReviewAsync(createReviewDto);
            return result.IsSuccess
                ? Results.Ok(result)
                : result.ToProblemDetails();
        }

        [HttpDelete("delete")]
        public async Task<IResult> DeleteReview([FromBody] DeleteReviewDto deleteReviewDto)
        {
            var result = await _reviewUseCase.DeleteReviewAsync(deleteReviewDto);
            return result.IsSuccess
                ? Results.Ok(result)
                : result.ToProblemDetails();
        }
    }
}
