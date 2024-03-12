using System;
using System.Collections.Generic;

namespace Entities;

public partial class Booking
{
    public int BookingId { get; set; }

    public string BookingCode { get; set; } = null!;

    public DateTime? BookingStartTime { get; set; } 

    public DateTime? BookingEndTime { get; set; }

    public bool? BookingEnabled { get; set; }

    public int? CustomerId { get; set; }
 
    public virtual User? Customer { get; set; }

    public virtual ICollection<MenuItem> Items { get; set; } = new List<MenuItem>();

    public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
}
