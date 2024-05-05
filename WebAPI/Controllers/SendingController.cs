using Application.Abstraction.Pagging;
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

        [HttpGet()]
        public async Task<IResult> GetSendingsByCategory(
            [FromQuery] int categoryId,
            [FromQuery] int page = 1,
            [FromQuery] PageSizeType pageSize = PageSizeType.Medium,
            [FromQuery] SortingType sortingType = SortingType.None)
        {
            var result = await _sendingCategoryUseCase.GetSendingsByCategoryAsync(categoryId, page, pageSize, sortingType);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : result.ToProblemDetails();
        }

        [HttpGet("general")]
        public async Task<IResult> GetSomeSendingByCategory(
            [FromQuery] int? companyId = null)
        {
            var result = await _sendingCategoryUseCase.GetSomeSendingByCategoryAsync(companyId);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : result.ToProblemDetails();
        }



    }
}
