using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Repository;

namespace NHLCafe.Pages.Auth.Category;

public class Delete : PageModel
{
    public IActionResult OnGet([FromQuery]int categoryId)
    {
        if (HttpContext.Session.GetString("session") == null || categoryId == 0)
            return RedirectToPage("../../Login");

        var cat = new CategoryRepository();
        cat.Delete(categoryId);
        return RedirectToPage("/Auth/Category/Index");
        
    }
}