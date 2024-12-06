using System;
using Microsoft.EntityFrameworkCore;

namespace MarketSatis
{
	public class MarketContext:DbContext
	{
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=MarketSatis;User ID=sa; Password=reallyStrongPwd123;TrustServerCertificate=true");
        }
    }
}

