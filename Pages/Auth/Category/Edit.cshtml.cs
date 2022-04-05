using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Repository;

namespace NHLCafe.Pages.Auth.Category;
public class Edit : PageModel
{
    public List<Models.Category> Categories { get; set; }
    public Models.Category Category { get; set; }
    
    public IActionResult OnGet([FromQuery] int CategoryId)
    {
        Categories = new CategoryRepository().GetAll();
        Category = new CategoryRepository().GetById(CategoryId);
        return Page();
    }
    
    public IActionResult OnPost([FromForm] string CategoryName, [FromQuery] int CategoryId)
    {
        Categories = new CategoryRepository().GetAll();
        Category = new CategoryRepository().GetById(CategoryId);
        
        if (ModelState.IsValid )
        {
            new CategoryRepository().Update(CategoryName, CategoryId);
            return RedirectToPage("/Auth/Category/Index");
        }
        return Page();
    }
}