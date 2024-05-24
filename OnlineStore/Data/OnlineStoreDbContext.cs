using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Data
{
    public class OnlineStoreDbContext:DbContext
    {
        public OnlineStoreDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
