namespace Application.DTOs.Sendings
{
    public record SendingWithoutDetailsDto(
        int Id,
        string Name,
        string CompanyName,
        string ImagePath,
        int CountSibscribers,
        decimal AverageRating);
}
