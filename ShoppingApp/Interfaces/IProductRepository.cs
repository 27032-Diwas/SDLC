using ShoppingApp.Models;

namespace ShoppingApp.Interfaces;

public interface IProductRepository
{
    List<Product> GetAllProducts();

    Product? GetProductById(Guid productId);

    List<Product> SearchByName(string name);

    List<Product> SearchByCategory(string category);

    List<Product> SearchByPriceRange(
        decimal minPrice,
        decimal maxPrice);

    void UpdateProduct(Product product);

    void AddProduct(Product product);

    void DeleteProduct(Guid productId);

    List<string> GetCategories();
}
