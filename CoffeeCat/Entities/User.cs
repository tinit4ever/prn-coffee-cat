namespace Entities;
using System.ComponentModel.DataAnnotations;

public partial class User {
    public int CustomerId { get; set; }

    [Display(Name = "Name")]
    [StringLength(23, ErrorMessage = "Range must be from 1 to 23")]
    public string CustomerName { get; set; } = null!;

    [Display(Name = "Email")]
    [StringLength(23, ErrorMessage = "Range must be from 1 to 23")]
    public string CustomerEmail { get; set; } = null!;

    [Display(Name = "Password")]
    [StringLength(23, ErrorMessage = "Range must be from 1 to 23")]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{8,}$", ErrorMessage = "Password must contain at least 8 characters, including one uppercase letter, one lowercase letter, one number, and one special character")]
    public string CustomerPassword { get; set; } = null!;

    public string? CustomerTelephone { get; set; }

    public bool? CustomerEnabled { get; set; }

    public int? RoleId { get; set; }

    public int? ShopId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Role? Role { get; set; }

    public virtual Shop? Shop { get; set; }
}
