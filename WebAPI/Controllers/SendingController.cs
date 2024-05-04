using Application.UseCases.Sendings.Category;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendingController : ControllerBase
    {

        private readonly ISendingCategoryUseCase _sendingCategoryUseCase;

        public SendingController(ISendingCategoryUseCase sendingCategoryUseCase)
        {
            _sendingCategoryUseCase = sendingCategoryUseCase;
        }

        [HttpGet("{categoryId}")]
        public async Task<IResult> GetSendingsByCategory(int categoryId)
        {
            var result = await _sendingCategoryUseCase.GetSendingsByCategoryAsync(categoryId);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : result.ToProblemDetails();
        }

        [HttpGet]
        public async Task<IResult> GetFewSendingsByCategory()
        {
            var result = await _sendingCategoryUseCase.GetFewSendingByCategoryAsync();
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : result.ToProblemDetails();
        }


    }
}
