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
    }
}