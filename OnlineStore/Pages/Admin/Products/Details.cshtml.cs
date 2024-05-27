using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.Pages.Admin.Products
{
    public class DetailsModel : PageModel
    {
        private readonly OnlineStoreDbContext _dbContext;

        public DetailsModel(OnlineStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _dbContext.Products.FindAsync(id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}

