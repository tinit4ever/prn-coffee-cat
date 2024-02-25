using System;
using System.Collections.Generic;

namespace Entities;

public partial class User
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string CustomerEmail { get; set; } = null!;

    public string CustomerPassword { get; set; } = null!;

    public string? CustomerTelephone { get; set; }

    public bool? CustomerEnabled { get; set; }

    public int? RoleId { get; set; }

    public int? ShopId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Role? Role { get; set; }

    public virtual Shop? Shop { get; set; }
}
