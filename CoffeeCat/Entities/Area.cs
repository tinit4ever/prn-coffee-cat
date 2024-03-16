using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities;

public partial class Area
{
    [Required(ErrorMessage = "Area name is required.")]
    [RegularExpression(@"^[\w\d\s]+$", ErrorMessage = "Area name cannot contain special characters.")]
    public int AreaId { get; set; }

    public string AreaName { get; set; } = null!;

    public bool? AreaEnabled { get; set; }

    public int? ShopId { get; set; }

    public virtual ICollection<Cat> Cats { get; set; } = new List<Cat>();

    public virtual Shop? Shop { get; set; }

    public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
}
