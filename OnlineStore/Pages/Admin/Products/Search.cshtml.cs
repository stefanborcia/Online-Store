using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.Pages.Admin.Products
{
    public class SearchModel : PageModel
    {
        private readonly OnlineStoreDbContext _dbContext;

        public SearchModel(OnlineStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> Products { get; set; }

        public async Task<IActionResult> OnGetAsync(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                Products = new List<Product>();
            }
            else
            {
                Products = await _dbContext.Products
                    .Where(p => p.Name.Contains(search))
                    .ToListAsync();
            }
            return Page();
        }
    }
}
