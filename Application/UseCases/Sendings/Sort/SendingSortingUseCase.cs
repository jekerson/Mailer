using Application.DTOs.Sendings;
using Application.UseCases.Sendings.Category;
using Domain.Abstraction;
using Domain.Interfaces.Sendings;

namespace Application.UseCases.Sendings.Sort
{
    public class SendingSortingUseCase : ISendingSortingUseCase
    {
        private readonly ISendingRepository _sendingRepository;
        private readonly ISendingCategoryRepository _sendingCategoryRepository;
        private readonly ISendingScoreRepository _sendingScoreRepository;
        private readonly ISendingCategoryUseCase _sendingCategoryUseCase;

        public SendingSortingUseCase(
            ISendingRepository sendingRepository,
            ISendingCategoryRepository sendingCategoryRepository,
            ISendingScoreRepository sendingScoreRepository,
            ISendingCategoryUseCase sendingCategoryUseCase)
        {
            _sendingRepository = sendingRepository;
            _sendingCategoryRepository = sendingCategoryRepository;
            _sendingScoreRepository = sendingScoreRepository;
            _sendingCategoryUseCase = sendingCategoryUseCase;
        }
        public async Task<Result<IEnumerable<SendingByCategoryDto>>> GetSendingByCompany(int companyId)
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

                var companySendings = sendings.Value.Where(s => s.CompanyId == companyId && !s.IsDeleted);

                var sendingDtos = new List<SendingWithoutDetailsDto>();
                foreach (var sending in companySendings.OrderBy(_ => Guid.NewGuid()).Take(12))
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
                    sendingDtos.Add(sendingDto);
                }

                if (sendingDtos.Any())
                {
                    var categoryDto = new SendingByCategoryDto(
                        category.Id,
                        category.Name,
                        sendingDtos
                    );

                    result.Add(categoryDto);
                }
            }

            return Result<IEnumerable<SendingByCategoryDto>>.Success(result);
        }
        public async Task<Result<SendingByCategoryDto>> GetSendingsByCategorySortedByReviewsAsync(int categoryId)
        {
            var result = await _sendingCategoryUseCase.GetSendingsByCategoryAsync(categoryId);
            if (result.IsFailure)
            {
                return Result<SendingByCategoryDto>.Failure(result.Error);
            }

            var sortedSendings = result.Value.Sendings.OrderByDescending(s => s.AverageRating).ToList();

            var sortedResult = new SendingByCategoryDto(
                result.Value.CategoryId,
                result.Value.CategoryName,
                sortedSendings
            );

            return Result<SendingByCategoryDto>.Success(sortedResult);
        }

        public async Task<Result<SendingByCategoryDto>> GetSendingsByCategorySortedBySubscribersAsync(int categoryId)
        {
            var result = await _sendingCategoryUseCase.GetSendingsByCategoryAsync(categoryId);
            if (result.IsFailure)
            {
                return Result<SendingByCategoryDto>.Failure(result.Error);
            }

            var sortedSendings = result.Value.Sendings.OrderByDescending(s => s.CountSibscribers).ToList();

            var sortedResult = new SendingByCategoryDto(
                result.Value.CategoryId,
                result.Value.CategoryName,
                sortedSendings
            );

            return Result<SendingByCategoryDto>.Success(sortedResult);
        }
    }
}
