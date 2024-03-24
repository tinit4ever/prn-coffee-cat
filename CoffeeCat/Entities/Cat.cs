using System;
using System.Collections.Generic;

namespace Entities;

public partial class Cat
{
    public int CatId { get; set; }

    public string CatName { get; set; } = null!;

    public string? CatImage { get; set; } = null;

    public bool? CatEnabled { get; set; }

    public int? AreaId { get; set; }

    public virtual Area? Area { get; set; }
}
