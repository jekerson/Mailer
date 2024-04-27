namespace Domain.Entities;

public partial class SendingCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Sending> Sendings { get; set; } = new List<Sending>();
}
