using System;
using System.Collections.Generic;

namespace Entities;

public partial class MenuItem
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public decimal? ItemPrice { get; set; }

    public bool? ItemEnabled { get; set; }

    public int? ShopId { get; set; }

    public virtual Shop? Shop { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
