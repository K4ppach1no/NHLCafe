using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Repository;

namespace NHLCafe.Pages;

public class IndexModel : PageModel
{ 
    public IActionResult OnGet()
    {
        //redirect to order page
        return RedirectToPage("/Order");
    }
}
