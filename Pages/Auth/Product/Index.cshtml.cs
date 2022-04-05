using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Models;
using NHLCafe.Pages.Repository;

namespace NHLCafe.Pages.Auth.Product;

public class Index : PageModel
{
    public List<Models.Category> Categories { get; set; }
    public List<Models.Product> Products { get; set; }
    public string Msg { get; set; }

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
        var prod = new ProductRepository();
        Products = prod.GetAll();
        var cat = new CategoryRepository();
        Categories = cat.GetAll();
        
        if (productName == null || productName == "")
        {
            Msg = "Product name cannot be empty";
        }
        else if (price <= 0)
        {
            Msg = "Price cannot be less than or equal to zero";
        }
        else
        {
            price = Math.Round(price/100, 2);
            prod.Add(productName, categoryId, price);
            Msg = "Product Added";
        }
        
        return RedirectToPage("/Auth/Product/Index");
        
    }
}