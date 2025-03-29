using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheFashionCanvas.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TheFashionCanvas.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly FashionDbContext _context;

        public HomeController(FashionDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            var totalOrders = orders.Count();
            var totalProducts = await _context.Products.CountAsync();
            var totalCategories = await _context.Categories.CountAsync();
            var totalRevenue = orders.Sum(o => o.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice));

            var recentOrders = orders.OrderByDescending(o => o.OrderDate).Take(5).ToList();
            var recentProducts = await _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.ProductId)
                .Take(4)
                .ToListAsync();

            ViewBag.TotalOrders = totalOrders;
            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalCategories = totalCategories;
            ViewBag.TotalRevenue = totalRevenue;
            ViewBag.RecentOrders = recentOrders;
            ViewBag.RecentProducts = recentProducts;

            return View();
        }
    }
}
