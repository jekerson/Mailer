namespace Domain.Entities;

public partial class CompanyRefreshToken
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Company? Company { get; set; }
}
