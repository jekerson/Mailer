using Application.DTOs.Sendings;
using Application.Validators.Sendings;
using Domain.Abstraction;
using Domain.Entities;
using Domain.Interfaces.Files;
using Domain.Interfaces.Sendings;


namespace Application.UseCases.Companies.Sendings.Managment
{
    public class SendingManagmentUseCase : ISendingManagmentUseCase
    {
        private readonly ISendingRepository _sendingRepository;
        private readonly IImageStorageRepository _imageStorageRepository;
        private readonly ISendingValidator _sendingValidator;
        private readonly ISendingScoreRepository _sendingScoreRepository;
        private readonly ISendingContentRepository _sendingContentRepository;

        public SendingManagmentUseCase(
            ISendingRepository sendingRepository,
            IImageStorageRepository imageStorageRepository,
            ISendingValidator sendingValidator,
            ISendingScoreRepository sendingScoreRepository,
            ISendingContentRepository sendingContentRepository)
        {
            _sendingRepository = sendingRepository;
            _imageStorageRepository = imageStorageRepository;
            _sendingValidator = sendingValidator;
            _sendingScoreRepository = sendingScoreRepository;
            _sendingContentRepository = sendingContentRepository;
        }
        public async Task<Result> CreateSendingAsync(CreateSendingDto createSendingDto)
        {
            // Валидация входных данных
            var validationResult = await _sendingValidator.ValidateAsync(createSendingDto);
            if (validationResult.IsFailure)
            {
                return Result.Failure(validationResult.Error);
            }

            // Создание новой рассылки без логотипа
            var sending = new Sending
            {
                Name = createSendingDto.Name,
                Description = createSendingDto.Description,
                SendingTypeId = createSendingDto.SendingTypeId,
                SendingTimeId = createSendingDto.SendingTimeId,
                SendingCategoryId = createSendingDto.CategoryId,
                CompanyId = createSendingDto.CompanyId,
                ImagePath = string.Empty,
            };

            var addSendingResult = await _sendingRepository.AddSendingAsync(sending);
            if (addSendingResult.IsFailure)
            {
                return Result.Failure(addSendingResult.Error);
            }

            // Сохранение логотипа рассылки с реальным идентификатором
            var logoResult = await _imageStorageRepository.SaveSendingLogoAsync(createSendingDto.Logo, sending.Id);
            if (logoResult.IsFailure)
            {
                // Удаление рассылки в случае ошибки сохранения логотипа
                await _sendingRepository.DeleteSendingAsync(sending.Id);
                return Result<int>.Failure(logoResult.Error);
            }

            // Обновление пути к изображению в рассылке
            sending.ImagePath = logoResult.Value;
            var updateResult = await _sendingRepository.UpdateSendingAsync(sending);
            if (updateResult.IsFailure)
            {
                await _sendingRepository.DeleteSendingAsync(sending.Id);
                return Result.Failure(updateResult.Error);
            }

            int startDay = DateTime.Now.Hour < 17 ? 0 : 1;

            // Создание контент-плана на 5 дней вперед
            for (int i = startDay; i < startDay + 5; i++)
            {
                var sendingContent = new SendingContent
                {
                    SendingId = sending.Id,
                    Name = $"План на {DateTime.Now.AddDays(i).ToShortDateString()}",
                    SendingDate = DateOnly.FromDateTime(DateTime.Now.AddDays(i)),
                    Content = "", // По умолчанию контент не задан
                    IsApproved = false
                };

                var contentResult = await _sendingContentRepository.AddSendingContentAsync(sendingContent);
                if (contentResult.IsFailure)
                {
                    await _sendingRepository.DeleteSendingAsync(sending.Id);
                    return Result.Failure(contentResult.Error);
                }
            }

            var sendingScore = new SendingScore
            {
                SendingId = sending.Id,
                ReviewScore = 0,
                CurrentSubscriber = 0
            };

            await _sendingScoreRepository.AddSendingScoreAsync(sendingScore);

            return Result.Success();
        }

        public async Task<Result> DeleteSendingAsync(DeleteSendingDto deleteSendingDto)
        {
            return await _sendingRepository.DeleteSendingAsync(deleteSendingDto.SendingId);
        }
    }
}
