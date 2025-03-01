using System;
using System.Collections.Generic;

namespace WEB.Data;

public partial class Status
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public string? Deccription { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
