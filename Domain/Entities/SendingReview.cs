namespace Domain.Entities;

public partial class SendingReview
{
    public int Id { get; set; }

    public int SendingId { get; set; }

    public int ReviewId { get; set; }

    public virtual Review? Review { get; set; }

    public virtual Sending? Sending { get; set; }
}
