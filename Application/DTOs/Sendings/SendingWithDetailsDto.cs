namespace Application.DTOs.Sendings
{
    public record SendingWithDetailsDto(
        int SendingId,
        string SendingName,
        string SendingDescription,
        string SendingImagePath,
        int CountSubscribers,
        decimal AverageRating,
        int CompanyId,
        string CompanyName,
        string CompanyDescription,
        string CompanyImagePath);
}
