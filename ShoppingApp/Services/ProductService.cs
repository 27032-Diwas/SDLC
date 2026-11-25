using ShoppingApp.Interfaces;
using ShoppingApp.Models;

namespace ShoppingApp.Services;

public class ProductService(
    IProductRepository productRepository)
    : IProductService
{
    public List<Product> GetAllProducts()
    {
        return productRepository.GetAllProducts();
    }

    public Product? GetProductById(Guid productId)
    {
        return productRepository.GetProductById(productId);
    }

    public List<Product> SearchByName(string name)
    {
        return productRepository.SearchByName(name);
    }

    public List<Product> SearchByCategory(string category)
    {
        return productRepository.SearchByCategory(category);
    }

    public List<Product> SearchByPriceRange(
        decimal minPrice,
        decimal maxPrice)
    {
        return productRepository
            .SearchByPriceRange(
                minPrice,
                maxPrice);
    }

    public List<string> GetCategories()
    {
        return productRepository.GetCategories();
    }

    public void AddProduct(Product product)
    {
        productRepository.AddProduct(product);
    }

    public void UpdateProduct(Product product)
    {
        productRepository.UpdateProduct(product);
    }

    public void DeleteProduct(Guid productId)
    {
        productRepository.DeleteProduct(
            productId);
    }
}
