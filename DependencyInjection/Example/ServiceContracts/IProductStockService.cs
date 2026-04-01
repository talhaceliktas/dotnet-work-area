using Entities;

namespace ServiceContracts
{
    public interface IProductStockService
    {
        List<Product> GetAllProducts();

        bool IsInStock(int productId);

        bool UpdateStock(int productId, int quantity);
    }
}
