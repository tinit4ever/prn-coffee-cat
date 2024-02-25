using System;
using System.Collections.Generic;

namespace Entities;

public partial class UserVoucher
{
    public int UserVoucherId { get; set; }

    public int? VoucherId { get; set; }

    public bool? Used { get; set; }

    public bool? VoucherEnabled { get; set; }

    public virtual ShopVoucher? Voucher { get; set; }
}
