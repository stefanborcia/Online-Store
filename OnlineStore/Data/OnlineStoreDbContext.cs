using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

namespace OnlineStore.Data
{
    public class OnlineStoreDbContext:DbContext
    {
        public OnlineStoreDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Product> Products {  get; set; }
    }
}
