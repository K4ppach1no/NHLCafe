using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Models;
using NHLCafe.Pages.Repository;

namespace NHLCafe.Pages.Auth.Product;

public class Edit : PageModel
{
    public List<Models.Category>? Categories { get; private set; }
    public Models.Product? Product { get; private set; }
    public string? Msg { get; private set; }
    
    public IActionResult OnGet([FromQuery] int productId)
    {
        if (HttpContext.Session.GetString("session") == null)
            return RedirectToPage("../../Login");
        
        var cat = new CategoryRepository();
        Categories = cat.GetAll();
        Product = new ProductRepository().GetById(productId);
        
        return Page();
    }

    public IActionResult OnPost([FromForm] string productName, int categoryId, double price, [FromQuery] int productId)
    {
        var product = new ProductRepository();
        product.Update(productId, productName, categoryId, price);

        Msg = "Product updated successfully";
        return RedirectToPage("/Auth/Product/Index");
    }
}