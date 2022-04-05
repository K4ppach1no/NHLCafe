using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Repository;

namespace NHLCafe.Pages
{
    public class AccountOverviewModel : PageModel
    {
        [BindProperty] public string Username { get; private set; } = "";

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("session") == null)
                return RedirectToPage("../Login");
            else
            {
                var user = new UserRepository();

                Username = user.GetById((int)HttpContext.Session.GetInt32("session")).UserName;
                return Page();
            }
        }
    }
}
