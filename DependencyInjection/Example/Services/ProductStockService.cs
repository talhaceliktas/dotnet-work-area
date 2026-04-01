using Entities;
using ServiceContracts;

namespace Services
{
    public class ProductStockService : IProductStockService
    {
        private readonly List<Product> _inMemoryProducts;

        public ProductStockService()
        {
            _inMemoryProducts = new List<Product>()
            {
                new Product { Id = 1, Name = "MacBook Pro", StockQuantity = 10 },
                new Product { Id = 2, Name = "Logitech Mouse", StockQuantity = 45 },
                new Product { Id = 3, Name = "Mekanik Klavye", StockQuantity = 0 }
            };
        }

        public List<Product> GetAllProducts()
        {
            return _inMemoryProducts;
        }

        public bool IsInStock(int productId)
        {
            return _inMemoryProducts.Any(x=> x.Id == productId && x.StockQuantity > 0);
        }

        public bool UpdateStock(int productId, int quantity)
        {
            Product? product = _inMemoryProducts.FirstOrDefault(x => x.Id == productId);
            if (product != null) {
                product.StockQuantity = quantity;
                return true;
            }
            return false;
        }
    }
}
