namespace Application.DTOs.Reviews
{
    public record CreateReviewDto(
        int UserId,
        int SendingId,
        int Rating,
        string Comment);
}
