using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineStore.Data;

namespace OnlineStore.Pages.Admin.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly OnlineStoreDbContext _dbContext;

        public DeleteModel(IWebHostEnvironment environment, OnlineStoreDbContext dbContext)
        {
            _environment=environment;
            _dbContext=dbContext;
        }
        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            var product = _dbContext.Products.Find(id);
            if (product == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            string imageFullPath = _environment.WebRootPath + "/product-images/" + product.ImageFileName;
            System.IO.File.Delete(imageFullPath);

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            Response.Redirect("/Admin/Products/Index");
        }
    }
}
