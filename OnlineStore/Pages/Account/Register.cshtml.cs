using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public RegisterModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public void OnGet()
        {
            var roles = _roleManager.Roles.ToList();
            var roleItems = new List<SelectListItem>();
            foreach (var role in roles)
            {
                roleItems.Add(new SelectListItem { Text = role.Name, Value = role.Id });
            }
            ViewData["Roles"] = roleItems;
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = _userManager.CreateAsync(user, Input.Password).Result;

                if (result.Succeeded)
                {
                    if (_userManager.IsInRoleAsync(user, Input.Role).Result)
                    {
                        ModelState.AddModelError(string.Empty, "User already has the selected role.");
                        return Page();
                    }

                    var roleResult = _userManager.AddToRoleAsync(user, Input.Role).Result;

                    if (roleResult.Succeeded)
                    {
                        return RedirectToPage("/Index");
                    }

                    foreach (var error in roleResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            [Required]
            [Display(Name = "Role")]
            public string Role { get; set; }
        }
    }
}
