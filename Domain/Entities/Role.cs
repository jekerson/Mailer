namespace Domain.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<CompanyRole> CompanyRoles { get; set; } = new List<CompanyRole>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
