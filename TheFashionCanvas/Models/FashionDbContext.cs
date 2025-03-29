using Microsoft.EntityFrameworkCore;
using TheFashionCanvas.Models;

namespace TheFashionCanvas.Data
{
    public class FashionDbContext : DbContext
    {
        public FashionDbContext(DbContextOptions<FashionDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Carts)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.CartItems)
                .WithOne(ci => ci.Product)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .Property(ci => ci.UnitPrice)
                .HasColumnType("decimal(18,2)");

            
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)");

            
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = "1",  
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@fashioncanvas.com",
                Username = "admin",
                Password = "Admin@123", 
                Role = "Admin",
                StreetAddress = "123 Admin Street",
                Apartment = "Apt 1",
                PostalCode = "12345",
                City = "Admin City",
                Province = "Admin Province",
                PhoneNumber = "1234567890"
            });
            modelBuilder.Entity<Category>().HasData(
           new Category { CategoryId = 1, Name = "menClothing" },
           new Category { CategoryId = 2, Name = "menClothing" },
           new Category { CategoryId = 3, Name = "Accessories" },
           new Category { CategoryId = 4, Name = "Footwear" }
       );
            modelBuilder.Entity<Product>().HasData(
             new Product
             {
                 ProductId = 1,
                 Name = "menT-Shirt",
                 Description = "Comfortable cotton t-shirt",
                 CategoryId = 1,
                 Stock = 100,
                 Price = 19.99m,
                 ImagePath = "mens_tshirt.jpg"
             },
             new Product
             {
                 ProductId = 2,
                 Name = "menDress",
                 Description = "Elegant evening dress",
                 CategoryId = 2,
                 Stock = 50,
                 Price = 49.99m,
                 ImagePath = "mens_dress.jpg"
             },
             new Product
             {
                 ProductId = 3,
                 Name = "Sunglasses",
                 Description = "Stylish sunglasses with UV protection",
                 CategoryId = 3,
                 Stock = 200,
                 Price = 29.99m,
                 ImagePath = "sunglasses.jpg"
             },
             new Product
             {
                 ProductId = 4,
                 Name = "Running Shoes",
                 Description = "Lightweight running shoes",
                 CategoryId = 4,
                 Stock = 150,
                 Price = 59.99m,
                 ImagePath = "running_shoes.jpg"
             },
             new Product
             {
                 ProductId = 5,
                 Name = "menJeans",
                 Description = "Classic blue denim jeans",
                 CategoryId = 1,
                 Stock = 80,
                 Price = 39.99m,
                 ImagePath = "mens_jeans.jpg"
             },
             new Product
             {
                 ProductId = 6,
                 Name = "menHandbag",
                 Description = "Stylish leather handbag",
                 CategoryId = 2,
                 Stock = 70,
                 Price = 79.99m,
                 ImagePath = "mens_handbag.jpg"
             },
             new Product
             {
                 ProductId = 7,
                 Name = "menWatch",
                 Description = "Elegant wristwatch",
                 CategoryId = 3,
                 Stock = 60,
                 Price = 199.99m,
                 ImagePath = "mens_watch.jpg"
             },
             new Product
             {
                 ProductId = 8,
                 Name = "menSandals",
                 Description = "Comfortable summer sandals",
                 CategoryId = 4,
                 Stock = 6,
                 Price = 34.99m,
                 ImagePath = "mens_sandals.jpg"
             },
             new Product
             {
                 ProductId = 9,
                 Name = "menJacket",
                 Description = "Warm winter jacket",
                 CategoryId = 1,
                 Stock = 4,
                 Price = 99.99m,
                 ImagePath = "mens_jacket.jpg"
             },
             new Product
             {
                 ProductId = 10,
                 Name = "menScarf",
                 Description = "Silk scarf",
                 CategoryId = 2,
                 Stock = 120,
                 Price = 24.99m,
                 ImagePath = "mens_scarf.jpg"
             }
         );
        }
    }
}
