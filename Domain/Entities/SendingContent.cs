namespace Domain.Entities;

public partial class SendingContent
{
    public int Id { get; set; }

    public int SendingId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly SendingDate { get; set; }

    public string? Content { get; set; }

    public bool IsApproved { get; set; }

    public virtual Sending? Sending { get; set; }
}
