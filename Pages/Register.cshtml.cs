using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Models;
using NHLCafe.Pages.Repository;
using System.ComponentModel.DataAnnotations;
using static NHLCafe.Pages.Repository.StaticUserRepository;

namespace NHLCafe.Pages
{
    public class RegisterModel : PageModel
    {
        [Required] [BindProperty] public string UserName { get; set; } = string.Empty;
        [Required] [BindProperty] public string Password { get; set; } = string.Empty;

        public string Msg { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            else
            {
                CafeUser NewUser = new CafeUser();
                NewUser.UserName = UserName;
                NewUser.Password = Password;
                NewUser.UniqueGuid = Guid.NewGuid();

                AddUserResult result = StaticUserRepository.AddUser(NewUser);
                if (result == AddUserResult.Success)
                    return new RedirectToPageResult("Login");
                else
                {
                    Msg = result.ToString();
                    return Page();
                }
            }
        }
    }
}
