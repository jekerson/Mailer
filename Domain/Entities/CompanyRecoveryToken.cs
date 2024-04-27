namespace Domain.Entities;

public partial class CompanyRecoveryToken
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public string IpAddress { get; set; } = null!;

    public string UserAgent { get; set; } = null!;

    public virtual Company? Company { get; set; }
}
