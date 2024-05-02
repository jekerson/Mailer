using Application.DTOs.Sendings;
using Domain.Abstraction;
using Domain.Interfaces.Sendings;

namespace Application.UseCases.Sendings.Details
{
    public class SendingDetailsUseCase : ISendingDetailsUseCase
    {
        private readonly ISendingRepository _sendingRepository;
        private readonly ISendingScoreRepository _sendingScoreRepository;

        public SendingDetailsUseCase(
            ISendingRepository sendingRepository,
            ISendingScoreRepository sendingScoreRepository)
        {
            _sendingRepository = sendingRepository;
            _sendingScoreRepository = sendingScoreRepository;
        }
        public async Task<Result<SendingWithDetailsDto>> GetSendingDetailsAsync(int sendingId)
        {
            var sendingResult = await _sendingRepository.GetSendingByIdAsync(sendingId);
            if (sendingResult.IsFailure)
            {
                return Result<SendingWithDetailsDto>.Failure(sendingResult.Error);
            }

            var sendingScoreResult = await _sendingScoreRepository.GetSendingScoreBySendingIdAsync(sendingId);
            if (sendingScoreResult.IsFailure)
            {
                return Result<SendingWithDetailsDto>.Failure(sendingScoreResult.Error);
            }

            var sending = sendingResult.Value;
            var sendingScore = sendingScoreResult.Value;

            var sendingDto = new SendingWithDetailsDto(
                sending.Id,
                sending.Name,
                sending.Description,
                sending.ImagePath,
                sendingScore.CurrentSubscriber,
                sendingScore.ReviewScore,
                sending.CompanyId,
                sending.Company.Name,
                sending.Company.Description,
                sending.Company.ImagePath
            );

            return Result<SendingWithDetailsDto>.Success(sendingDto);
        }
    }
}
