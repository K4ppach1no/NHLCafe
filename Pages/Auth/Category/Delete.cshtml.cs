using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Repository;

namespace NHLCafe.Pages.Auth.Category;

public class Delete : PageModel
{
    public IActionResult OnGet([FromQuery]int CategoryId)
    {
        if (HttpContext.Session.GetString("session") == null || CategoryId == 0)
            return RedirectToPage("../../Login");

        var cat = new CategoryRepository();
        cat.Delete(CategoryId);
        return RedirectToPage("/Auth/Product/Index");
        
    }
}