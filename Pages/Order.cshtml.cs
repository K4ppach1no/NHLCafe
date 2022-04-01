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
        public double Total { get; set; }
        
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

        public void OnPostBuy([FromForm] int productId)
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
            // check if we have a cart
            if (Cart == null)
            {
                Cart = new List<Item>();
                Cart.Add(new Item(prod.GetById(productId), 1));
                
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
                
                CalculateTotal();
            }
            // if we do we can use it
            else
            {
                int index = Exists(Cart, productId);
                
                if (index == -1)
                    Cart.Add(new Item(prod.GetById(productId), 1));
                else
                    Cart[index].Quantity++;
                
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
                
                CalculateTotal();
            }
        }

        public IActionResult OnPostMinus([FromForm] int productId)
        {
            var cat = new CategoryRepository();
            var prod = new ProductRepository();
            Categories = cat.GetAll();
            Products = prod.GetAll();
            
            Cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = Exists(Cart, productId);
            if (Cart != null && Cart[index].Quantity > 1)
            {
                Cart[index].Quantity--;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
            }
            else
            {
                Cart?.RemoveAt(index);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
            }
            
            CalculateTotal();

            return new RedirectResult("/Order");
        }
        
        public IActionResult OnPostPlus([FromForm] int productId)
        {
            var cat = new CategoryRepository();
            var prod = new ProductRepository();
            Categories = cat.GetAll();
            Products = prod.GetAll();
            
            Cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (Cart != null)
            {
                int index = Exists(Cart, productId);
                if (index != -1)
                {
                    Cart[index].Quantity++;
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
                }
            }

            CalculateTotal();
            
            return new RedirectResult("/Order");
        }
        
        public IActionResult OnPostRemove([FromForm] int productId)
        {
            var cat = new CategoryRepository();
            var prod = new ProductRepository();
            Categories = cat.GetAll();
            Products = prod.GetAll();
            
            Cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = Exists(Cart, productId);
            if (index != -1 && Cart != null)
            {
                Cart.RemoveAt(index);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
            }

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
    }
}
