﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheFashionCanvas.Data;

#nullable disable

namespace TheFashionCanvas.Migrations
{
    [DbContext(typeof(FashionDbContext))]
    [Migration("20240805021933_Initial1")]
    partial class Initial1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"), 1L, 1);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CartId");

                    b.HasIndex("UserId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("TheFashionCanvas.Models.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartItemId"), 1L, 1);

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CartItemId");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("TheFashionCanvas.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Name = "menClothing"
                        },
                        new
                        {
                            CategoryId = 2,
                            Name = "menClothing"
                        },
                        new
                        {
                            CategoryId = 3,
                            Name = "Accessories"
                        },
                        new
                        {
                            CategoryId = 4,
                            Name = "Footwear"
                        });
                });

            modelBuilder.Entity("TheFashionCanvas.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShippingAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TheFashionCanvas.Models.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"), 1L, 1);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("TheFashionCanvas.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            Description = "Comfortable cotton t-shirt",
                            ImagePath = "mens_tshirt.jpg",
                            Name = "menT-Shirt",
                            Price = 19.99m,
                            Stock = 100
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 2,
                            Description = "Elegant evening dress",
                            ImagePath = "mens_dress.jpg",
                            Name = "menDress",
                            Price = 49.99m,
                            Stock = 50
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 3,
                            Description = "Stylish sunglasses with UV protection",
                            ImagePath = "sunglasses.jpg",
                            Name = "Sunglasses",
                            Price = 29.99m,
                            Stock = 200
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 4,
                            Description = "Lightweight running shoes",
                            ImagePath = "running_shoes.jpg",
                            Name = "Running Shoes",
                            Price = 59.99m,
                            Stock = 150
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 1,
                            Description = "Classic blue denim jeans",
                            ImagePath = "mens_jeans.jpg",
                            Name = "menJeans",
                            Price = 39.99m,
                            Stock = 80
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 2,
                            Description = "Stylish leather handbag",
                            ImagePath = "mens_handbag.jpg",
                            Name = "menHandbag",
                            Price = 79.99m,
                            Stock = 70
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 3,
                            Description = "Elegant wristwatch",
                            ImagePath = "mens_watch.jpg",
                            Name = "menWatch",
                            Price = 199.99m,
                            Stock = 60
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryId = 4,
                            Description = "Comfortable summer sandals",
                            ImagePath = "mens_sandals.jpg",
                            Name = "menSandals",
                            Price = 34.99m,
                            Stock = 6
                        },
                        new
                        {
                            ProductId = 9,
                            CategoryId = 1,
                            Description = "Warm winter jacket",
                            ImagePath = "mens_jacket.jpg",
                            Name = "menJacket",
                            Price = 99.99m,
                            Stock = 4
                        },
                        new
                        {
                            ProductId = 10,
                            CategoryId = 2,
                            Description = "Silk scarf",
                            ImagePath = "mens_scarf.jpg",
                            Name = "menScarf",
                            Price = 24.99m,
                            Stock = 120
                        });
                });

            modelBuilder.Entity("TheFashionCanvas.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Apartment")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = "1",
                            Apartment = "Apt 1",
                            City = "Admin City",
                            Email = "admin@fashioncanvas.com",
                            FirstName = "Admin",
                            LastName = "User",
                            Password = "Admin@123",
                            PhoneNumber = "1234567890",
                            PostalCode = "12345",
                            Province = "Admin Province",
                            Role = "Admin",
                            StreetAddress = "123 Admin Street",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("Cart", b =>
                {
                    b.HasOne("TheFashionCanvas.Models.User", "User")
                        .WithMany("Carts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TheFashionCanvas.Models.CartItem", b =>
                {
                    b.HasOne("Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheFashionCanvas.Models.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TheFashionCanvas.Models.Order", b =>
                {
                    b.HasOne("TheFashionCanvas.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TheFashionCanvas.Models.OrderItem", b =>
                {
                    b.HasOne("TheFashionCanvas.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheFashionCanvas.Models.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TheFashionCanvas.Models.Product", b =>
                {
                    b.HasOne("TheFashionCanvas.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("TheFashionCanvas.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("TheFashionCanvas.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("TheFashionCanvas.Models.Product", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("TheFashionCanvas.Models.User", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
