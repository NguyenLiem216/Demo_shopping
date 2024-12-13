using Demo_shopping.Data;
using Demo_shopping.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo_shopping.SeedData
{
    public class SeedData
    {
        public static void SeedingData(DataContext _context)
        {
            _context.Database.Migrate();
            if (!_context.Products.Any())
            {
                CategoriesModel macbook = new CategoriesModel
                {
                    Name = "Macbook",
                    Slug = "macbook",
                    Description = "Macbook is Large Product in the world",
                    Status = 1
                };
                CategoriesModel pc = new CategoriesModel
                {
                    Name = "PC",
                    Slug = "pc",
                    Description = "PC is Large Product in the world",
                    Status = 1
                };
                BrandsModel apple = new BrandsModel
                {
                    Name = "Apple",
                    Slug = "apple",
                    Description = "Apple is Large Brand in the world",
                    Status = 1
                };
                BrandsModel samsung = new BrandsModel
                {
                    Name = "Samsung",
                    Slug = "samsung",
                    Description = "Samsung is Large Brand in the world",
                    Status = 1
                };
                _context.Products.AddRange(
                    new ProductsModel
                    {
                        Name = "Macbook",
                        Slug = "macbook",
                        Description = "Macbook is Best",
                        Image = "1.jpg",
                        Category = macbook,
                        Price = 1223,
                        Brand = apple
                    },
                    new ProductsModel
                    {
                        Name = "PC",
                        Slug = "pc",
                        Description = "PC is Best",
                        Image = "1.jpg",
                        Category = pc,
                        Price = 1223,
                        Brand = samsung
                    }
                    );
                _context.SaveChanges();
            }
        }
    }
}
