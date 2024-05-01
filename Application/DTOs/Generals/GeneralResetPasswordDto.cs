namespace Application.DTOs.Generals
{
    public record GeneralResetPasswordDto(string Email, string Password, string NewPassword);
}
