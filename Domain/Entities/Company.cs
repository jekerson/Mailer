namespace Domain.Entities;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ImagePath { get; set; } = null!;

    public string HashedPassword { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<CompanyRecoveryToken> CompanyRecoveryTokens { get; set; } = new List<CompanyRecoveryToken>();

    public virtual ICollection<CompanyRefreshToken> CompanyRefreshTokens { get; set; } = new List<CompanyRefreshToken>();

    public virtual ICollection<CompanyRole> CompanyRoles { get; set; } = new List<CompanyRole>();

    public virtual ICollection<Sending> Sendings { get; set; } = new List<Sending>();
}
