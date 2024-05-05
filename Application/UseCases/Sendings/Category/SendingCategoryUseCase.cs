using Application.Abstraction.Pagging;
using Application.DTOs.Sendings;
using Domain.Abstraction;
using Domain.Entities;
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

        public async Task<Result<SendingByCategoryDto>> GetSendingsByCategoryAsync(
            int categoryId,
            int page, PageSizeType pageSize,
            SortingType sortingType = SortingType.None)
        {
            var categoryResult = await _sendingCategoryRepository.GetSendingCategoryByIdAsync(categoryId);
            if (categoryResult.IsFailure)
            {
                return Result<SendingByCategoryDto>.Failure(categoryResult.Error);
            }

            var sendingsResult = await _sendingRepository.GetSendingsBySendingCategoryIdAsync(categoryId);

            var sendings = sendingsResult.Value.Where(s => !s.IsDeleted);

            switch (sortingType)
            {
                case SortingType.ByReview:
                    sendings = sendings.OrderByDescending(s => s.SendingScores.FirstOrDefault()?.ReviewScore ?? 0);
                    break;
                case SortingType.BySubscriber:
                    sendings = sendings.OrderByDescending(s => s.SendingScores.FirstOrDefault()?.CurrentSubscriber ?? 0);
                    break;
            }

            var pagedSendings = PagedList<Sending>.ToPagedList(sendings.AsQueryable(), page, pageSize);

            var sendingDtos = new List<SendingWithoutDetailsDto>();
            foreach (var sending in pagedSendings)
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
                sendingDtos,
                pagedSendings.TotalPages
            );

            return Result<SendingByCategoryDto>.Success(resultDto);
        }


        public async Task<Result<IEnumerable<SendingByCategoryDto>>> GetSomeSendingByCategoryAsync(int? CompanyId = null)
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

                IEnumerable<Sending> filteredSendings;

                if (CompanyId.HasValue)
                {
                    filteredSendings = sendings.Value.Where(s => s.CompanyId == CompanyId.Value && !s.IsDeleted);
                }
                else
                {
                    filteredSendings = sendings.Value.Where(s => !s.IsDeleted);
                }

                var randomSendings = new List<SendingWithoutDetailsDto>();

                foreach (var sending in filteredSendings.OrderBy(_ => Guid.NewGuid()).Take(CompanyId.HasValue ? 50 : 12))
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

                if (randomSendings.Any())
                {
                    var categoryDto = new SendingByCategoryDto(
                        category.Id,
                        category.Name,
                        randomSendings,
                        0
                    );

                    result.Add(categoryDto);
                }
            }

            return Result<IEnumerable<SendingByCategoryDto>>.Success(result);
        }


    }

    public enum SortingType
    {
        None,
        ByReview,
        BySubscriber

    }
}
