using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Models;
using NHLCafe.Pages.Repository;
using System.ComponentModel.DataAnnotations;

namespace NHLCafe.Pages
{
    public class LoginModel : PageModel
    {
        [Required] [BindProperty] public string UserName { get; set; } = string.Empty;
        [Required] [BindProperty] public string Password { get; set; } = string.Empty;
        public string Msg { get; set; } = string.Empty;
        
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("session") != null)
                return RedirectToPage("/Auth/AccountOverview");
            return Page();
        }

        public IActionResult OnPost()
        {
            var user = new UserRepository();
            if (ModelState.IsValid)
            {
                var res = user.Auth(UserName, Password).ToList();
                if (res[0].auth)
                {
                    HttpContext.Session.SetInt32("session", res[0].userid);
                    return RedirectToPage("/Auth/AccountOverview");
                }
                else
                {
                    Msg = "Invalid Credentials";
                    return Page();
                }
            }
            else
            {
                Msg = "Wrong username or password";
                return Page();
            }
        }
    }
}
