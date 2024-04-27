namespace Domain.Entities;

public partial class UserSending
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SendingId { get; set; }

    public virtual Sending? Sending { get; set; }

    public virtual UserInfo? User { get; set; }
}
