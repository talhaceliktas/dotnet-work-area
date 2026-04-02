using Entities;

namespace ServiceContracts
{
    public interface IProductStockService
    {
        List<Product> GetAllProducts();

        Product? GetProduct(int productId);

        bool UpdateStock(int productId, int quantity);
    }
}
