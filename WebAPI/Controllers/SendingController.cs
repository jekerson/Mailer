using Application.Abstraction.Pagging;
using Application.DTOs.Sendings;
using Application.UseCases.Companies.Sendings.Managment;
using Application.UseCases.Sendings.Category;
using Application.UseCases.Users.Sendings.Subscriptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SendingController : ControllerBase
    {
        private readonly ISendingManagmentUseCase _sendingManagmentUseCase;
        private readonly ISendingSubscriptionUseCase _sendingSubscriptionUseCase;

        public SendingController(ISendingManagmentUseCase sendingManagmentUseCase, ISendingSubscriptionUseCase sendingSubscriptionUseCase)
        {
            _sendingManagmentUseCase = sendingManagmentUseCase;
            _sendingSubscriptionUseCase = sendingSubscriptionUseCase;
        }

        [HttpPost("create")]
        public async Task<IResult> CreateSending([FromForm] CreateSendingDto createSendingDto)
        {
            var result = await _sendingManagmentUseCase.CreateSendingAsync(createSendingDto);
            return result.IsSuccess
                ? Results.Ok(result)
                : result.ToProblemDetails();
        }

        [HttpDelete("delete")]
        public async Task<IResult> DeleteSending([FromBody] DeleteSendingDto deleteSendingDto)
        {
            var result = await _sendingManagmentUseCase.DeleteSendingAsync(deleteSendingDto);
            return result.IsSuccess
                ? Results.Ok(result)
                : result.ToProblemDetails();
        }

        [HttpPost("subscribe")]
        public async Task<IResult> SubscribeToSending([FromBody] SendingSubscriptionDto sendingSubscriptionDto)
        {
            var result = await _sendingSubscriptionUseCase.SubscribeToSendingAsync(sendingSubscriptionDto);
            return result.IsSuccess
                ? Results.Ok(result)
                : result.ToProblemDetails();
        }

        [HttpPost("unsubscribe")]
        public async Task<IResult> UnsubscribeFromSending([FromBody] SendingSubscriptionDto sendingSubscriptionDto)
        {
            var result = await _sendingSubscriptionUseCase.UnsubscribeFromSendingAsync(sendingSubscriptionDto);
            return result.IsSuccess
                ? Results.Ok(result)
                : result.ToProblemDetails();
        }
    }
}
