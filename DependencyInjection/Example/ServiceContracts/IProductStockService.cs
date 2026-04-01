using Entities;

namespace ServiceContracts
{
    public interface IProductStockService
    {
        List<Product> GetAllProducts();

        bool IsInStock(Guid productId);

        bool UpdateStock(Guid productId, int quantity);
    }
}
