using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

namespace OnlineStore.Data
{
    public class OnlineStoreDbContext: IdentityDbContext<IdentityUser>
    {
        public OnlineStoreDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Product> Products {  get; set; }
    }
}
