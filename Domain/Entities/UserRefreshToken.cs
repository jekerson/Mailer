using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UserRefreshToken
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual UserInfo? User { get; set; }
}
