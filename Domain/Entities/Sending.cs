using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Sending
{
    public int Id { get; set; }

    public int? SendingTypeId { get; set; }

    public int? SendingTimeId { get; set; }

    public int? SendingCategoryId { get; set; }

    public int? CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ImagePath { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual Company? Company { get; set; }

    public virtual SendingCategory? SendingCategory { get; set; }

    public virtual ICollection<SendingContent> SendingContents { get; set; } = new List<SendingContent>();

    public virtual ICollection<SendingReview> SendingReviews { get; set; } = new List<SendingReview>();

    public virtual ICollection<SendingScore> SendingScores { get; set; } = new List<SendingScore>();

    public virtual SendingTime? SendingTime { get; set; }

    public virtual SendingType? SendingType { get; set; }

    public virtual ICollection<UserSending> UserSendings { get; set; } = new List<UserSending>();
}
