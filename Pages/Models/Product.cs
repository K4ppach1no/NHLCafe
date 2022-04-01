namespace NHLCafe.Pages.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public double Price { get; set; }

    }
}