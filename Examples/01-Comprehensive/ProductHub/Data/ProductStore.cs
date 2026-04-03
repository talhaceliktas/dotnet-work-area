namespace ProductHub.Models 
{
    public static class ProductStore
    {
        public static List<Product> ProductList { get; set; } = new List<Product>
        {
            new Product { Id = 1, Name = "iPhone 15", Price = 60000 },
            new Product { Id = 2, Name = "MacBook Pro", Price = 90000 },
            new Product { Id = 3, Name = "AirPods", Price = 8000 }
        };
    }
}