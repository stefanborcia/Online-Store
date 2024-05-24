using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly OnlineStoreDbContext _dbContext;
        [BindProperty]
        public ProductDto ProductDto { get; set; } = new();

        public CreateModel(IWebHostEnvironment environment,OnlineStoreDbContext dbContext)
        {
            _environment= environment;
            _dbContext= dbContext;
        }
        public void OnGet()
        {
        }
        public string errorMessage = "";
        public string successMessage = "";
        public void OnPost()
        {
            if (ProductDto.ImageFile == null)
            {
                ModelState.AddModelError("ProductDto.ImageFile", "The image file is required");
            }

            if(!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields";
                return;
            }

            string newImageFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newImageFileName += Path.GetExtension(ProductDto.ImageFile!.FileName);
            string imageFullPath = _environment.WebRootPath + "/product-images/" + newImageFileName;
            using(var stream=System.IO.File.Create(imageFullPath))
            {
                ProductDto.ImageFile.CopyTo(stream);
            }

            Product product = new Product()
            {
                Name=ProductDto.Name,
                Brand=ProductDto.Brand,
                Category=ProductDto.Category,
                Price=ProductDto.Price,
                Description=ProductDto.Description ?? "",
                ImageFileName= newImageFileName,
                CreateAt=DateTime.Now
            };

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            ProductDto.Name = "";
            ProductDto.Brand = "";
            ProductDto.Category = "";
            ProductDto.Price = 0;
            ProductDto.Description = "";
            ProductDto.ImageFile = null;
            ModelState.Clear();

            successMessage = "Product created successfully";

            Response.Redirect("/Admin/Products/Index");
        }
    }
}
