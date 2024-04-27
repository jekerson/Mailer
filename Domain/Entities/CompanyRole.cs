namespace Domain.Entities;

public partial class CompanyRole
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public int RoleId { get; set; }

    public virtual Company? Company { get; set; }

    public virtual Role? Role { get; set; }
}
