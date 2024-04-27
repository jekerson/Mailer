namespace Domain.Entities;

public partial class Review
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<SendingReview> SendingReviews { get; set; } = new List<SendingReview>();

    public virtual UserInfo? User { get; set; }
}
