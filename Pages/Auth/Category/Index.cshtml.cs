using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Repository;

namespace NHLCafe.Pages.Auth.Category;

public class Index : PageModel
{
    public List<Models.Category>? Categories { get; private set; }
    public string? Msg { get; private set; }
    
    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("session") == null)
            return RedirectToPage("../../Login");

        Categories = new CategoryRepository().GetAll();

        return Page();
    }

    public IActionResult OnPost([FromForm] string name)
    {
        Categories = new CategoryRepository().GetAll();
        
        if (HttpContext.Session.GetString("session") == null)
            return RedirectToPage("../../Login");

        if (string.IsNullOrEmpty(name))
        {
            Msg = "Please enter a category name";
            return Page();
        }

        if (new CategoryRepository().Add(name))
            return RedirectToPage("/Auth/Category/Index");

        Msg = "Category already exists";
        return Page();
    }
}