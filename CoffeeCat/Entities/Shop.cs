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

    public bool? ShopEnabled { get; set; }

    public string? ShopArea { get; set; }
    public string? ShopImage { get; set; }

    public virtual ICollection<Cat> Cats { get; set; } = new List<Cat>();

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

    public virtual ICollection<ShopVoucher> ShopVouchers { get; set; } = new List<ShopVoucher>();

    public virtual ICollection<Table> Tables { get; set; } = new List<Table>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
