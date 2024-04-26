using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class SendingTime
{
    public int Id { get; set; }

    public TimeOnly SendTime { get; set; }

    public virtual ICollection<Sending> Sendings { get; set; } = new List<Sending>();
}
