﻿using System;
using System.Collections.Generic;

namespace Demo_shopping.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string OrderCode { get; set; }

    public long ProductId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}
