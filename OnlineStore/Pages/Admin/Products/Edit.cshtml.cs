using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.Pages.Admin.Products
{
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly OnlineStoreDbContext _dbContext;
        [BindProperty]
        public ProductDto ProductDto { get; set; } = new();
        public Product Product { get; set; } = new();

        public string errorMessage = "";
        public string successMessage = "";

        public EditModel(IWebHostEnvironment environment,OnlineStoreDbContext dbContext)
        {
            _environment = environment;
            _dbContext = dbContext;
        }
        public void OnGet(int? id)
        {
            if(id == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            var product = _dbContext.Products.Find(id);
            if(product == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            ProductDto.Name = product.Name;
            ProductDto.Brand= product.Brand;
            ProductDto.Description = product.Description;
            ProductDto.Price = product.Price;
            ProductDto.Category=product.Category;

            Product = product;
        }

        public void OnPost(int? id)
        {
            if(id == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            if(!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields";
                return;
            }

            var product= _dbContext.Products.Find(id);
            if(product == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            string newImageFileName=product.ImageFileName;
            if(ProductDto.ImageFile != null)
            {
                newImageFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newImageFileName += Path.GetExtension(ProductDto.ImageFile.FileName);

                string imageFullPath= _environment.WebRootPath + "/product-images/" + newImageFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    ProductDto.ImageFile.CopyTo(stream);
                }

                string oldImageFullPath = _environment.WebRootPath + "/product-images/" + product.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }

            product.Name=ProductDto.Name;
            product.Brand = ProductDto.Brand;
            product.Category=ProductDto.Category;
            product.Price=ProductDto.Price;
            product.Description=Product.Description?? "";
            product.ImageFileName=newImageFileName;
            
            _dbContext.SaveChanges();



            Product = product;
            successMessage = "Product updated successfully";

            Response.Redirect("/Admin/Products/Index");
        }
    }
}
