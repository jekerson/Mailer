using Application.DTOs.Sendings;
using Domain.Abstraction;
using Domain.Interfaces.Sendings;

namespace Application.UseCases.Sendings.Category
{
    public class SendingCategoryUseCase : ISendingCategoryUseCase
    {
        private readonly ISendingRepository _sendingRepository;
        private readonly ISendingCategoryRepository _sendingCategoryRepository;
        private readonly ISendingScoreRepository _sendingScoreRepository;



        public SendingCategoryUseCase(
            ISendingRepository sendingRepository,
            ISendingCategoryRepository categoryRepository,
            ISendingScoreRepository sendingScoreRepository)
        {
            _sendingRepository = sendingRepository;
            _sendingCategoryRepository = categoryRepository;
            _sendingScoreRepository = sendingScoreRepository;
        }

        public async Task<Result<SendingByCategoryDto>> GetSendingsByCategoryAsync(int categoryId)
        {
            var categoryResult = await _sendingCategoryRepository.GetSendingCategoryByIdAsync(categoryId);
            if (categoryResult.IsFailure)
            {
                return Result<SendingByCategoryDto>.Failure(categoryResult.Error);
            }

            var sendingsResult = await _sendingRepository.GetSendingsBySendingCategoryIdAsync(categoryId);

            var sendings = sendingsResult.Value.Where(s => !s.IsDeleted);

            var sendingDtos = new List<SendingWithoutDetailsDto>();
            foreach (var sending in sendings)
            {

                sendingDtos.Add(new SendingWithoutDetailsDto(
                    sending.Id,
                    sending.Name,
                    sending.Company.Name,
                    sending.ImagePath,
                    sending.SendingScores.Count > 0 ? sending.SendingScores.First().CurrentSubscriber : 0,
                    sending.SendingScores.Count > 0 ? sending.SendingScores.First().ReviewScore : 0
                ));
            }

            var resultDto = new SendingByCategoryDto(
                categoryId,
                categoryResult.Value.Name,
                sendingDtos
            );

            return Result<SendingByCategoryDto>.Success(resultDto);
        }

        public async Task<Result<IEnumerable<SendingByCategoryDto>>> GetFewSendingByCategoryAsync()
        {
            var categories = await _sendingCategoryRepository.GetAllSendingCategoriesAsync();
            if (categories.IsFailure)
            {
                return Result<IEnumerable<SendingByCategoryDto>>.Failure(categories.Error);
            }
            var result = new List<SendingByCategoryDto>();
            foreach (var category in categories.Value)
            {
                var sendings = await _sendingRepository.GetSendingsBySendingCategoryIdAsync(category.Id);
                if (sendings.IsFailure)
                {
                    return Result<IEnumerable<SendingByCategoryDto>>.Failure(sendings.Error);
                }
                var randomSendings = new List<SendingWithoutDetailsDto>();
                foreach (var sending in sendings.Value.Where(s => !s.IsDeleted).OrderBy(_ => Guid.NewGuid()).Take(12))
                {
                    var sendingScore = await _sendingScoreRepository.GetSendingScoreBySendingIdAsync(sending.Id);
                    var sendingDto = new SendingWithoutDetailsDto(
                        sending.Id,
                        sending.Name,
                        sending.Company.Name,
                        sending.ImagePath,
                        sendingScore.IsSuccess ? sendingScore.Value.CurrentSubscriber : 0,
                        sendingScore.IsSuccess ? sendingScore.Value.ReviewScore : 0
                    );
                    randomSendings.Add(sendingDto);
                }
                var categoryDto = new SendingByCategoryDto(
                    category.Id,
                    category.Name,
                    randomSendings
                );

                result.Add(categoryDto);


            }
            return Result<IEnumerable<SendingByCategoryDto>>.Success(result);

        }

    }
}
