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
                new Product { Id = Guid.NewGuid(), Name = "MacBook Pro", StockQuantity = 10 },
                new Product { Id = Guid.NewGuid(), Name = "Logitech Mouse", StockQuantity = 45 },
                new Product { Id = Guid.NewGuid(), Name = "Mekanik Klavye", StockQuantity = 0 }
            };
        }

        public List<Product> GetAllProducts()
        {
            return _inMemoryProducts;
        }

        public bool IsInStock(Guid productId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStock(Guid productId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
