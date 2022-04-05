using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Models;
using NHLCafe.Pages.Repository;
using Microsoft.AspNetCore.Mvc;
using NHLCafe.Pages.Helpers;

namespace NHLCafe.Pages
{
    public class OrderModel : PageModel
    {
        public List<Category>? Categories { get; set; }
        public List<Product>? Products { get; set; }
        public List<Item>? Cart { get; set; }
        public List<Item>? PerPersoonBetalen { get; set; }
        public double Total { get; set; }
        public List<int> Tafels { get; set; } = new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        
        [BindProperty] public int TableNr { get; set; }
        
        [BindProperty] public int ProductId { get; set; }
        
        public void OnGet()
        {
            // create a new instance of the repository
            var cat = new CategoryRepository();
            // create a new instance of the repository
            var prod = new ProductRepository();
            
                
            //Get the categories from the repository
            Categories = cat.GetAll();
            
            //Get the products from the repository
            Products = prod.GetAll();
            
            Cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (Cart != null)
            {
                Total = Cart.Sum(item => item.Product.Price * item.Quantity);
            }
        }
        
        public void OnPost()
        {
            var prod = new ProductRepository();
            var cat = new CategoryRepository();
            Products = prod.GetAll();
            Categories = cat.GetAll();
        }
        
        public IActionResult OnPostPerPersoonBetalen(int productId, string action)
        {
            Cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") ?? new List<Item>();
            PerPersoonBetalen = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "perpersoonbetalen") ?? new List<Item>();
            var prod = new ProductRepository();
            var cat = new CategoryRepository();
            Categories = cat.GetAll();
            Products = prod.GetAll();
            
            switch (action)
            {
                case "Add":
                    // Add the product to the cart
                    if (PerPersoonBetalen.Exists(x => x.Product.ProductId == productId))
                    {
                        var availableQuantity = Cart.FirstOrDefault(x => x.Product.ProductId == productId).Quantity;
                        var item = PerPersoonBetalen.FirstOrDefault(x => x.Product.ProductId == productId);
                        if (item.Quantity < availableQuantity)
                            item.Quantity++;
                    }
                    else
                    {
                        PerPersoonBetalen.Add(new Item(prod.GetById(productId), 1));
                    }
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "perpersoonbetalen", PerPersoonBetalen);
                    break;
                case "Remove":
                    // Remove the product from the cart
                    if (PerPersoonBetalen.Exists(x => x.Product.ProductId == productId))
                    {
                        var item = PerPersoonBetalen.FirstOrDefault(x => x.Product.ProductId == productId);
                        item.Quantity -= 1;
                        if (item.Quantity <= 0)
                        {
                            PerPersoonBetalen.Remove(item);
                        }
                    }
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "perpersoonbetalen", PerPersoonBetalen);
                    break;
                case "Pay":
                    foreach (var item in PerPersoonBetalen)
                    {
                        Cart.FirstOrDefault(x => x.Product.ProductId == item.Product.ProductId).Quantity -= item.Quantity;
                        if (Cart.FirstOrDefault(x => x.Product.ProductId == item.Product.ProductId).Quantity <= 0)
                        {
                            Cart.Remove(Cart.FirstOrDefault(x => x.Product.ProductId == item.Product.ProductId));
                        }
                    }
                    
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
                    PerPersoonBetalen.Clear();
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "perpersoonbetalen", PerPersoonBetalen);
                    break;
            }


            return Page();
        }

        public IActionResult OnPostBuy([FromForm] int productId)
        {
            ModifyCart(productId, "add");
            
            CalculateTotal();
            return RedirectToPage("/Order");
        }

        public IActionResult OnPostMinus([FromForm] int productId)
        {
            ModifyCart(productId, "remove");
            
            CalculateTotal();

            return new RedirectResult("/Order");
        }
        
        public IActionResult OnPostPlus([FromForm] int productId)
        {
            ModifyCart(ProductId, "add");

            CalculateTotal();
            
            return new RedirectResult("/Order");
        }
        
        public IActionResult OnPostRemove([FromForm] int productId)
        {
            ModifyCart(productId, "remove");
            CalculateTotal();

            return new RedirectResult("/Order");
        }
        
        public IActionResult OnPostPay([FromForm] int productId)
        {
            // Empty the cart
            Cart = new List<Item>();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
            
            CalculateTotal();

            return new RedirectResult("/Order");
        }
        
        
        private int Exists(List<Item>? cart, int id)
        {
            if (cart == null)
                return -1;
            
            for (var i = 0; i < cart.Count; i++)
            {
                var product = cart[i].Product;
                if (product != null && product.ProductId == id)
                    return i;
            }
            
            return -1;
        }
        
        private void ModifyCart(int ProductId, string action)
        {
            var cat = new CategoryRepository();
            var prod = new ProductRepository();
            Categories = cat.GetAll();
            Products = prod.GetAll();
            
            Cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            
            var index = Exists(Cart, ProductId);
            Cart ??= new List<Item>();
            
            switch (action)
            {
                case "add":
                    // Check if the product is already in the cart
                    if (index == -1)
                    {
                        var product = prod.GetById(ProductId);
                        var item = new Item(product, 1);
                        Cart.Add(item);
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
                    }
                    // If the product is already in the cart, increase the quantity
                    else
                    {
                        Cart[index].Quantity++;
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
                    }
                    break;
                
                case "remove":
                    // check if the item is in the cart
                    if (index == -1) break;
                    
                    // check if we have more than one item in the cart
                    if (Cart[index].Quantity > 1)
                    {
                        Cart[index].Quantity--;
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
                    }
                    // if we have only one item in the cart, remove it
                    else
                    {
                        Cart.RemoveAt(index);
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
                    }
                    break;
            }
        }
        
        private void CalculateTotal()
        {
            Cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (Cart == null) return;
            Total = Cart.Sum(item =>
            {
                if (item.Product != null) return item.Product.Price * item.Quantity;
                return 0;
            });
        }
        
        // Get the quantity of a product in the PerPersoonBetalen
        public int GetQuantity(int productId)
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "perpersoonbetalen");
            var index = Exists(cart, productId);
            if (index == -1) return 0;
            return cart[index].Quantity;
        }

        public double GetTotalPerPersoon()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "perpersoonbetalen");
            if (cart == null) return 0;
            return cart.Sum(item =>
            {
                if (item.Product != null) return item.Product.Price * item.Quantity;
                return 0;
            });
        }
        
    }
}
