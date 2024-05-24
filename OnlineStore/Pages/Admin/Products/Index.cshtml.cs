using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        private readonly OnlineStoreDbContext _dbContext;

        public List<Product> Products { get; set; } = new();
        public IndexModel(OnlineStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet()
        {
            Products = _dbContext.Products.OrderByDescending(p => p.Id).ToList();
        }
    }
}
