using System;
using System.Collections.Generic;

namespace Entities;

public partial class Cat
{
    public int CatId { get; set; }

    public string CatName { get; set; } = null!;

    public string? CatImage { get; set; }

    public bool? CatEnabled { get; set; }

    public int? ShopId { get; set; }

    public virtual Shop? Shop { get; set; }
}
