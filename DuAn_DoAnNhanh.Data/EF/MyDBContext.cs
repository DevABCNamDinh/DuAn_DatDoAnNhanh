using Microsoft.EntityFrameworkCore;
using DuAn_DoAnNhanh.Data.Configurations;
using DuAn_DoAnNhanh.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.EF
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure using Fluent API
            modelBuilder.ApplyConfiguration(new CartConfigurations());
            modelBuilder.ApplyConfiguration(new CartItemConfigurations());
            modelBuilder.ApplyConfiguration(new OrderConfigurations());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfigurations());
            modelBuilder.ApplyConfiguration(new ComboConfigurations());
            modelBuilder.ApplyConfiguration(new ProductComboConfigurations());
            modelBuilder.ApplyConfiguration(new ProductConfigurations());
            modelBuilder.ApplyConfiguration(new AddressConfigurations());
            modelBuilder.ApplyConfiguration(new UserConfigurations());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Bill> Orders { get; set; }
        public DbSet<BillDetails> BillDetailses { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCombo> productCombos { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
