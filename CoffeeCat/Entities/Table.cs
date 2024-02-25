using System;
using System.Collections.Generic;

namespace Entities;

public partial class Table
{
    public int TableId { get; set; }

    public string TableName { get; set; } = null!;

    public int? TableCapacity { get; set; }

    public bool? TableStatus { get; set; }

    public bool? TableEnabled { get; set; }

    public int? ShopId { get; set; }

    public virtual Shop? Shop { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
