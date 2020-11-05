using Microsoft.EntityFrameworkCore;

namespace RayanStore.Data
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Product> Products {get;set;}
    }
}