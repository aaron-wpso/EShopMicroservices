﻿using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;
    public DiscountContext()
    {
        
    }
    public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "IPhone X", ProductDescription = "IPhone Discount", Amount = 150.00 },
            new Coupon { Id = 2, ProductName = "Samsung 10", ProductDescription = "Samsung Discount", Amount = 100.00 }
            );
    }
}
