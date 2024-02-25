using System;
using System.Collections.Generic;

namespace Entities;

public partial class Menu
{
    public int MenuId { get; set; }

    public string MenuName { get; set; } = null!;

    public bool? MenuEnabled { get; set; }

    public int? ShopId { get; set; }

    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    public virtual Shop? Shop { get; set; }
}
