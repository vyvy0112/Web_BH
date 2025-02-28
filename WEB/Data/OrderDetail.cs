﻿using System;
using System.Collections.Generic;

namespace WEB.Data;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public double? Price { get; set; }

    public double? Total { get; set; }

    public double? Discount { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
