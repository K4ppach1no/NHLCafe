using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Repository;

namespace NHLCafe.Pages.Auth.Category;
public class Edit : PageModel
{
    public List<Models.Category>? Categories { get; set; }
    public Models.Category? Category { get; private set; }
    
    public IActionResult OnGet([FromQuery] int categoryId)
    {
        Categories = new CategoryRepository().GetAll();
        Category = new CategoryRepository().GetById(categoryId);
        return Page();
    }
    
    public IActionResult OnPost([FromForm] string categoryName, [FromQuery] int categoryId)
    {
        Categories = new CategoryRepository().GetAll();
        Category = new CategoryRepository().GetById(categoryId);

        if (!ModelState.IsValid) return Page();
        
        new CategoryRepository().Update(categoryName, categoryId);
        return RedirectToPage("/Auth/Category/Index");
    }
}