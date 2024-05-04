namespace Application.DTOs.Generals
{
    public record GeneralPasswordRecoveryDto(
        string Email,
        string IpAddress,
        string UserAgent);
}
