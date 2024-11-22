﻿using Demo_shopping.Models;
using Demo_shopping.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

namespace Demo_shopping.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<BrandsModel> Brands { get; set; }
        public DbSet<ProductsModel> Products { get; set; }
        public DbSet<CategoriesModel> Categories { get; set; }
    }
}
