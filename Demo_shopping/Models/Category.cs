﻿using System;
using System.Collections.Generic;

namespace Demo_shopping.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Slug { get; set; }

    public int Status { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
