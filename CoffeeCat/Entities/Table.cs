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

    public int? AreaId { get; set; }

    public virtual Area? Area { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
