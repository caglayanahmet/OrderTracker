using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderTracker.Models.DataModels;
using Microsoft.EntityFrameworkCore;


namespace OrderTracker.Models
{
    public class OrderTrackerDbContext : DbContext
    {
        public OrderTrackerDbContext(DbContextOptions<OrderTrackerDbContext> options) : base(options)
        {
        }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<SellerEntity> Sellers { get; set; }
        public DbSet<DeliveryInfoEntity> DeliveryInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
