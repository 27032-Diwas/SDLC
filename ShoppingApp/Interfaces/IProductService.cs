using ShoppingApp.Models;

namespace ShoppingApp.Interfaces;

public interface IProductService
{
    List<Product> GetAllProducts();

    Product? GetProductById(Guid productId);

    List<Product> SearchByName(string name);

    List<Product> SearchByCategory(string category);

    List<Product> SearchByPriceRange(
        decimal minPrice,
        decimal maxPrice);
}