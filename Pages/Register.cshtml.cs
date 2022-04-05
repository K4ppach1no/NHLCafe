using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Repository;
using System.ComponentModel.DataAnnotations;

namespace NHLCafe.Pages
{
    public class RegisterModel : PageModel
    {
        [Required] [BindProperty] public string UserName { get; set; } = string.Empty;
        [Required] [BindProperty] public string Password { get; set; } = string.Empty;
        [Required][BindProperty] public string ConfirmPassword { get; set; } = string.Empty;

        public string Msg { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            else if (Password != ConfirmPassword)
            {
                Msg = "Passwords do not match";
                return Page();
            }
            else
            {
                var user = new UserRepository();
                user.AddUser(UserName, Password);
                Msg = "User added successfully";
                return RedirectToPage("Login");
            }
        }
    }
}
