using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheFashionCanvas.Data;
using TheFashionCanvas.Models;
using System.Linq;
using System.Threading.Tasks;

namespace TheFashionCanvas.Controllers
{
    public class ProductController : Controller
    {
        private readonly FashionDbContext _context;

        public ProductController(FashionDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index(int? categoryId, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            var products = from p in _context.Products.Include(p => p.Category)
                           select p;

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "Price":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(await products.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
