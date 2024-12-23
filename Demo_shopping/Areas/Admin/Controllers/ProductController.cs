using Demo_shopping.Data;
using Demo_shopping.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Demo_shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductController(DataContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                            .Include(p => p.Category)
                            .Include(p => p.Brand)
                            .OrderByDescending(p => p.Id)
                            .ToListAsync();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Brands = new SelectList(_context.Brands, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductsModel products)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", products.CategoryId);
            ViewBag.Brands = new SelectList(_context.Brands, "Id", "Name", products.BrandId);

            if (ModelState.IsValid)
            {
                //Them du lieu
                products.Slug = products.Name.Replace(" ", "-");
                var slug = await _context.Products.FirstOrDefaultAsync(p => p.Slug == products.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Sản phẩm đã có trong database");
                    return View(products);
                }
                else
                {
                    if (products.ImageUpload != null)
                    {
                        string uploadDir = Path.Combine(_environment.WebRootPath, "media/products");
                        string imageName = Guid.NewGuid().ToString() + "_" + products.ImageUpload.FileName;
                        string filePath = Path.Combine(uploadDir, imageName);


                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        await products.ImageUpload.CopyToAsync(fs);
                        fs.Close();

                        products.Image = imageName;
                    }
                }
                _context.Add(products);
                await _context.SaveChangesAsync();
                TempData["success"] = "Thêm sản phẩm thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Model đang bị lỗi";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }
            return View(products);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            ProductsModel product = await _context.Products.FindAsync(Id);
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_context.Brands, "Id", "Name", product.BrandId);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductsModel products)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", products.CategoryId);
            ViewBag.Brands = new SelectList(_context.Brands, "Id", "Name", products.BrandId);

            var existed_product = _context.Products.Find(products.Id);

            if (ModelState.IsValid)
            {
                //Them du lieu
                products.Slug = products.Name.Replace(" ", "-");
                var slug = await _context.Products.FirstOrDefaultAsync(p => p.Slug == products.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Sản phẩm đã có trong database");
                    return View(products);
                }
                if (products.ImageUpload != null)
                {
                    //upload new image
                    string uploadDir = Path.Combine(_environment.WebRootPath, "media/products");
                    string imageName = Guid.NewGuid().ToString() + "_" + products.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);

                    //delete old picture
                    string oldFilePath = Path.Combine(uploadDir, existed_product.Image);

                    try
                    {
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "An error occured while deleting the product image.");
                    }


                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await products.ImageUpload.CopyToAsync(fs);
                    fs.Close();

                    existed_product.Image = imageName;
                }
                existed_product.Name = products.Name;
                existed_product.Description = products.Description;
                existed_product.Price = products.Price;
                existed_product.CategoryId = products.CategoryId;
                existed_product.BrandId = products.BrandId;
                _context.Update(existed_product);
                await _context.SaveChangesAsync();
                TempData["success"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Model đang bị lỗi";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }
            return View(products);
        }


        public async Task<IActionResult> Delete(int Id)
        {
            ProductsModel products = await _context.Products.FindAsync(Id);
            if (!string.Equals(products.Image, "noname.jpg"))
            {
                string uploadDir = Path.Combine(_environment.WebRootPath, "media/products");
                string oldFileImage = Path.Combine(uploadDir, products.Image);

                if (System.IO.File.Exists(oldFileImage))
                {
                    System.IO.File.Delete(oldFileImage);
                }

            }
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            TempData["error"] = "Sản phẩm đã xóa";

            return RedirectToAction("Index");
        }
    }
}
