using System;
using System.Collections.Generic;

namespace Entities;

public partial class ShopVoucher
{
    public int VoucherId { get; set; }

    public string VoucherCode { get; set; } = null!;

    public decimal? VoucherDiscount { get; set; }

    public DateOnly? VoucherExpiryDate { get; set; }

    public bool? VoucherEnabled { get; set; }

    public int? CoffeeShopId { get; set; }

    public virtual Shop? CoffeeShop { get; set; }

    public virtual ICollection<UserVoucher> UserVouchers { get; set; } = new List<UserVoucher>();
}
