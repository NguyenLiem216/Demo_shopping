using System;
using System.Collections.Generic;

namespace Demo_shopping.Models;

public partial class Order
{
    public int Id { get; set; }

    public string OrderCode { get; set; }

    public string UserName { get; set; }

    public DateTime CreatedDate { get; set; }

    public int Status { get; set; }
}
