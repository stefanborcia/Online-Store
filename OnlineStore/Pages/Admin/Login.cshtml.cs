using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineStore.Pages.Admin
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "User name")]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "Password")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Check if the user is an admin
                if (Input.UserName == "admin" && Input.Password == "password")
                {
                    // Create a new claim for the user
                    var claims = new[] {
                        new Claim(ClaimTypes.Name, Input.UserName),
                        new Claim(ClaimTypes.Role, "Admin")
                    };

                    // Create a new identity for the user
                    var identity = new ClaimsIdentity(claims, "Login");

                    // Create a new principal for the user
                    var principal = new ClaimsPrincipal(identity);

                    // Set the current user to the new principal
                    await HttpContext.SignInAsync(principal);

                    // Redirect to the admin page
                    return RedirectToPage("/Admin/Products/Index");
                }
            }
            // If we got this far, something failed, redisplay the form
            return Page();

        }
    }
}
