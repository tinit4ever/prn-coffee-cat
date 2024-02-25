using System;
using System.Collections.Generic;

namespace Entities;

public partial class MenuItem
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public decimal? ItemPrice { get; set; }

    public bool? ItemEnabled { get; set; }

    public int? MenuId { get; set; }

    public virtual Menu? Menu { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
