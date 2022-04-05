using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Models;
using NHLCafe.Pages.Repository;

namespace NHLCafe.Pages.Auth.Product;

public class Edit : PageModel
{
    public List<Models.Category> Categories { get; set; }
    public Models.Product Product { get; set; }
    public string Msg { get; set; }
    
    public IActionResult OnGet([FromQuery] int ProductId)
    {
        if (HttpContext.Session.GetString("session") == null)
            return RedirectToPage("../../Login");
        
        var cat = new CategoryRepository();
        Categories = cat.GetAll();
        Product = new ProductRepository().GetById(ProductId);
        
        return Page();
    }

    public IActionResult OnPost([FromForm] string ProductName, int CategoryId, double Price, [FromQuery] int ProductId)
    {
        var product = new ProductRepository();
        product.Update(ProductId, ProductName, CategoryId, Price);

        Msg = "Product updated successfully";
        return RedirectToPage("/Auth/Product/Index");
    }
}