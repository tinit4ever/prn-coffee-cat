using System;
using System.Collections.Generic;

namespace Entities;

public partial class Shop
{
    public int ShopId { get; set; }

    public string ShopName { get; set; } = null!;

    public string? ShopEmail { get; set; }

    public string? ShopAddress { get; set; }

    public string? ShopTelephone { get; set; }

    public string? ShopImage { get; set; }

    public bool? ShopEnabled { get; set; }

    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();

    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    public virtual ICollection<ShopVoucher> ShopVouchers { get; set; } = new List<ShopVoucher>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
