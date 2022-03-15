using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Models;
using NHLCafe.Pages.Repository;

namespace NHLCafe.Pages
{
    public class AccountOverviewModel : PageModel
    {
        [BindProperty] public string Username { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            if (HttpContext.Session.Id == null)
                return RedirectToPage("Login");

            Guid guid = new Guid(HttpContext.Session.Id);
            CafeUser LoggedInUser = StaticUserRepository.GetUserById(guid);

            if (LoggedInUser == null)
                return RedirectToPage("Login");

            Username = LoggedInUser.UserName;

            return Page();
        }
    }
}
