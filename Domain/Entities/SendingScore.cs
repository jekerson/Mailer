using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class SendingScore
{
    public int Id { get; set; }

    public int? SendingId { get; set; }

    public decimal? ReviewScore { get; set; }

    public int? CurrentSubscriber { get; set; }

    public virtual Sending? Sending { get; set; }
}
