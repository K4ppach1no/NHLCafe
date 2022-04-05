using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Repository;

namespace NHLCafe.Pages.Auth.Product;

public class Delete : PageModel
{
    public IActionResult OnGet([FromQuery]int ProductId)
    {
        if (HttpContext.Session.GetString("session") == null)
            return RedirectToPage("../../Login");

        var prod = new ProductRepository();
        prod.Delete(ProductId);
        return RedirectToPage("/Auth/Product/Index");
        
    }
}