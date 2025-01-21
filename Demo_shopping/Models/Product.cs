using System;
using System.Collections.Generic;

namespace Demo_shopping.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string Slug { get; set; }

    public int BrandId { get; set; }

    public int CategoryId { get; set; }

    public string Image { get; set; }

    public virtual Brand Brand { get; set; }

    public virtual Category Category { get; set; }
}
