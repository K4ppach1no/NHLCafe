using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Repository;

namespace NHLCafe.Pages.Auth.Product;

public class Index : PageModel
{
    public List<Models.Category>? Categories { get; private set; }
    public List<Models.Product>? Products { get; private set; }
    public string? Msg { get; private set; }

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("session") == null)
            return RedirectToPage("../../Login");
        
        var prod = new ProductRepository();
        Products = prod.GetAll();
        var cat = new CategoryRepository();
        Categories = cat.GetAll();
        
        return Page();
    }
    
    public IActionResult OnPost([FromForm] string productName, int categoryId, double price)
    {
        if (HttpContext.Session.GetString("session") == null)
            return RedirectToPage("../../Login");
        
        var prod = new ProductRepository();
        Products = prod.GetAll();
        var cat = new CategoryRepository();
        Categories = cat.GetAll();

        if (productName is null or "")
        {
            Msg = "Product name cannot be empty";
            return Page();
        }
        else if (Products != null && Products.Any(p => p.Name == productName))
        {
            Msg = "Product name already exists";
            return Page();
        }
        else if (price <= 0)
        {
            Msg = "Price cannot be less than or equal to zero";
            return Page();
        }
        else
        {
            if (price >= 100)
                price /= 100;
            
            price = Math.Round(price, 2);
            prod.Add(productName, categoryId, price);
            Msg = "Product Added";
            return RedirectToPage("/Auth/Product/Index");
        }

    }
}