using Microsoft.EntityFrameworkCore;
using OrderService.Models;
using OrderService.Data;

namespace OrderService.Data{
    public class OrderContext : DbContext{
        public OrderContext(DbContextOptions<OrderContext> options) : base(options){}
        public DbSet<Order> Orders {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Order>().Property(o => o.ProductName).IsRequired();
        }
    }
}
