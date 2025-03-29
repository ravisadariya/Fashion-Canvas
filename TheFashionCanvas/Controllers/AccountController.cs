using Microsoft.AspNetCore.Mvc;
using TheFashionCanvas.Data;
using TheFashionCanvas.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using TheFashionCanvas.Models;
using Microsoft.EntityFrameworkCore;

namespace TheFashionCanvas.Controllers
{
    public class AccountController : Controller
    {
        private readonly FashionDbContext _context;

        public AccountController(FashionDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                if (await _context.Users.AnyAsync(u => u.Username == model.Username || u.Email == model.Email))
                {
                    ModelState.AddModelError(string.Empty, "Username or Email already exists.");
                    return View(model);
                }

                
                var existingUserIds = await _context.Users
                    .Select(u => u.UserId)
                    .ToListAsync();

                string nextId;
                if (existingUserIds.Any())
                {
                    
                    int maxId = existingUserIds
                        .Select(id => int.TryParse(id, out var num) ? num : 0)
                        .Max();

                    nextId = (maxId + 1).ToString(); 
                }
                else
                {
                    nextId = "1"; 
                }

                var user = new User
                {
                    UserId = nextId, 
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Username = model.Username,
                    Password = model.Password, 
                    Role = "Customer",
                    StreetAddress = model.StreetAddress,
                    Apartment = model.Apartment,
                    PostalCode = model.PostalCode,
                    City = model.City,
                    Province = model.Province,
                    PhoneNumber = model.PhoneNumber
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Registration successful! Please login.";

                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .Where(u => u.Username == model.Username && u.Password == model.Password)
                    .Select(u => new
                    {
                        u.Username,
                        u.Role,
                        u.UserId
                    })
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.UserId)
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        // Additional properties if needed
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    TempData["SuccessMessage"] = $"Welcome back, {user.Username}!";

                    if (user.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else if (user.Role == "Customer")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid username or password."; // Set the error message
                }
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
