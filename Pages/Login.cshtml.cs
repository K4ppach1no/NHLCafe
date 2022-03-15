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

        public const string SessionKeyId = "_Id";

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            CafeUser SessionKeyUser = StaticUserRepository.GetUser(UserName, Password);
            if (SessionKeyUser.UniqueGuid.ToString() != null)
            {
                HttpContext.Session.SetString(SessionKeyId, SessionKeyUser.UniqueGuid.ToString());
                return RedirectToPage("AccountOverview");
            }
            return Page();
        }
    }
}
