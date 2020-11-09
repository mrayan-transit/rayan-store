using Microsoft.EntityFrameworkCore;

namespace RayanStore.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Product> Products {get;set;}

        public DbSet<User> Users {get;set;}

        public DbSet<Order> Orders {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>()
                .HasKey(x => new { x.OrderId, x.ProductId });
            base.OnModelCreating(modelBuilder);
        }
    }
}