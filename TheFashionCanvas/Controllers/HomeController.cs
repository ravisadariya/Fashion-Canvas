using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using TheFashionCanvas.Data;
using TheFashionCanvas.Models;

namespace TheFashionCanvas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FashionDbContext _context;  // Inject the database context

        public HomeController(ILogger<HomeController> logger, FashionDbContext context)
        {
            _logger = logger;
            _context = context;  // Initialize the context
        }

        public IActionResult Index()
        {
            // Retrieve featured products (for example, first 3 products)
            var featuredProducts = _context.Products.Take(4).ToList();

            // Pass the products to the view
            return View(featuredProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
