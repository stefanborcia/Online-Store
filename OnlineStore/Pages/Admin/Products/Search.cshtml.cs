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

        public IList<Product> Products { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        public IActionResult OnGet()
        {
            if (string.IsNullOrWhiteSpace(Search))
            {
                TempData["Message"] = "Search field can not be empty.";
                return Page();
            }

            Products = _dbContext.Products
                .Where(p => p.Name.Contains(Search))
                .ToList();

            if (!Products.Any())
            {
                TempData["Message"] = "No products found.";
            }

            return Page();
        }
    }
}
