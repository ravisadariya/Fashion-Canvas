using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TheFashionCanvas.Data;
using TheFashionCanvas.Models;
using TheFashionCanvas.ViewModels;

namespace TheFashionCanvas.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly FashionDbContext _context;

        public CartController(FashionDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var cart = await _context.Carts
                                     .Include(c => c.CartItems)
                                     .ThenInclude(ci => ci.Product)
                                     .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
            {
                return View(new List<CartItem>());
            }

            return View(cart.CartItems);
        }

        
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var cart = await _context.Carts
                                     .Include(c => c.CartItems)
                                     .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if (cartItem == null)
            {
                var product = await _context.Products.FindAsync(productId);

                if (product == null)
                {
                    return NotFound();
                }

                cartItem = new CartItem
                {
                    ProductId = productId,
                    CartId = cart.CartId,
                    Quantity = quantity,
                    UnitPrice = product.Price
                };
                cart.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Product successfully added to cart!";
            return RedirectToAction("Index", "Product");
        }

        
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);

            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Item successfully removed from cart!";
            return RedirectToAction("Index");
        }

       
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);

            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.Quantity = quantity;

            if (cartItem.Quantity <= 0)
            {
                _context.CartItems.Remove(cartItem);
                TempData["SuccessMessage"] = "Item successfully removed from cart!";
            }
            else
            {
                TempData["SuccessMessage"] = "Cart updated successfully!";
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var cart = await _context.Carts
                                     .Include(c => c.CartItems)
                                     .ThenInclude(ci => ci.Product)
                                     .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
            {
                return RedirectToAction("Index");
            }

            var user = await _context.Users.FindAsync(userId);

            decimal merchandiseTotal = cart.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice);
            decimal tax = merchandiseTotal * 0.13m;
            decimal shippingFee;

            if (merchandiseTotal < 100)
            {
                shippingFee = 50;
            }
            else if (merchandiseTotal >= 100 && merchandiseTotal <= 500)
            {
                shippingFee = 20;
            }
            else
            {
                shippingFee = 0;
            }

            decimal totalAmount = merchandiseTotal + tax + shippingFee;

            var viewModel = new CheckoutViewModel
            {
                CartItems = cart.CartItems.ToList(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                StreetAddress = user.StreetAddress,
                Apartment = user.Apartment,
                PostalCode = user.PostalCode,
                City = user.City,
                Province = user.Province,
                PhoneNumber = user.PhoneNumber,
                TotalAmount = totalAmount,
                Tax = tax,
                ShippingFee = shippingFee
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var cart = await _context.Carts
                                     .Include(c => c.CartItems)
                                     .ThenInclude(ci => ci.Product)
                                     .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
            {
                return RedirectToAction("Index");
            }

            var order = new Order
            {
                OrderDate = DateTime.Now,
                UserId = userId,
                ShippingAddress = model.StreetAddress + (string.IsNullOrEmpty(model.Apartment) ? "" : ", " + model.Apartment) + ", " + model.City + ", " + model.Province + ", " + model.PostalCode,
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.UnitPrice
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Your order has been placed successfully!";

            return RedirectToAction("OrderConfirmation", new { id = order.OrderId });
        }

      
        public async Task<IActionResult> OrderConfirmation(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
