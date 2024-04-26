using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UserInfo
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string HashedPassword { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<UserRecoveryToken> UserRecoveryTokens { get; set; } = new List<UserRecoveryToken>();

    public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; } = new List<UserRefreshToken>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public virtual ICollection<UserSending> UserSendings { get; set; } = new List<UserSending>();
}
