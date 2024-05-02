namespace Application.DTOs.Sendings
{
    public record SendingByCategoryDto(
        int CategoryId,
        string CategoryName,
        IEnumerable<SendingWithoutDetailsDto> Sendings);
}
